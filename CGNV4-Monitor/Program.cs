using Flurl.Http;
using InfluxDB.Collector;
using NLog;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace CGNV4_Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger mLogger = LogManager.GetCurrentClassLogger();

            string baseUrl = "http://192.168.100.1";

            var cli = new FlurlClient(baseUrl).EnableCookies().Configure(settings =>
            {
                settings.BeforeCall = call => mLogger.Debug($"Calling CGNV4 Url {call.Request.RequestUri}");
                settings.OnError = call => mLogger.Error($"Error calling CGNV4: {call.RequestBody} -  {call.Exception}");
                settings.AfterCall = call =>
                    mLogger.Debug($"called CGNV4 with {call.HttpStatus} in {call.Duration}");
            });

            //urls i found:

            //http://192.168.100.1/data/getSysInfo.asp?_=1555060334993
            //http://192.168.100.1/data/dsinfo.asp?_=1555059523324
            //http://192.168.100.1/data/getCmDocsisWan.asp?_=1555059523327
            //http://192.168.100.1/data/usinfo.asp?_=1555059523329
            //http://192.168.100.1/data/getTuneFreq.asp?_=1555059523332
            //http://192.168.100.1/data/status_log.asp?_=1555059607669
            //http://192.168.100.1/data/wireless_basic.asp?_=1555059624496
            //http://192.168.100.1/data/wireless_ssid.asp?_=1555059624497
            //http://192.168.100.1/data/wireless_wifi4all.asp?_=1555059624501
            //http://192.168.100.1/data/mta_status.asp?_=1555059659383



            mLogger.Debug("Calling out to telegraf");
            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Batch.AtInterval(TimeSpan.FromSeconds(30))
                .WriteTo.InfluxDB("http://192.168.1.119:8086", "telegraf")
                .CreateCollector();

            while (true)
            {
                try
                {
                    var indexPage = cli.Request("/").GetStringAsync().GetAwaiter().GetResult();

                    string preSessionKey = cli.Cookies["preSession"].Value;

                    var login = cli.Request("goform/login").PostUrlEncodedAsync(new { usr = "admin", pwd = "admin", preSession = preSessionKey, forcelogoff = 1 }).ReceiveString().GetAwaiter().GetResult();

                    //var user = cli.Request("data/getUser.asp").GetStringAsync().GetAwaiter().GetResult();

                    //var sysInfo = cli.Request("data/getSysInfo.asp").GetJsonAsync<SystemInfo[]>().GetAwaiter().GetResult();

                    //var cmInfo = cli.Request("data/getCMInit.asp").GetJsonAsync<CMInfo[]>().GetAwaiter().GetResult();

                    var upstreamInfo = cli.Request("data/usinfo.asp").GetJsonAsync<StreamInfo[]>().GetAwaiter().GetResult();

                    var downstreamInfo = cli.Request("data/dsinfo.asp").GetJsonAsync<StreamInfo[]>().GetAwaiter().GetResult();

                    //var cmDocsisWan = cli.Request("data/getCmDocsisWan.asp").GetJsonAsync<DocsisWanInfo[]>().GetAwaiter().GetResult();

                    Dictionary<string, object> channelStuff = new Dictionary<string, object>();

                    foreach (var u in upstreamInfo)
                    {
                        channelStuff.Add($"us{u.channelId}", decimal.Parse(u.signalStrength));
                    }
                    foreach (var d in downstreamInfo)
                    {
                        channelStuff.Add($"ds{d.channelId}", decimal.Parse(d.signalStrength));
                    }

                    Metrics.Write("docsis_channel", channelStuff);
                }
                catch(Exception ex)
                {
                    mLogger.Error(ex, "Error somewhere");
                }

                Thread.Sleep(30000);
            }
        }
    }
}
