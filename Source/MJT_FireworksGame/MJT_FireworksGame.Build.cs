// Fill out your copyright notice in the Description page of Project Settings.

using UnrealBuildTool;

public class MJT_FireworksGame : ModuleRules
{
	public MJT_FireworksGame(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
	
		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore" });

		PrivateDependencyModuleNames.AddRange(new string[] {  });

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });
		
		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}

private string ModulePath
{
	get { return ModuleDirectory; }
}

private string DiscordPath
{
	get { return Path.GetFullPath(Path.Combine(ModulePath, "..", "Discord")); }
}

private string DiscordSDKDLLName
{
	get { return "discord_game_sdk"; }
}

private void AddDiscordLibPathWindows(ReadOnlyTargetRules Target)
{
	string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "x86_64" : "x86";
	
	string dll = Path.Combine(DiscordPath, lib, PlatformString, DiscordSDKDLLName + ".dll");
	
	PublicAdditionalLibraries.Add(dll + ".lib"));
	RuntimeDependencies.Add(dll));
}

private void AddDiscordLibPathMac(ReadOnlyTargetRules Target)
{
	string dll = Path.Combine(DiscordPath, lib, "Mac", DiscordSDKDLLName + ".bundle");
	RuntimeDependencies.Add(dll));
}

public bool LoadDiscordSDK(ReadOnlyTargetRules Target)
{
    if ((Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetnPlatform.Win32))
    {
		PublicDefinitions.Add("_EG_WINDOWS_PLATFORM");
		AddDiscordLibPathWindows(Target);
    }
	else if (Target.Platform == UnrealTargetPlatform.Mac)
    {
		PublicDefinitions.Add("_EG_IMAC_PLATFORM");
		AddDiscordLibPathMac(Target);
    }
	else
    {
		throw new Exception("\nTarget platform not supported: " + Target.Platform);
    }

	PublicIncludePaths.Add(Paths.Combine(DiscordPath, "includes"));

	return true;
}
