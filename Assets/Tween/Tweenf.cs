/*
﻿The MIT License (MIT)

Copyright (c) 2016 Taiki Hagiwara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

https://github.com/TaikiJL/UnityOSS 
*/

using UnityEngine;

/// <summary>
/// Tween functions with various easings.
/// </summary>
public struct Tweenf
{

    public static float QuadraticEaseIn(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (b - a) * ct * ct + a;
    }

    public static float QuadraticEaseOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (a -b) * ct * (ct - 2f) + a;
    }

    public static float QuadraticEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (b - a) * 0.5f * ct * ct + a;
        --ct;
        return (a - b) * 0.5f * (ct * (ct - 2f) - 1f) + a;
    }

    public static float CubicEaseIn(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (b - a) * ct * ct * ct + a;
    }

    public static float CubicEaseOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) - 1f;
        return (b - a) * (ct * ct * ct + 1f) + a;
    }

    public static float CubicEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (b - a) * 0.5f * ct * ct * ct + a;
        ct -= 2f;
        return (b - a) * 0.5f * (ct * ct * ct + 2f) + a;
    }

    public static float QuarticEaseIn(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (b - a) * ct * ct * ct * ct + a;
    }

    public static float QuarticEaseOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) - 1f;
        return (a - b) * (ct * ct * ct * ct - 1f) + a;
    }

    public static float QuarticEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (b - a) * 0.5f * ct * ct * ct * ct + a;
        ct -= 2f;
        return (a - b) * 0.5f * (ct * ct * ct * ct - 2f) + a;
    }

    public static float QuinticEaseIn(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (b - a) * ct * ct * ct * ct * ct + a;
    }

    public static float QuinticEaseOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) - 1f;
        return (b - a) * (ct * ct * ct * ct * ct + 1f) + a;
    }

    public static float QuinticEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (b - a) * 0.5f * ct * ct * ct * ct * ct + a;
        ct -= 2f;
        return (b - a) * 0.5f * (ct * ct * ct * ct * ct + 2f) + a;
    }

    public static float SineEaseIn(float a, float b, float t)
    {
        float d = b - a;
        return -d * Mathf.Cos(Mathf.Clamp01(t) * Mathf.PI * 0.5f) + d + a;
    }

    public static float SineEaseOut(float a, float b, float t)
    {
        return (b - a) * Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI * 0.5f) + a;
    }

    public static float SineEaseInOut(float a, float b, float t)
    {
        return (a - b) * 0.5f * (Mathf.Cos(Mathf.Clamp01(t) * Mathf.PI) - 1f) + a;
    }

    public static float ExpoEaseIn(float a, float b, float t)
    {
        return (b - a) * Mathf.Pow(2f, 10f * (Mathf.Clamp01(t) - 1f)) + a;
    }

    public static float ExpoEaseOut(float a, float b, float t)
    {
        return (b - a) * (-Mathf.Pow(2f, -10f * (Mathf.Clamp01(t))) + 1f) + a;
    }

    public static float ExpoEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (b - a) * 0.5f * Mathf.Pow(2f, 10f * (ct - 1f)) + a;
        return (b - a) * 0.5f * (-Mathf.Pow(2f, -10f * (ct - 1)) + 2f) + a;
    }

    public static float CircularEaseIn(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t);
        return (a- b) * (Mathf.Sqrt(1f - ct * ct) - 1f) + a;
    }

    public static float CircularEaseOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) - 1f;
        return (b - a) * Mathf.Sqrt(1f - ct * ct) + a;
    }

    public static float CircularEaseInOut(float a, float b, float t)
    {
        float ct = Mathf.Clamp01(t) * 2f;
        if (ct < 1f)
            return (a -b) * 0.5f * (Mathf.Sqrt(1f - ct * ct) - 1f) + a;
        ct -= 2f;
        return (b - a) * 0.5f * (Mathf.Sqrt(1f - ct * ct) + 1f) + a;
    }

}
