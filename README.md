# Cake.MultiTarget

A Cake extension that hacks in the ability to pass in multiple targets (tasks) to run as comma separated values.

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![NuGet Version](http://img.shields.io/nuget/v/Cake.MultiTarget.svg?style=flat)](https://www.nuget.org/packages/Cake.MultiTarget/)


## Dependencies

* Cake v0.19.5

## Usage

1. Reference the multitarget library:

```csharp
#addin "Cake.MultiTarget"
```

2. Instead of calling RunTarget at the end of your cake script call:

```csharp
RunMultiple.Targets(Task, RunTarget, Argument("target", "Default"));
```

3. When running your script you can now call multiple targets by passing them in as comma separated values:

```powershell
.\build.ps1 -Target "A,B"
```

## Example

```csharp
#addin "Cake.MultiTarget"

var target = Argument("target", "A");

Task("A")
    .Does(() =>
    {
        Information("A");
    });

Task("B")
    .Does(() =>
    {
        Information("B");
    });

RunMultiple.Targets(Task, RunTarget, target);
```

```powershell
.\build.ps1 -Target "A,B"
```

## General Notes

**This is an initial version and not tested thoroughly**.

I've made this package for use in my own cake scripts therefore they have only been tested on Windows. Use at your own risk :)
