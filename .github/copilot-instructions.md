# CommonHelpers Repository Instructions

## Repository structure

- `src/CommonHelpers` is the core `netstandard2.0` library and produces the `CommonHelpers` NuGet package.
- `src/CommonHelpers.Maui` is the .NET 10 MAUI companion package and consumes `CommonHelpers`.
- `src/CommonHelpers.Tests` is the .NET 10 MSTest project.
- GitHub Actions workflows run on Windows and use the .NET 10 SDK.

## Testing conventions

- The test project uses MSTest, `Microsoft.NET.Test.Sdk`, `GitHubActionsTestLogger`, and `coverlet.collector`.
- Keep `coverlet.collector` private to the test project. All workflows request the `XPlat Code Coverage` collector with OpenCover output.
- CI builds `CommonHelpers` before building the test project. Build the tests with:

  ```powershell
  dotnet restore --runtime any --ignore-failed-sources
  dotnet build -c Release --no-restore --no-dependencies
  ```

- Run the already-built tests with:

  ```powershell
  dotnet test -c Release --no-build --logger GitHubActions --blame-crash --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
  ```

- Do not add `--runtime any` to the `dotnet test --no-build` command. The test build produces `bin\Release\net10.0\CommonHelpers.Tests.dll`; specifying that runtime makes the test runner incorrectly look under `net10.0\any`.
- Keep `--no-dependencies` on the test build. Without it, the project reference rebuilds and repackages `CommonHelpers`, causing duplicate SBOM generation and manifest-deletion warnings.
- Cancellation tests should assert the public cancellation contract through `OperationCanceledException` unless an exact subtype is part of the API contract. Use deterministic canceled tasks such as `Task.FromCanceled<T>` rather than scheduling-dependent `Task.Run` cancellation.
- MSTest equality assertions always use expected value first and actual value second:

  ```csharp
  Assert.AreEqual(expected, actual);
  Assert.AreNotEqual(notExpected, actual);
  ```

## Package build dependencies

- Workflows remove `src/nuget.config` and add the newly built `src\CommonHelpers\bin\Release` directory as a package source.
- Before restoring `CommonHelpers.Maui`, explicitly set its `CommonHelpers` package reference to the version produced by the current workflow:

  ```powershell
  dotnet add package "CommonHelpers" --version "${{ steps.get-version.outputs.package-version }}"
  ```

- Do not rely on the package version range in `CommonHelpers.Maui.csproj` during CI. NuGet may otherwise select a stable public package instead of the alpha or release-candidate package built by the current run.
- Install the MAUI workload only once per build job.

## Artifact handling

- The NuGet artifact name is also the package filename, stored in `PKG_NAME` or `PKG_NAME_MAUI`.
- `actions/download-artifact@v8` exposes `download-path`, which is the destination directory. It does not expose `artifact-path`.
- Construct the full downloaded package path by appending the known filename:

  ```yaml
  "${{ steps.dl-commonhelpers-nupkg.outputs.download-path }}/${{ env.PKG_NAME }}"
  "${{ steps.dl-commonhelpers-maui-nupkg.outputs.download-path }}/${{ env.PKG_NAME_MAUI }}"
  ```

- Use the constructed full file path for existence checks, NuGet signing, artifact replacement, NuGet publishing, and GitHub release files.
- Quote package paths in YAML and PowerShell. Quotes protect valid expanded paths but cannot fix an invalid or nonexistent GitHub Actions output.
- `actions/download-artifact` extracts a single named artifact directly into the selected directory. Its output identifies that directory, not the extracted file.

## Release workflow requirements

- Keep shared build and test commands consistent across:
  - `.github/workflows/ci_main.yml`
  - `.github/workflows/ci_main-maui.yml`
  - `.github/workflows/cd_prerelease.yml`
  - `.github/workflows/cd_release.yml`
  - `.github/workflows/cd_release-maui.yml`
- Release jobs that publish to NuGet and create GitHub releases require:

  ```yaml
  permissions:
    contents: write
    id-token: write
  ```

- Use `NuGet/login` OIDC authentication rather than a long-lived NuGet API key.
- For combined prereleases, publish `CommonHelpers` before `CommonHelpers.Maui`.
- Preserve the existing self-hosted DigiCert signing flow and verify each downloaded package exists before signing.

## Workflow maintenance

- Apply fixes to every workflow that contains the shared operation; avoid allowing CI, prerelease, and release commands to drift.
- Prefer explicit package paths and versions over implicit discovery after artifacts cross job boundaries.
- When investigating a failure, inspect both the failed step and the preceding build output. Confirm the exact output directory before adding `--no-build`, runtime selectors, or artifact path expressions.
