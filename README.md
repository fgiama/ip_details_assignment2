# ip_stack_assesment

The Framework used for this solution is .NET Framework 4.7.2

This solution creates:

1. A library 'IPDetailsLibrary' that creates a request to IPStack API to get information about an IP. 
	- When the IPStack responses with an error, then an Exception with this error message is thrown. 
	- When the no Ip inforamtion are received, then an Exception is thrown.
	- Finally, the library throws an IpServiceNotAccessibleException if any exception is thrown.

Nuget Packages used :
  - Newtonsoft.Json: version="12.0.2"


2. An ASP.NET Wep Api that calls the library above to get IP information. Moreover:
	- It uses EntityFramework 6 as an ORM to connect to db.
	- .NET Memory cache is used for caching.
	- For dependency injection Autofac is used.
	- The expiration of the Cache is defined by configuration in web.config
	- There are scripts in folder 'IPDetailsWebApi\DBScripts' to create database and tables.

Nuget Packages used :
  - Newtonsoft.Json: version="12.0.2"
  - Autofac: version="4.9.4"
  - Autofac.WebApi2: version="4.3.1"
  - EntityFramework: version="6.2.0"

To use the Web API use the following uri:

	- Get ip details : api/ipdetails/{ip} (get)

3. A Windows Service that writes a set of ip adresses in a csv every 5 minutes 
	- For the csv creation CSVHelper is used.
	- The service name is IP.Details.Service
	- In order for the service to connect to sql server I had to give access to local system. 
	In Sql Server Management Studio navigate to Security->Logins->NT AUTHORITY\SYSTEM:right click -> Properties->Server Roles check sysadmin.


 Nuget Packages used :
  - Newtonsoft.Json: version="12.0.2"
  - CsvHelper: version="12.1.2"

To use the Service install following the instructions bellow:

- Rebuild the application.
- Run command prompt as administrator
- Fire command:  cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
- Copy the bin\Debug folder of the appplication
- Fire command: InstallUtil.exe {path_to_debug_folder}\IPDetailsService.exe
- Go to Services and find IP.Details.Service
- Start the service.
- In bin\Debug\CSVs folder, a cvs will be created every 5 minutes.
- The file format, the headers and the interval can be modified in IPDetailsService.exe.config 
