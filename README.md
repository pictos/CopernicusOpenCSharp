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
using CopernicusOpenCSharp;

CopernicusService service = new CopernicusService("userName", "password");
```

- You can get the userName and password follow this steps <https://scihub.copernicus.eu/userguide/1SelfRegistration>

You can access the data information using this call:

```csharp
string id = "'fea3cd38-918d-4974-8586-2578cbb07844'";
var test  = await service.GetDataAsync(id: id);
```

If the 'id = null' the Service return all data from selected Entites.

## Extention Methods

```csharp
/// <summary>
/// Method to save MetaData in external file.
/// </summary>
/// <param name="data">MetaData</param>
/// <param name="path">Destination where you save the file</param>
/// <param name="fileMode">Specifies how  the operating system </param>
/// <returns>True if success, False if is not</returns>
public static bool SaveData(this string data,string path, FileMode fileMode = FileMode.Create);

/// <summary>
/// Method to return a .Net object from json string. AllData.
/// </summary>
/// <param name="json">MetaData returned from GetDataAsync.</param>
/// <returns>return the .net object for all data.</returns>
public static Query ExtractJson(this string json);

/// <summary>
/// Method to return a .Net object from json string. Specific data.
/// </summary>
/// <param name="json">MetaData returned from GetDataAsync</param>
/// <returns>return the .net object for all data.</returns>
public static QueryId ExtractJsonId(this string json);
```

With this extentions methods you can be more productive.

For ex. to get a Model Class for data with ID, you can do:

```csharp
using CopernicusOpenCSharp.Extensions;

var teste  = await service.GetDataAsync(id: id);
var test2  = teste.ExtractJsonId();
```