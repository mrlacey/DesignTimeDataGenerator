using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace DesignTimeDataGenerator.UnitTests;

public static class TestHelper<T> where T : IIncrementalGenerator, new()
{
	public static Task VerifyAxaml(string source)
	{
		var csharp = "namespace IntegrationTesting { public class SomeClass {} }";

		// Parse the provided string into a C# syntax tree
		SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(csharp);

		// Create references for assemblies we require
		IEnumerable<PortableExecutableReference> references = new[]
		{
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
		};

		// Create a Roslyn compilation for the syntax tree.
		CSharpCompilation compilation = CSharpCompilation.Create(
			assemblyName: "Tests",
			syntaxTrees: [syntaxTree],
			references: references);

		var generator = new T();

		// Prepare AdditionalText inputs for the generator
		var additionalTexts = new[] { new InMemoryAdditionalText("MainWindow.axaml", source) };

		// The GeneratorDriver is used to run our generator against a compilation
		GeneratorDriver driver = CSharpGeneratorDriver.Create(
			generators: [generator.AsSourceGenerator()],
			additionalTexts: additionalTexts);

		// Run the source generator!
		driver = driver.RunGenerators(compilation);

		// Use verify to snapshot test the source generator output!
		return Verifier.Verify(driver).UseDirectory("Snapshots");
	}
}


public sealed class InMemoryAdditionalText : AdditionalText
{
	private readonly SourceText _text;

	public InMemoryAdditionalText(string path, string content, Encoding? encoding = null)
	{
		Path = path;
		_text = SourceText.From(content, encoding ?? Encoding.UTF8);
	}

	public override string Path { get; }

	public override SourceText GetText(System.Threading.CancellationToken cancellationToken = default) => _text;
}