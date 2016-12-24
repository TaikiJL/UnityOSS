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

    protected override void OnValidate()
    {
        base.OnValidate();

        Debug.Assert(shader != null,
            "Please assign \"UIVignetting.shader\" as the default shader of the UIVignetting script before creating one.");

        if (m_Material == null)
        {
            m_Material = new Material(shader);
        }

        UpdateMaterialParameters();

        raycastTarget = false;
    }

    protected override void Awake()
    {
        base.Awake();

        Debug.Assert(shader != null,
            "Please assign \"UIVignetting.shader\" as the default shader of the UIVignetting script before creating one.");

        if (m_Material == null)
        {
            m_Material = new Material(shader);
        }

        m_BorderDistanceID = Shader.PropertyToID("_BorderDistance");
        m_IntensityID = Shader.PropertyToID("_Intensity");

        UpdateMaterialParameters();

        raycastTarget = false;
    }

    void UpdateMaterialParameters()
    {
        if (m_Material != null)
        {
            float x = m_Border.x / rectTransform.rect.width;
            float y = m_Border.y / rectTransform.rect.height;
            x = (x - 0.5f) * 2f;
            y = (y - 0.5f) * 2f;
            Vector2 coord = new Vector2(x, y);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                m_Material.SetFloat("_BorderDistance", Vector2.Dot(coord, coord));
                m_Material.SetFloat("_Intensity", m_Intensity);
                return;
            }
#endif

            m_Material.SetFloat(m_BorderDistanceID, Vector2.Dot(coord, coord));
            m_Material.SetFloat(m_IntensityID, m_Intensity);
        }
    }

    protected override void OnRectTransformDimensionsChange()
    {
        UpdateMaterialParameters();

        base.OnRectTransformDimensionsChange();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = rectTransform.rect;

        float halfWidth = rect.width * 0.5f;
        float halfHeight = rect.height * 0.5f;

        float borderX = m_Border.x;
        float borderY = m_Border.y;

        float borderWidthRatio = m_Border.x / rect.width;
        float borderHeightRatio = m_Border.y / rect.height;

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

}
