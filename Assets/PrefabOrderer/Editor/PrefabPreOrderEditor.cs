using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PrefabPreOrder))]
public class PrefabPreOrderEditor : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        EditorGUI.ObjectField(position, property.FindPropertyRelative("m_Prefab"), label);

        EditorGUI.EndProperty();
    }

}
