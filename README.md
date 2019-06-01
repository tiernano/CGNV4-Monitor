# CGNV4 Monitor

## What is it?
Quick and dirty .NET core app to read data from the [Hitron CGNV4](https://www.hitrontech.com/product/cgnv4/) cable modem offered by [Virgin Media](https://www.virginmedia.ie/) in Ireland (and other countries) for business (and possibly other) customers. 

## Whats it monitoring?
currenty it gets the upstream and downstream channels and their information, but depending on the region and config, you may get more. check the code in program.cs.

## whats it do with the info?
Current we throw it at [InfluxDB](https://www.influxdata.com/). i then use [Grafana](https://grafana.com/) for graphing it...

## what about Docker support
yea, its there. working on building an image, but you can build it manually by running `docker build .` in the folder...

## how do i build this?
[VS2017 or 2019](https://visualstudio.microsoft.com/), [.NET Core 2.2 SDK](https://dotnet.microsoft.com/), [Docker for Windows, Docker for Mac](https://www.docker.com/), [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/), Linux command line.. all possible... i built on Windows with either VS 2019 or VS code. 


## Whats next?
No idea. what do you want?! possibly looking into [Prometheus](https://prometheus.io/) support (seperate app). no idea though.

## License?
[MIT](LICENSE.md)