# CsvSerialiserFrontend

This is the frontend plugin for the Vonk Server.

## Installation

NodeJS 14.15.* is needed for the project to compile.  
Publish project and copy `csv-serialiser` folder and `CsvFrontend.dll` to `webapplication/` **inside** the Vonk plugin folder. The folder `webapplication` will need to be created first. Alternatively, the folder can be changed in [VPlugin.cs line 15](VPlugin.cs#L15) and [VPlugin.cs line 29](VPlugin.cs#L29). 

Then add the plugin to the Vonk configuration by adding `CsvFrontend` to the Include-Path of PluginBranches, like so:

```
[ommitted]
"PipelineOptions": {
    "PluginDirectory": "./plugins",
    "Branches": [
      {
        "Path": "/",
        "Include": [
          "Vonk.Core",
          "Vonk.Fhir.R3",
          "Vonk.Fhir.R4",
          "CsvFrontend", <---- add this!
          [ommitted]
```

## Usage

Once the plugin is loaded, it is accessible via `http://localhost:4080`

## Error handling

Errors regarding a missing `Microsoft.AspNetCore.SpaServices.Extensions.dll` can be fixed by adding the following files to the same plugin folder. 

```
Microsoft.AspNetCore.SpaServices.Extensions.dll
Microsoft.AspNetCore.SpaServices.dll
```
