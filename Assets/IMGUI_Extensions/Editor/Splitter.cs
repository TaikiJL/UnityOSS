/*
﻿The MIT License (MIT)

Copyright (c) 2016 Taiki Hagiwara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

https://github.com/TaikiJL/UnityOSS 
*/

using UnityEngine;

namespace UnityEditor
{

    /// <summary>
    /// Extensions to EditorGUI.
    /// </summary>
    public partial class EditorGUIExt
    {

        private static int m_SplitterId = -1;

        /// <summary>
        /// A horizontal splitter to resize the height of a GUI element.
        /// </summary>
        /// <param name="id">A unique ID to use for each splitter (horizontal or vertical).</param>
        /// <param name="position">Rectangle on the screen to use for the splitter.</param>
        /// <param name="value">The height of the GUI element to resize.</param>
        /// <returns>The modified value.</returns>
        public static float HorizontalSplitter(int id, Rect position, float value)
        {
            if (m_SplitterId >= 0)
            {
                if (id != m_SplitterId)
                    return value;

                if (Event.current.type == EventType.MouseUp)
                {
                    m_SplitterId = -1;
                    Event.current.Use();
                    return value;
                }

                Vector2 mousePosition = Event.current.mousePosition;
                Vector2 mouseDelta = Event.current.delta;
                EditorGUIUtility.AddCursorRect(
                        new Rect(mousePosition, mousePosition - mouseDelta),
                        MouseCursor.ResizeVertical);
                if (Event.current.type == EventType.MouseDrag)
                {
                    Event.current.Use();
                    return value + mouseDelta.y;
                }
            }
            else
            {
                EditorGUIUtility.AddCursorRect(position, MouseCursor.ResizeVertical);

                if (Event.current.type == EventType.MouseDown
                    && position.Contains(Event.current.mousePosition))
                {
                    m_SplitterId = id;
                }
            }

            return value;
        }

        /// <summary>
        /// A vertical splitter to resize the width of a GUI element.
        /// </summary>
        /// <param name="id">A unique ID to use for each splitter (horizontal or vertical).</param>
        /// <param name="position">Rectangle on the screen to use for the splitter.</param>
        /// <param name="value">The width of the GUI element to resize.</param>
        /// <returns>The modified value.</returns>
        public static float VerticalSplitter(int id, Rect position, float value)
        {
            if (m_SplitterId >= 0)
            {
                if (id != m_SplitterId)
                    return value;

                if (Event.current.type == EventType.MouseUp)
                {
                    m_SplitterId = -1;
                    Event.current.Use();
                    return value;
                }

                Vector2 mousePosition = Event.current.mousePosition;
                Vector2 mouseDelta = Event.current.delta;
                EditorGUIUtility.AddCursorRect(
                        new Rect(mousePosition, mousePosition + mouseDelta),
                        MouseCursor.ResizeHorizontal);
                if (Event.current.type == EventType.MouseDrag)
                {
                    Event.current.Use();
                    return value + mouseDelta.x;
                }
            }
            else
            {
                EditorGUIUtility.AddCursorRect(position, MouseCursor.ResizeHorizontal);

                if (Event.current.type == EventType.MouseDown
                    && position.Contains(Event.current.mousePosition))
                {
                    m_SplitterId = id;
                }
            }

            return value;
        }

    }

    /// <summary>
    /// Extensions to EditorGUILayout.
    /// </summary>
    public partial class EditorGUILayoutExt
    {

        /// <summary>
        /// A horizontal splitter to resize the height of the last GUILayout element.
        /// </summary>
        /// <param name="id">A unique ID to use for each splitter (horizontal or vertical).</param>
        /// <param name="value">The height of the GUILayout element to resize.</param>
        /// <returns>The modified value.</returns>
        public static float HorizontalSplitter(int id, float value)
        {
            Rect position = GUILayoutUtility.GetLastRect();

            return EditorGUIExt.HorizontalSplitter(id,
                new Rect(position.x, position.yMax - 5f, position.width, 10f),
                value);
        }

        /// <summary>
        /// A vertical splitter to resize the width of the last GUILayout element.
        /// In order to make this work correctly, this function should only be called from a Horizontal control group.
        /// </summary>
        /// <param name="id">A unique ID to use for each splitter (horizontal or vertical).</param>
        /// <param name="value">The width of the GUILayout element to resize.</param>
        /// <returns>The modified value.</returns>
        public static float VerticalSplitter(int id, float value)
        {
            Rect position = GUILayoutUtility.GetLastRect();

            return EditorGUIExt.VerticalSplitter(id,
                new Rect(position.xMax - 2.5f, position.y, 5f, position.height),
                value);
        }

    }

}
