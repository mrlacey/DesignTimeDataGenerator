using DesignTimeDataGenerator.Generator;

namespace DesignTimeDataGenerator.UnitTests;

[UsesVerify]
public class AxamlGeneratorSnapshotTests
{
	[Fact]
	public Task SimpleStringsWithStringFormat()
	{
		var sourceToTest = @"
<Window xmlns=""https://github.com/avaloniaui""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        x:DataType=""SomeDataType""
        mc:Ignorable=""d"" d:DesignWidth=""900"" d:DesignHeight=""700""
        x:Class=""MainWindow""
        MinWidth=""600"" MinHeight=""500"">
    <StackPanel >
      <TextBlock Text=""{Binding ServiceTitle}"" />
      <TextBlock Text=""{Binding ServerName, StringFormat='with {0}'}"" />
      <TextBlock Text=""{Binding ServiceDateTime, StringFormat={}{0:dd MMM yyyy}}"" />
      <TextBlock Text=""{Binding ServiceDateTime, StringFormat={}{0:HH:mm}}"" />
      <TextBlock Text=""{Binding ServicePrice, StringFormat={}{0:£0.00}}"" />
      <TextBlock Text=""{Binding Description, StringFormat={}{0:£0.00}}"" />
    </StackPanel>
</Window>
";

		return TestHelper<AxamlGenerator>.VerifyAxaml(sourceToTest);
	}
}
