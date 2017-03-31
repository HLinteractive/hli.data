![HL Interactive](https://www.dropbox.com/s/fdyzvkso9zs9ndf/HLi.Signature.DVDs.jpg?dl=1)
> HL Interactive (HLi) Source Code Readme
> Copyright © HL Interactive 2015, Thomas Hagström,
> Horisontvägen 85, Stockholm, Sweden

# <a name="hlidata"></a>HLI.Data #
HL Interactive (HLi) data operations project.

## Purpose and Scope ##
Typed ```HttpClient``` and other portable .net client utilities.

## Usage ##
### HliHttpClient ###
[**`HttpClient`**](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient "Microsoft HttpClient") with strongly typed request / response. It also parses HTTP errors and throws friendly Exceptions.

#### Typed GET Request ####

    var client = new HliHttpClient("http://httpbin.org/", "ip");
    var result = await client.GetDataAsTypeAsync<IpResponse>();

#### Typed POST Request ####

    var client = new HliHttpClient("http://httpbin.org/", "post");
    var result = await client.PostDataAsTypeAsync<HttpBinPost>();

## Delivery & Deployment ##
Download the nuget package through Package Manager Console:

> install-package HLI.Data

## Dependencies ##
* **Projects**
* **Packages**
	* HLI.Core
	* Microsoft.Net.Http
	* Newtonsoft.Json

### NuGet Package Generation ###
The project **HLI.Data** is configured to automatically generate a ***.nupkg** upon build with ```dotnet cli```.

## Solution File Structure ##

* **HLI.Data** - solution root folder
	* **HLI.Data (Portable)**  - main project. Depends on Microsoft.AspNet.WebApi.Client
		* **Clients** - contain helper classes for constructing clients for web services HTTP, WCF etc.
		* **Extensions** - data layer related extensions.
	* **HLI.Data.Tests.Integration** - integration tests for this solution

## Changes and backward compatibility ##
### 1.0.5.0 ###
- Converted to netcore: multi-target netstandard 1.4 & PCL.

### 1.0.4.3 ###
- Removed invalid `Microsoft.AspNet.WebApi.Client` reference (fixing Android errors).
- Optimized Json Serialization / Deserialization

### 1.0.4.2 ###
- *Newtonsoft.Json* is now set to **`Copy local = False`** to prevent conflicts.
Ensure the above package is installed and referenced in the platform project.

- Constructors for `HliHttpClient` are now completely revised to prevent ambigus constructor issues. Usage might need to be updated.

- Added support for different type request / response in **`PostDataAsTypeAsync`**

## Unit Tests ##
Unit Tests as well as integration tests should be placed in solution root.