// Copyright 2026, BlueprintsLab, All rights reserved

using UnrealBuildTool;

public class UECPCore : ModuleRules
{
	public UECPCore(ReadOnlyTargetRules Target) : base(Target)
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
			"Slate",
			"SlateCore"
		});

		PrivateDependencyModuleNames.AddRange(new string[]
		{
			"Projects",
			"UnrealEd",
			"EditorFramework",
			"ToolMenus",
			"InputCore",
			"ApplicationCore",
			"DesktopPlatform",
			"Sockets",
			"Networking",
			"AssetRegistry",
			"AssetTools",
			"ContentBrowser",
			"ContentBrowserData",
			"WebBrowser",
			"WebBrowserWidget",

			// Describer infrastructure â€” UECPCore owns FBpGraphDescriber,
			// FBtGraphDescriber, FMaterialGraphDescriber, FMaterialNodeDescriber.
			// Shell @-mentions, Analyst summaries, and AnalysisTools all consume
			// these, so they live in Core to avoid duplication across modules.
			"BlueprintGraph", "Kismet", "GraphEditor",
			"AIModule", "AIGraph", "BehaviorTreeEditor",
			"MaterialEditor",
		});

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			PublicSystemLibraries.Add("Iphlpapi.lib");
		}
		if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			PublicFrameworks.AddRange(new string[] { "IOKit", "Foundation" });
		}
	}
}

