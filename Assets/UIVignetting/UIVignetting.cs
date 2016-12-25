/*
﻿The MIT License (MIT)
Copyright (c) 2016 Taiki Hagiwara
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
https://github.com/TaikiJL/UnityOSS 
*/

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI component to make a vignetting effect. It is designed to not overdraw the central zone by using a holed mesh.
/// </summary>
[AddComponentMenu("UI/Vignetting", 50)]
public class UIVignetting : Graphic
{

    [HideInInspector]
    public Shader shader;

    [SerializeField]
    private Vector2 m_Border = new Vector2(50f, 50f);
    [SerializeField, Range(0f, 5f)]
    private float m_Intensity = 1f;

    private int m_BorderDistanceID;
    private int m_IntensityID;

    /// <summary>
    /// Intensity of the vignetting effect.
    /// </summary>
    public float intensity
    {
        get { return m_Intensity; }
        set
        {
            if (value != m_Intensity)
            {
                m_Intensity = Mathf.Max(0f, value);
                UpdateMaterialParameters();
            }
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Editor-only! Do not use it!
    /// </summary>
    [System.NonSerialized]
    public bool showBorder = false;

    protected override void Reset()
    {
        base.Reset();

        raycastTarget = false;
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        ClampBorderSize();
        UpdateMaterialParameters();
    }
#endif

    protected override void Awake()
    {
        base.Awake();

        m_BorderDistanceID = Shader.PropertyToID("_BorderDistance");
        m_IntensityID = Shader.PropertyToID("_Intensity");

        UpdateMaterialParameters();

        raycastTarget = false;
    }

    void UpdateMaterialParameters()
    {
        if (m_Material == null)
        {
#if UNITY_EDITOR
            if (shader == null)
            {
                shader = Shader.Find("Hidden/UIVignetting");
                Debug.Assert(shader != null, "The \"UIVignetting\" shader could not be found in the project.");
            }
#endif

            m_Material = new Material(shader);
        }

        Rect rect = rectTransform.rect;
        float x = m_Border.x / rect.width;
        float y = m_Border.y / rect.height;
        x = (x - 0.5f) * 2f;
        y = (y - 0.5f) * 2f;
        float dotCoord = x * x + y * y;

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            m_Material.SetFloat("_BorderDistance", showBorder ? 0f : dotCoord);
            m_Material.SetFloat("_Intensity", showBorder ? 1 : m_Intensity);
            return;
        }
#endif

        m_Material.SetFloat(m_BorderDistanceID, dotCoord);
        m_Material.SetFloat(m_IntensityID, m_Intensity);
    }

    void ClampBorderSize()
    {
        Rect rect = rectTransform.rect;
        m_Border.x = Mathf.Clamp(m_Border.x, 0f, rect.width * 0.5f);
        m_Border.y = Mathf.Clamp(m_Border.y, 0f, rect.height * 0.5f);
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = rectTransform.rect;

        float halfWidth = rect.width * 0.5f;
        float halfHeight = rect.height * 0.5f;

        float borderX = m_Border.x;
        float borderY = m_Border.y;

        float borderWidthRatio = borderX / rect.width;
        float borderHeightRatio = borderY / rect.height;

        // Outer vertices
        vh.AddVert(new Vector3(-halfWidth, -halfHeight, 0f), color, new Vector2(0f, 0f));
        vh.AddVert(new Vector3(-halfWidth, halfHeight, 0f), color, new Vector2(0f, 1f));
        vh.AddVert(new Vector3(halfWidth, halfHeight, 0f), color, new Vector2(1f, 1f));
        vh.AddVert(new Vector3(halfWidth, -halfHeight, 0f), color, new Vector2(1f, 0f));

        // Inner vertices
        vh.AddVert(new Vector3(-halfWidth + borderX, -halfHeight + borderY, 0f), color, new Vector2(borderWidthRatio, borderHeightRatio));
        vh.AddVert(new Vector3(-halfWidth + borderX, halfHeight - borderY, 0f), color, new Vector2(borderWidthRatio, 1f - borderHeightRatio));
        vh.AddVert(new Vector3(halfWidth - borderX, halfHeight - borderY, 0f), color, new Vector2(1f - borderWidthRatio, 1f - borderHeightRatio));
        vh.AddVert(new Vector3(halfWidth - borderX, -halfHeight + borderY, 0f), color, new Vector2(1f - borderWidthRatio, borderHeightRatio));

        // Left side
        vh.AddTriangle(0, 1, 5);
        vh.AddTriangle(5, 4, 0);

        // Lower side
        vh.AddTriangle(0, 4, 3);
        vh.AddTriangle(3, 4, 7);

        // Right side
        vh.AddTriangle(3, 7, 2);
        vh.AddTriangle(2, 7, 6);

        // Upper side
        vh.AddTriangle(6, 5, 2);
        vh.AddTriangle(2, 5, 1);
    }

    protected override void OnRectTransformDimensionsChange()
    {
        ClampBorderSize();
        UpdateMaterialParameters();

        base.OnRectTransformDimensionsChange();
    }

}
