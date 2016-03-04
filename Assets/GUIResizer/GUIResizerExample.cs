using UnityEngine;
using UnityEditor;
using System.Collections;

public class GUIResizerExample : EditorWindow
{

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/GUIResizerExample")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = GetWindow<GUIResizerExample>();
        window.titleContent = new GUIContent("GUI Resizer Example Window");
        window.Show();
    }

    private float boxWidth = 300f;
    private float bowHeight = 50f;

    void OnGUI()
    {
        GUIResizer.Update(this);

        GUILayout.Box("Drag the right side of the box to change its width.\n Drag the bottom side of the box to change its height.",
            GUILayout.Width(boxWidth), GUILayout.Height(bowHeight));

        Rect boxRect = GUILayoutUtility.GetLastRect();

        GUIResizer.Control(delegate (float x) { boxWidth += x; },
            new Rect(boxRect.xMax - 2.5f, boxRect.y, 5f, boxRect.height),
            ResizeDirection.Horizontal, this);

        GUIResizer.Control(delegate (float y) { bowHeight += y; },
            new Rect(boxRect.x, boxRect.yMax - 2.5f, boxRect.width, 5f),
            ResizeDirection.Vertical, this);
    }

}
