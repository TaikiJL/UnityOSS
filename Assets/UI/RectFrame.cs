/*
﻿The MIT License (MIT)
Copyright (c) 2016 Taiki Hagiwara
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
https://github.com/TaikiJL/UnityOSS 
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Frame border for a parent Graphic object.
/// </summary>
public class RectFrame : Graphic
{

    [SerializeField]
    private Color m_EffectColor = new Color(0f, 0f, 0f, 1f);

    [SerializeField]
    private Vector2 m_EffectDistance = new Vector2(1f, 1f);

    [SerializeField]
    private bool m_UseGraphicAlpha = true;

    private const float kMaxEffectDistance = 600f;

    private Graphic m_ParentGraphic;
    private UnityAction m_SetVerticesDirty;

    protected RectFrame()
    { }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        effectDistance = m_EffectDistance;
        base.OnValidate();
    }

#endif

    public Color effectColor
    {
        get { return m_EffectColor; }
        set
        {
            m_EffectColor = value;
            SetVerticesDirty();
        }
    }

    public Vector2 effectDistance
    {
        get { return m_EffectDistance; }
        set
        {
            if (value.x > kMaxEffectDistance)
                value.x = kMaxEffectDistance;
            if (value.x < -kMaxEffectDistance)
                value.x = -kMaxEffectDistance;

            if (value.y > kMaxEffectDistance)
                value.y = kMaxEffectDistance;
            if (value.y < -kMaxEffectDistance)
                value.y = -kMaxEffectDistance;

            if (m_EffectDistance == value)
                return;

            m_EffectDistance = value;

            SetVerticesDirty();
        }
    }

    public bool useGraphicAlpha
    {
        get { return m_UseGraphicAlpha; }
        set
        {
            m_UseGraphicAlpha = value;
            SetVerticesDirty();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        m_SetVerticesDirty = SetVerticesDirty;

        GetAdopted();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(CheckParentChange());
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        vh.Clear();

        Rect rect = m_ParentGraphic.rectTransform.rect;

        float halfWidth = rect.width * 0.5f;
        float halfHeight = rect.height * 0.5f;

        float borderX = m_EffectDistance.x;
        float borderY = m_EffectDistance.y;

        float borderWidthRatio = borderX / rect.width;
        float borderHeightRatio = borderY / rect.height;

        Color32 color = m_EffectColor;
        if (m_UseGraphicAlpha)
            color.a = (byte)(m_ParentGraphic.color.a * 255f);

        // Inner vertices
        vh.AddVert(new Vector3(-halfWidth, -halfHeight, 0f),    color, new Vector2(borderWidthRatio, borderHeightRatio));
        vh.AddVert(new Vector3(-halfWidth, halfHeight, 0f),     color, new Vector2(borderWidthRatio, 1f - borderHeightRatio));
        vh.AddVert(new Vector3(halfWidth, halfHeight, 0f),      color, new Vector2(1f - borderWidthRatio, 1f - borderHeightRatio));
        vh.AddVert(new Vector3(halfWidth, -halfHeight, 0f),     color, new Vector2(1f - borderWidthRatio, borderHeightRatio));

        // Outer vertices
        vh.AddVert(new Vector3(-halfWidth - borderX, -halfHeight - borderY, 0f),    color, new Vector2(0f, 0f));
        vh.AddVert(new Vector3(-halfWidth - borderX, halfHeight + borderY, 0f),     color, new Vector2(0f, 1f));
        vh.AddVert(new Vector3(halfWidth + borderX, halfHeight + borderY, 0f),      color, new Vector2(1f, 1f));
        vh.AddVert(new Vector3(halfWidth + borderX, -halfHeight - borderY, 0f),     color, new Vector2(1f, 0f));

        // Left side
        vh.AddTriangle(0, 4, 5);
        vh.AddTriangle(0, 5, 1);

        // Lower side
        vh.AddTriangle(0, 3, 4);
        vh.AddTriangle(3, 7, 4);

        // Right side
        vh.AddTriangle(3, 6, 7);
        vh.AddTriangle(3, 2, 6);

        // Upper side
        vh.AddTriangle(1, 6, 2);
        vh.AddTriangle(1, 5, 6);
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();

        GetAdopted();
    }

    private void GetAdopted()
    {
        m_ParentGraphic = transform.parent.GetComponent<Graphic>();
    }

    private IEnumerator CheckParentChange()
    {
        RectTransform parent = m_ParentGraphic.rectTransform;
        Vector2 previousSize = Vector2.zero;
        float previousAlpha = -1f;

        while (enabled)
        {
            Rect rect = parent.rect;
            if (rect.width != previousSize.x
                || rect.height != previousSize.y
                || m_ParentGraphic.color.a != previousAlpha)
            {
                SetVerticesDirty();
            }
            previousSize = rect.size;
            previousAlpha = m_ParentGraphic.color.a;
            yield return null;
        }
    }

}
