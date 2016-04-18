/*
﻿The MIT License (MIT)

Copyright (c) 2016 Taiki Hagiwara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

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
