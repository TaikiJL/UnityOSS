﻿/*
﻿The MIT License (MIT)

Copyright (c) 2016 Taiki Hagiwara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

/// <summary>
/// Singleton base class which holds project-wide settings.
/// </summary>
/// <typeparam name="T">Type of the derived custom settings class.</typeparam>
public abstract class CustomProjectSettings<T> : ScriptableObject where T : CustomProjectSettings<T>
{

    private static T m_Instance = null;

    /// <summary>
    /// Returns the singleton instance of the settings class (creates one if none is found).
    /// </summary>
    protected static T instance
    {
        get
        {
            if (m_Instance == null)
            {
                string typeName = typeof(T).Name;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var asset = Resources.Load(typeName);
                    if (asset == null)
                    {
                        var paths = Directory.GetFiles(
                            "Assets/", "CustomProjectSettings.cs",
                            SearchOption.AllDirectories);
                        if (paths.Length > 1)
                        {
                            Debug.LogError(
                                "There are more than 1 file named \"CustomProjectSettings.cs\" in your project. Please use only 1.");
                            return null;
                        }
                        else if (paths.Length == 0)
                        {
                            Debug.LogError("There isn't any file named \"CustomProjectSettings.cs\" in your project. Please name the file containing the class \"CustomProjectSettings\" accordingly.");
                            return null;
                        }

                        string resourcesPath = Path.GetDirectoryName(paths[0]) + "/Resources/";
                        m_Instance = CreateInstance<T>();
                        if (!Directory.Exists(resourcesPath))
                            Directory.CreateDirectory(resourcesPath);
                        AssetDatabase.CreateAsset(m_Instance, resourcesPath + typeName + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        m_Instance = AssetDatabase.LoadAssetAtPath<T>(
                            AssetDatabase.GetAssetPath(asset));
                    }
                    return m_Instance;
                }
#endif
                m_Instance = Resources.Load<T>(typeName);
                if (m_Instance == null)
                {
                    Debug.LogErrorFormat(
                        "No settings file for \"{0}\" was found.",
                        typeName);
                    return null;
                }
            }
            return m_Instance;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// [EDITOR-ONLY] Selects the settings and makes it editable in the Unity Inspector.
    /// It is recommended to implement a function which calls this function and with the [MenuItem] attribute.
    /// </summary>
    public static void SelectSettings()
    {
        Selection.activeObject = instance;
    }
#endif

    /// <summary>
    /// Loads the settings file if it is not already loaded (otherwise the file would be loaded by the first call to "instance").
    /// </summary>
    public static void Load()
    {
        if (m_Instance != null)
            return;
        string typeName = typeof(T).Name;
        m_Instance = Resources.Load<T>(typeName);
        if (m_Instance == null)
        {
            Debug.LogErrorFormat(
                "No settings file for \"{0}\" was found.",
                typeName);
        }
    }

}
