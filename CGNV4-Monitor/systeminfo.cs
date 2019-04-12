using System;
using System.Collections.Generic;
using System.Text;

namespace CGNV4_Monitor
{
    public class SystemInfo
    {
        public string hwVersion { get; set; }
        public string swVersion { get; set; }
        public string serialNumber { get; set; }
        public string rfMac { get; set; }
        public string wanIp { get; set; }
        public string aftrName { get; set; }
        public string aftrAddr { get; set; }
        public string delegatedPrefix { get; set; }
        public string lanIPv6Addr { get; set; }
        public string systemUptime { get; set; }
        public string systemTime { get; set; }
        public string timezone { get; set; }
        public string WRecPkt { get; set; }
        public string WSendPkt { get; set; }
        public string lanIp { get; set; }
        public string LRecPkt { get; set; }
        public string LSendPkt { get; set; }
    }

    public class CMInfo
    {
        public string hwInit { get; set; }
        public string findDownstream { get; set; }
        public string ranging { get; set; }
        public string dhcp { get; set; }
        public string timeOfday { get; set; }
        public string downloadCfg { get; set; }
        public string registration { get; set; }
        public string eaeStatus { get; set; }
        public string bpiStatus { get; set; }
        public string networkAccess { get; set; }
        public string trafficStatus { get; set; }
    }




    public class DocsisWanInfo
    {
        public string Configname { get; set; }
        public string NetworkAccess { get; set; }
        public string CmIpAddress { get; set; }
        public string CmNetMask { get; set; }
        public string CmGateway { get; set; }
        public string CmIpLeaseDuration { get; set; }
    }



    public class StreamInfo
    {
        public string portId { get; set; }
        public string frequency { get; set; }
        public string bandwidth { get; set; }
        public string scdmaMode { get; set; }
        public string signalStrength { get; set; }
        public string channelId { get; set; }
    }


    
    public class EventInfo
    {
        public int index { get; set; }
        public string time { get; set; }
        public string type { get; set; }
        public string priority { get; set; }
        public string _event { get; set; }
    }




}
