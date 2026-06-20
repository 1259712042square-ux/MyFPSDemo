// Copyright 2026, BlueprintsLab, All rights reserved

using UnrealBuildTool;

public class UECPFeedback : ModuleRules
{
	public UECPFeedback(ReadOnlyTargetRules Target) : base(Target)
	{
		bUsePrecompiled = true;
		if (Target.Type != TargetType.Editor)
		{
			return;
		}

		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		bUseUnity = false;

		PublicDependencyModuleNames.AddRange(new string[]
		{
			"Core",
			"CoreUObject",
			"Engine",
			"HTTP",
			"Json",
			"JsonUtilities",
			"UECPCore",
			"UECPTools",
			"UECPShell"
		});

		PrivateDependencyModuleNames.AddRange(new string[]
		{
			"Projects",
			"UnrealEd",
			"Slate",
			"SlateCore",
			"DesktopPlatform"
		});
	}
}

