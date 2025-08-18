# DesignTimeDataGenerator

This NuGet package contains an incremental Roslyn source generator that automatically includes all `.axaml` files in a consuming project as MSBuild `AdditionalFiles` and generates a partial class for each `.axaml` file.

What it does now
- Adds `**/*.axaml` to `AdditionalFiles` when the package is referenced (via `build/DesignTimeDataGenerator.targets`).
- For each `.axaml` file, generates a `public partial class <FileName>` inside namespace `DesignTimeDataGenerator.Generated`.
- The generated file contains a small header comment with the source path and the number of lines in the `.axaml`.

How to consume
1. Add this package to your project (from NuGet or local feed).
2. Add `.axaml` files anywhere in the project (they will be included as AdditionalFiles automatically).
3. Build the project. Generated sources will appear during compile.

Notes
- Class names are sanitized to valid C# identifiers.
- This initial version only emits an empty partial class and line-count comment; future versions will add richer generation.

License: MIT
