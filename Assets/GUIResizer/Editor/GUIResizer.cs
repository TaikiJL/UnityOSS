using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

/// <summary>
/// Direction in which the GUIResizer resize (horizontal or vertical).
/// </summary>
public enum ResizeDirection
{
    Horizontal,
    Vertical
}

/// <summary>
/// Utility to make resize controls for GUI elements in an EditorWindow.
/// </summary>
public class GUIResizer
{

    private static IEnumerator m_Coroutine = null;
    private static EditorWindow m_Window = null;

    private GUIResizer() { }

    /// <summary>
    /// Resizing control for a GUI element.
    /// </summary>
    /// <param name="resizeFunc">The function in which the size variable should be modified.</param>
    /// <param name="mouseZone">The rectangle zone in which the resizing can begin.</param>
    /// <param name="direction">The direction of the resizing (horizontal or vertical)</param>
    /// <param name="window">The editor window which holds the GUI element to resize.</param>
	public static void Control(Action<float> resizeFunc, Rect mouseZone, ResizeDirection direction, EditorWindow window)
    {
        EditorGUIUtility.AddCursorRect(mouseZone,
            direction == ResizeDirection.Horizontal ? MouseCursor.ResizeHorizontal : MouseCursor.ResizeVertical);

        if (Event.current.type == EventType.MouseDown
            && mouseZone.Contains(Event.current.mousePosition))
        {
            m_Window = window;
            m_Coroutine = Resize(resizeFunc, direction, window);
        }
            
    }

    /// <summary>
    /// Resizing splitter control for a GUI element.
    /// </summary>
    /// <param name="resizeFunc">The function in which the size variable should be modified.</param>
    /// <param name="direction">The direction of the resizing (horizontal or vertical)</param>
    /// <param name="window">The editor window which holds the GUI element to resize.</param>
    public static void Control(Action<float> resizeFunc, ResizeDirection direction, EditorWindow window)
    {
        Rect lastControlRect = GUILayoutUtility.GetLastRect();
        GUILayout.Space(4f);
        Rect spaceRect = GUILayoutUtility.GetLastRect();
        if (direction == ResizeDirection.Horizontal)
            spaceRect.height = lastControlRect.height;
        else
            spaceRect.width = lastControlRect.width;
        Color prevColor = GUI.color;
        if (EditorGUIUtility.isProSkin)
            GUI.color = new Color32(41, 41, 41, 255);
        else
            GUI.color = new Color32(86, 86, 86, 255);
        GUI.DrawTexture(spaceRect, EditorGUIUtility.whiteTexture);
        GUI.color = prevColor;

        Control(resizeFunc, spaceRect, direction, window);
    }

    /// <summary>
    /// Update the resizing (must be called from an EditorWindow's OnGUI).
    /// </summary>
    /// <param name="window">The EditorWindow calling the function.</param>
    public static void Update(EditorWindow window)
    {
        if (m_Coroutine == null || m_Window != window)
            return;

        if (m_Coroutine.MoveNext() == false)
        {
            m_Coroutine = null;
            m_Window = null;
        }
    }

    private static IEnumerator Resize(Action<float> resizeFunc, ResizeDirection direction, EditorWindow window)
    {
        MouseCursor mc = direction == ResizeDirection.Horizontal ? MouseCursor.ResizeHorizontal : MouseCursor.ResizeVertical;
        while (Event.current.type != EventType.MouseUp)
        {
            var md = Event.current.delta;
            if (Event.current.type == EventType.MouseDrag)
                resizeFunc(direction == ResizeDirection.Horizontal ? md.x : md.y);
            EditorGUIUtility.AddCursorRect(new Rect(0f, 0f, Screen.width, Screen.height), mc);
            window.Repaint();
            yield return null;
        }
    }

}
