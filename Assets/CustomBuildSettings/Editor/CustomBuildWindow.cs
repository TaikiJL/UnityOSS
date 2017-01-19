using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomBuildWindow : ScriptableWizard
{

	private CustomBuildSettings m_Settings = null;

    [MenuItem("Build/Window")]
    static void CreateWizard()
    {
        var window = ScriptableWizard.DisplayWizard<CustomBuildWindow>("Custom Build Settings", "Build");
		if (window.m_Settings == null)
			window.m_Settings = CustomBuildSettings.Load();
    }

    void OnWizardCreate()
    {
		
    }

    void OnWizardUpdate()
    {
        helpString = "Please set the color of the light!";
    }

}
