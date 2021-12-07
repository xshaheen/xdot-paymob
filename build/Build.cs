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
    static AbsolutePath SourceDirectory => RootDirectory / "src";
    static AbsolutePath TestsDirectory => RootDirectory / "tests";

    static AbsolutePath ArtifactsDirectory => RootDirectory / ".artifacts";
    static AbsolutePath TestResults => ArtifactsDirectory / "test-results";

    Target Clean => _ => _
        .Description("Cleans the artifacts, bin and obj directories.")
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
        .Description("Runs unit tests and outputs test results to the artifacts directory.")
        .DependsOn(Compile)
        .Executes(() => {
            Solution.GetProjects("*.Tests.Unit.*").ForEach(project => {
                DotNetTest(settings => settings
                    .SetProjectFile(project)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild()
                    .EnableBlameCrash()
                    .SetDataCollector("XPlat Code Coverage")
                    .EnableCollectCoverage()
                    .SetResultsDirectory(TestResults)
                    .SetLoggers("console;verbosity=detailed", $"trx;LogFileName={project.Name}.trx", $"html;LogFileName={project.Name}.html")
                );
            });
        });

    Target Pack => _ => _
        .Description("Creates NuGet packages and outputs them to the artifacts directory.")
        .DependsOn(Test)
        .Executes(() => {
            DotNetPack(settings => settings
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .EnableIncludeSymbols()
                .SetOutputDirectory(ArtifactsDirectory)
                .SetContinuousIntegrationBuild(!IsLocalBuild)
            );
        });
}
