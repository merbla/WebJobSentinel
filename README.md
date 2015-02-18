# WebJobSentinel
A simple implementation to indicate shut down in Azure Web Jobs based on (https://github.com/projectkudu/kudu/wiki/Web-jobs), inspired by http://blog.amitapple.com/post/2014/05/webjobs-graceful-shutdown/

[![NuGet Version](http://img.shields.io/nuget/v/WebJobSentinel.svg?style=flat)](https://www.nuget.org/packages/WebJobSentinel/) [![Build status](https://ci.appveyor.com/api/projects/status/2yi7pkowhmxq7ydu?svg=true)](https://ci.appveyor.com/project/merbla/webjobsentinel)
 
##Installation & Usage
Simplly install the [WebJobSentinel](https://www.nuget.org/packages/WebJobSentinel/) NuGet package to a Web Job host (Console App or SDK)

`PM> Install-Package WebJobSentinel`


**Create the sentinel, with the file created/changed actions**

```
var mySentinel = new Sentinel(
    args => Log.Information("File Created"),
    args => Log.Information("File Changed"));
```

###OR

**Create the sentinel and wire up later**

```
var mySentinel = new Sentinel();

mySentinel.OnShutdownFileChanged += args => { //Do Something };
mySentinel.OnShutdownFileCreated += args => { //Do Something };
```

**Example output when using logging above**

```
[02/13/2015 22:42:40 > 59b875: INFO] 2015-02-13 22:42:40 [Information] Running and waiting
[02/13/2015 22:42:40 > 59b875: SYS INFO] Status changed to Disabling
[02/13/2015 22:42:40 > 59b875: SYS INFO] Status changed to Stopping
[02/13/2015 22:42:40 > 59b875: INFO] 2015-02-13 22:42:40 [Information] File Created
[02/13/2015 22:42:40 > 59b875: INFO] 2015-02-13 22:42:40 [Information] File Changed
```

## Contributing
Most welcome! Just fork away and send a pull request.

## Copyright

Copyright © 2015 Matthew Erbs & [Contributors](https://github.com/merbla/WebJobSentinel/graphs/contributors)
