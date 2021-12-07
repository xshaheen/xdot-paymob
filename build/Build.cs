using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild {
    public static int Main() => Execute<Build>(build => build.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath TestResults => ArtifactsDirectory / "test-results";

    Target Clean => _ => _
        .Description("Cleans the artefacts, bin and obj directories.")
        .Before(Restore)
        .Executes(() => {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Description("Restores NuGet packages.")
        .Executes(() => {
            DotNetRestore(settings => settings.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .Description("Builds the solution.")
        .DependsOn(Restore)
        .Executes(() => {
            DotNetBuild(settings => settings
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
            );
        });

    Target Test => _ => _
        .Description("Runs unit tests and outputs test results to the artefacts directory.")
        .DependsOn(Compile)
        .Executes(() => {

            Solution.GetProjects("*.Tests.Unit").ForEach(project => { // "./tests/**/*.csproj"
                DotNetTest(settings => settings
                    .SetProjectFile(project)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild()
                    .EnableBlameMode()
                    .SetDataCollector("XPlat Code Coverage")
                    .EnableCollectCoverage()
                    .SetResultsDirectory(TestResults)
                    .SetLoggers($"trx;LogFileName={project.Name}.trx", $"html;LogFileName={project.Name}.html")
                );
            });
        });

// Task("Test")
//     .Description("Runs unit tests and outputs test results to the artefacts directory.")
//     .DoesForEach(GetFiles("./Tests/**/*.csproj"), project =>
//     {
//         DotNetTest(
//             project.ToString(),
//             new DotNetTestSettings()
//             {
//                 Blame = true,
//                 Collectors = new string[] { "XPlat Code Coverage" },
//                 Configuration = configuration,
//                 Loggers = new string[]
//                 {
//                     $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
//                     $"html;LogFileName={project.GetFilenameWithoutExtension()}.html",
//                 },
//                 NoBuild = true,
//                 NoRestore = true,
//                 ResultsDirectory = artefactsDirectory,
//             });
//     });

    // Target Pack => _ => _
    //     .DependsOn(Test)
    //     .Executes(() => {
    //         DotNetPack(settings => settings
    //             .SetProject(_solution)
    //             .SetConfiguration(_configuration)
    //             .EnableNoBuild()
    //             .EnableNoRestore()
    //             .SetOutputDirectory(ArtifactsDirectory)
    //             .SetVersionSuffix(GitVersion.NuGetVersionV2)
    //         );
    //     });
    //
    // Target Publish => _ => _
    //     .DependsOn(Test)
    //     .Executes(() => {
    //         DotNetPublish(settings => settings
    //             .SetProject(_solution)
    //             .SetConfiguration(_configuration)
    //             .EnableNoBuild()
    //             .EnableNoRestore()
    //             .SetOutputDirectory(ArtifactsDirectory)
    //             .SetVersionSuffix(GitVersion.NuGetVersionV2)
    //         );
    //     });
    //
    // Target Default => _ => _
    //     .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then creates NuGet packages.")
    // .DependsOn(Clean, Compile, Restore, Test, Pack);
}


// Task("Pack")
//     .Description("Creates NuGet packages and outputs them to the artefacts directory.")
//     .Does(() =>
//     {
//         DotNetPack(
//             ".",
//             new DotNetPackSettings()
//             {
//                 Configuration = configuration,
//                 IncludeSymbols = true,
//                 MSBuildSettings = new DotNetMSBuildSettings()
//                 {
//                     ContinuousIntegrationBuild = !BuildSystem.IsLocalBuild,
//                 },
//                 NoBuild = true,
//                 NoRestore = true,
//                 OutputDirectory = artefactsDirectory,
//             });
//     });
//
