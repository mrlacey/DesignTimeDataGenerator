using System.Runtime.CompilerServices;

namespace DesignTimeDataGenerator.UnitTests;

public static class ModuleInitializer
{
	[ModuleInitializer]
	public static void Init()
	{
		VerifySourceGenerators.Enable();
	}
}
