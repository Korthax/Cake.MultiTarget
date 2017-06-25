var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = Argument("version", "*");

Task("Clean")
    .Does(() =>
    {
        CleanDirectories("./artifacts/**");
        CleanDirectories("./src/**/bin");
        CleanDirectories("./src/**/obj");
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var settings = new DotNetCoreRestoreSettings
        {
            Verbose = false
        };

        DotNetCoreRestore(settings);
    });

Task("BuildSource")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var settingsNet45 = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            Framework = "net45"
        };

        var settingsNetStandard = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            Framework = "netstandard1.6"
        };

        foreach(var file in GetFiles("./src/*/*.csproj"))
        {
            DotNetCoreBuild(file.ToString(), settingsNet45);
            DotNetCoreBuild(file.ToString(), settingsNetStandard);
        }
    });

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings
        {
            Configuration = "Release",
            OutputDirectory = "./artifacts/Cake.MultiTarget"
        };

        DotNetCorePack("./src/Cake.MultiTarget/Cake.MultiTarget.csproj", settings);
    });

Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);