# CopernicusOpen CSharp

This repository contains the source code of nuget packet of Copernicus Satellite for .NET.
<http://copernicus.eu>

## Setup

- Available on NuGet: <https://www.nuget.org/packages/CopernicusOpenCSharp/>
- Install into your core project.

### Methods

```csharp
/// <summary>
/// Method to get metadata of all resource or specific resource
/// </summary>
/// <param name="options">Enumerable of possible resources</param>
/// <param name="format">Enumerable of possible formats to get data</param>
/// <param name="id">id for get a specific product</param>
/// <returns></returns>
Task<string> GetDataAsync(Entites options = Entites.Products, Format format = Format.json, string id = null);
 /// <summary>
 /// Method to download and save the metadata into a file.
 /// </summary>
 /// <param name="path">Destination of downloaded file</param>
 /// <param name="options">Resource to download</param>
 /// <param name="id">Id of resource</param>
 /// <returns>Return true for success</returns>
Task<bool> DownloadData(string path, string id = null, Entites options = Entites.Products);
```

### API Usage

To gain access to the resources from Copernicus simply create an instance from service, like this:

```csharp
CopernicusService service = new CopernicusService("userName", "password");
```

- You can get the userName and password follow this steps <https://scihub.copernicus.eu/userguide/1SelfRegistration>

You can access the data information using this call:

```csharp
string id = "'fea3cd38-918d-4974-8586-2578cbb07844'";
var test  = await service.GetDataAsync(id: id);
```

If the 'id = null' the Service return all data from selected Entites.
