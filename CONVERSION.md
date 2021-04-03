# Conversion from .Net Framework to something that will run on Linux

Mainly as proof-of-concept for a larger project.

Web API
- not available in .Net Standard ?
- .Net Core App (Web API) cannot reference a .Net Standard library

Microsoft.EntityFrameworkCore
- .Net Framework or .Net Standard only

.Net Core vs .Net Standard
- https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/september/net-standard-demystifying-net-core-and-net-standard
```
Here’s how .NET Core and .NET Standard fit into this:

* .NET Core: This is the latest .NET implementation. It’s open source and available for multiple OSes. With .NET Core, you can build cross-platform console apps and ASP.NET Core Web applications and cloud services.

* .NET Standard: This is the set of fundamental APIs (commonly referred to as base class library or BCL) that all .NET implementations must implement. By targeting .NET Standard, you can build libraries that you can share across all your .NET apps, no matter on which .NET implementation or OS they run.
```

clearing NuGet caches - see if netcoreapp2.1 can now use netstandard2.1 libraries
- no, didn't help

looks like maybe netstandard2.0 CAN be used by netcoreapp2.1
- lowering EntityFrameworkCore version from 5.0.4 (netstandard2.1) to 3.1.13 (netstandard2.0)
- wait, here's the actual requirement `Microsoft.AspNetCore.App 2.1.1 -> Microsoft.EntityFrameworkCore (>= 2.1.1 && < 2.2.0).`

EntityFrameworkCore 2.1.14 limits EntityFrameworkCore.Relational to 2.1.14
- and that does not contain extension method `DbSet<T>.FromRawSql` which I need for stored proc calls
- looks like just calling it `FromSql` instead fixes it