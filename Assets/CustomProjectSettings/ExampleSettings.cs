using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExampleSettings : CustomProjectSettings<ExampleSettings>
{

#if UNITY_EDITOR
    [MenuItem("Settings/Example Settings")]
    static void Select()
    {
        SelectSettings();
    }
#endif

    [SerializeField]
    private float m_SomeFloatValue;

    public static float SomeFloatValue
    {
        get { return instance.m_SomeFloatValue; }
    }

}
