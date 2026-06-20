// Copyright 2026, BlueprintsLab, All rights reserved

using UnrealBuildTool;

public class UECPGeometryScriptExt : ModuleRules
{
	public UECPGeometryScriptExt(ReadOnlyTargetRules Target) : base(Target)
	{
		bUsePrecompiled = true;
		if (Target.Type != TargetType.Editor) return;
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		bUseUnity = false;

		PublicDependencyModuleNames.AddRange(new string[]
		{
			"Core", "CoreUObject", "Engine", "Json", "JsonUtilities",
			"UECPCore", "UECPTools",
		});

		PrivateDependencyModuleNames.AddRange(new string[]
		{
			"UnrealEd", "AssetTools", "AssetRegistry", "EditorScriptingUtilities",
			"GeometryScriptingCore", "GeometryFramework",
		});
	}
}

