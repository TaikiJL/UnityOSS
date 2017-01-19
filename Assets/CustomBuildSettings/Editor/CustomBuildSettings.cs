using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CustomBuildSettings : ScriptableObject
{

	[SerializeField]
	private string[] m_CustomSymbols;

	public static CustomBuildSettings Load()
	{
		string path = "Assets/EditorDefaultResources/CustomBuildSettings.asset";
		CustomBuildSettings settings;
		if (!File.Exists(path))
		{
			settings = CreateInstance<CustomBuildSettings>();
			AssetDatabase.CreateAsset(settings, path);
		}
		else
		{
			settings = AssetDatabase.LoadAssetAtPath<CustomBuildSettings>(path);
		}

		return settings;
	}

	public string[] symbols { get { return m_CustomSymbols; } }

}
