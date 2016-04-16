using UnityEngine;
using UnityEditor;

public class SplitterExample : EditorWindow
{

    [MenuItem("Window/Splitter Example")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = GetWindow<SplitterExample>();
        window.titleContent = new GUIContent("Splitter Example Window");
        window.Show();
    }

    private float boxWidth = 300f;
    private float boxHeight = 50f;

    void OnGUI()
    {
        using (var horizontalScope = new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Box(
                "Drag the right side of the box to change its width.\n Drag the bottom side of the box to change its height.",
                GUILayout.Width(boxWidth), GUILayout.Height(boxHeight));
            boxWidth = EditorGUILayoutExt.VerticalSplitter(2, boxWidth);
        }

        boxHeight = EditorGUILayoutExt.HorizontalSplitter(3, boxHeight);
    }

}
