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
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Tween functions with various easings.
/// </summary>
public struct Tweenf
{

	#region Clamped

	public static float QuadraticEaseIn(float a, float b, float t)
	{
		float ct = Mathf.Clamp01(t);
		return (b - a) * ct * ct + a;
	}

	public static float QuadraticEaseOut(float a, float b, float t)
	{
		float ct = Mathf.Clamp01(t);
		return (a - b) * ct * (ct - 2f) + a;
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
		return (a - b) * (Mathf.Sqrt(1f - ct * ct) - 1f) + a;
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
			return (a - b) * 0.5f * (Mathf.Sqrt(1f - ct * ct) - 1f) + a;
		ct -= 2f;
		return (b - a) * 0.5f * (Mathf.Sqrt(1f - ct * ct) + 1f) + a;
	}

	#endregion

	#region Unclamped

	public static float QuadraticEaseInUnclamped(float a, float b, float t)
	{
		return (b - a) * t * t + a;
	}

	public static float QuadraticEaseOutUnclamped(float a, float b, float t)
	{
		return (a - b) * t * (t - 2f) + a;
	}

	public static float QuadraticEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (b - a) * 0.5f * t * t + a;
		--t;
		return (a - b) * 0.5f * (t * (t - 2f) - 1f) + a;
	}

	public static float CubicEaseInUnclamped(float a, float b, float t)
	{
		return (b - a) * t * t * t + a;
	}

	public static float CubicEaseOutUnclamped(float a, float b, float t)
	{
		t -= 1f;
		return (b - a) * (t * t * t + 1f) + a;
	}

	public static float CubicEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (b - a) * 0.5f * t * t * t + a;
		t -= 2f;
		return (b - a) * 0.5f * (t * t * t + 2f) + a;
	}

	public static float QuarticEaseInUnclamped(float a, float b, float t)
	{
		return (b - a) * t * t * t * t + a;
	}

	public static float QuarticEaseOutUnclamped(float a, float b, float t)
	{
		t -= 1f;
		return (a - b) * (t * t * t * t - 1f) + a;
	}

	public static float QuarticEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (b - a) * 0.5f * t * t * t * t + a;
		t -= 2f;
		return (a - b) * 0.5f * (t * t * t * t - 2f) + a;
	}

	public static float QuinticEaseInUnclamped(float a, float b, float t)
	{
		return (b - a) * t * t * t * t * t + a;
	}

	public static float QuinticEaseOutUnclamped(float a, float b, float t)
	{
		t -= 1f;
		return (b - a) * (t * t * t * t * t + 1f) + a;
	}

	public static float QuinticEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (b - a) * 0.5f * t * t * t * t * t + a;
		t -= 2f;
		return (b - a) * 0.5f * (t * t * t * t * t + 2f) + a;
	}

	public static float SineEaseInUnclamped(float a, float b, float t)
	{
		float d = b - a;
		return -d * Mathf.Cos(t * Mathf.PI * 0.5f) + d + a;
	}

	public static float SineEaseOutUnclamped(float a, float b, float t)
	{
		return (b - a) * Mathf.Sin(t * Mathf.PI * 0.5f) + a;
	}

	public static float SineEaseInOutUnclamped(float a, float b, float t)
	{
		return (a - b) * 0.5f * (Mathf.Cos(t * Mathf.PI) - 1f) + a;
	}

	public static float ExpoEaseInUnclamped(float a, float b, float t)
	{
		return (b - a) * Mathf.Pow(2f, 10f * (t - 1f)) + a;
	}

	public static float ExpoEaseOutUnclamped(float a, float b, float t)
	{
		return (b - a) * (-Mathf.Pow(2f, -10f * t) + 1f) + a;
	}

	public static float ExpoEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (b - a) * 0.5f * Mathf.Pow(2f, 10f * (t - 1f)) + a;
		return (b - a) * 0.5f * (-Mathf.Pow(2f, -10f * (t - 1)) + 2f) + a;
	}

	public static float CircularEaseInUnclamped(float a, float b, float t)
	{
		return (a - b) * (Mathf.Sqrt(1f - t * t) - 1f) + a;
	}

	public static float CircularEaseOutUnclamped(float a, float b, float t)
	{
		t -= 1f;
		return (b - a) * Mathf.Sqrt(1f - t * t) + a;
	}

	public static float CircularEaseInOutUnclamped(float a, float b, float t)
	{
		t *= 2f;
		if (t < 1f)
			return (a - b) * 0.5f * (Mathf.Sqrt(1f - t * t) - 1f) + a;
		t -= 2f;
		return (b - a) * 0.5f * (Mathf.Sqrt(1f - t * t) + 1f) + a;
	}

	#endregion

	#region Helpers

    public delegate float TweenFunc(float a, float b, float t);

    private static TweenFunc _quadraticEaseIn;
    private static TweenFunc _quadraticEaseOut;
    private static TweenFunc _quadraticEaseInOut;
    private static TweenFunc _cubicEaseIn;
    private static TweenFunc _cubicEaseOut;
    private static TweenFunc _cubicEaseInOut;
    private static TweenFunc _quarticEaseIn;
    private static TweenFunc _quarticEaseOut;
    private static TweenFunc _quarticEaseInOut;
    private static TweenFunc _quinticEaseIn;
    private static TweenFunc _quinticEaseOut;
    private static TweenFunc _quinticEaseInOut;
    private static TweenFunc _sineEaseIn;
    private static TweenFunc _sineEaseOut;
    private static TweenFunc _sineEaseInOut;
    private static TweenFunc _expoEaseIn;
    private static TweenFunc _expoEaseOut;
    private static TweenFunc _expoEaseInOut;
    private static TweenFunc _circularEaseIn;
    private static TweenFunc _circularEaseOut;
    private static TweenFunc _circularEaseInOut;

    private static TweenFunc _quadraticEaseInUnclamped;
    private static TweenFunc _quadraticEaseOutUnclamped;
    private static TweenFunc _quadraticEaseInOutUnclamped;
    private static TweenFunc _cubicEaseInUnclamped;
    private static TweenFunc _cubicEaseOutUnclamped;
    private static TweenFunc _cubicEaseInOutUnclamped;
    private static TweenFunc _quarticEaseInUnclamped;
    private static TweenFunc _quarticEaseOutUnclamped;
    private static TweenFunc _quarticEaseInOutUnclamped;
    private static TweenFunc _quinticEaseInUnclamped;
    private static TweenFunc _quinticEaseOutUnclamped;
    private static TweenFunc _quinticEaseInOutUnclamped;
    private static TweenFunc _sineEaseInUnclamped;
    private static TweenFunc _sineEaseOutUnclamped;
    private static TweenFunc _sineEaseInOutUnclamped;
    private static TweenFunc _expoEaseInUnclamped;
    private static TweenFunc _expoEaseOutUnclamped;
    private static TweenFunc _expoEaseInOutUnclamped;
    private static TweenFunc _circularEaseInUnclamped;
    private static TweenFunc _circularEaseOutUnclamped;
    private static TweenFunc _circularEaseInOutUnclamped;

    public static TweenFunc GetTweenFunction(TweenfType tweenType, bool clamped = true)
    {
        if (clamped)
        {
            switch (tweenType)
            {
                case TweenfType.QuadraticEaseIn:
                    return _quadraticEaseIn ?? (_quadraticEaseIn = new TweenFunc(QuadraticEaseIn));
                case TweenfType.QuadraticEaseOut:
                    return _quadraticEaseOut ?? (_quadraticEaseOut = new TweenFunc(QuadraticEaseOut));
                case TweenfType.QuadraticEaseInOut:
                    return _quadraticEaseInOut ?? (_quadraticEaseInOut = new TweenFunc(QuadraticEaseInOut));
                case TweenfType.CubicEaseIn:
                    return _cubicEaseIn ?? (_cubicEaseIn = new TweenFunc(CubicEaseIn));
                case TweenfType.CubicEaseOut:
                    return _cubicEaseOut ?? (_cubicEaseOut = new TweenFunc(CubicEaseOut));
                case TweenfType.CubicEaseInOut:
                    return _cubicEaseInOut ?? (_cubicEaseInOut = new TweenFunc(CubicEaseInOut));
                case TweenfType.QuarticEaseIn:
                    return _quarticEaseIn ?? (_quarticEaseIn = new TweenFunc(QuarticEaseIn));
                case TweenfType.QuarticEaseOut:
                    return _quarticEaseOut ?? (_quarticEaseOut = new TweenFunc(QuarticEaseOut));
                case TweenfType.QuarticEaseInOut:
                    return _quarticEaseInOut ?? (_quarticEaseInOut = new TweenFunc(QuarticEaseInOut));
                case TweenfType.QuinticEaseIn:
                    return _quinticEaseIn ?? (_quinticEaseIn = new TweenFunc(QuinticEaseIn));
                case TweenfType.QuinticEaseOut:
                    return _quinticEaseOut ?? (_quinticEaseOut = new TweenFunc(QuinticEaseOut));
                case TweenfType.QuinticEaseInOut:
                    return _quinticEaseInOut ?? (_quinticEaseInOut = new TweenFunc(QuinticEaseInOut));
                case TweenfType.SineEaseIn:
                    return _sineEaseIn ?? (_sineEaseIn = new TweenFunc(SineEaseIn));
                case TweenfType.SineEaseOut:
                    return _sineEaseOut ?? (_sineEaseOut = new TweenFunc(SineEaseOut));
                case TweenfType.SineEaseInOut:
                    return _sineEaseInOut ?? (_sineEaseInOut = new TweenFunc(SineEaseInOut));
                case TweenfType.ExpoEaseIn:
                    return _expoEaseIn ?? (_expoEaseIn = new TweenFunc(ExpoEaseIn));
                case TweenfType.ExpoEaseOut:
                    return _expoEaseOut ?? (_expoEaseOut = new TweenFunc(ExpoEaseOut));
                case TweenfType.ExpoEaseInOut:
                    return _expoEaseInOut ?? (_expoEaseInOut = new TweenFunc(ExpoEaseInOut));
                case TweenfType.CircularEaseIn:
                    return _circularEaseIn ?? (_circularEaseIn = new TweenFunc(CircularEaseIn));
                case TweenfType.CircularEaseOut:
                    return _circularEaseOut ?? (_circularEaseOut = new TweenFunc(CircularEaseOut));
                case TweenfType.CircularEaseInOut:
                    return _circularEaseInOut ?? (_circularEaseInOut = new TweenFunc(CircularEaseInOut));
                default: return Mathf.Lerp;
            }
        }
        else
        {
            switch (tweenType)
            {
                case TweenfType.QuadraticEaseIn:
                    return _quadraticEaseInUnclamped ?? (_quadraticEaseInUnclamped = new TweenFunc(QuadraticEaseInUnclamped));
                case TweenfType.QuadraticEaseOut:
                    return _quadraticEaseOutUnclamped ?? (_quadraticEaseOutUnclamped = new TweenFunc(QuadraticEaseOutUnclamped));
                case TweenfType.QuadraticEaseInOut:
                    return _quadraticEaseInOutUnclamped ?? (_quadraticEaseInOutUnclamped = new TweenFunc(QuadraticEaseInOutUnclamped));
                case TweenfType.CubicEaseIn:
                    return _cubicEaseInUnclamped ?? (_cubicEaseInUnclamped = new TweenFunc(CubicEaseInUnclamped));
                case TweenfType.CubicEaseOut:
                    return _cubicEaseOutUnclamped ?? (_cubicEaseOutUnclamped = new TweenFunc(CubicEaseOutUnclamped));
                case TweenfType.CubicEaseInOut:
                    return _cubicEaseInOutUnclamped ?? (_cubicEaseInOutUnclamped = new TweenFunc(CubicEaseInOutUnclamped));
                case TweenfType.QuarticEaseIn:
                    return _quarticEaseInUnclamped ?? (_quarticEaseInUnclamped = new TweenFunc(QuarticEaseInUnclamped));
                case TweenfType.QuarticEaseOut:
                    return _quarticEaseOutUnclamped ?? (_quarticEaseOutUnclamped = new TweenFunc(QuarticEaseOutUnclamped));
                case TweenfType.QuarticEaseInOut:
                    return _quarticEaseInOutUnclamped ?? (_quarticEaseInOutUnclamped = new TweenFunc(QuarticEaseInOutUnclamped));
                case TweenfType.QuinticEaseIn:
                    return _quinticEaseInUnclamped ?? (_quinticEaseInUnclamped = new TweenFunc(QuinticEaseInUnclamped));
                case TweenfType.QuinticEaseOut:
                    return _quinticEaseOutUnclamped ?? (_quinticEaseOutUnclamped = new TweenFunc(QuinticEaseOutUnclamped));
                case TweenfType.QuinticEaseInOut:
                    return _quinticEaseInOutUnclamped ?? (_quinticEaseInOutUnclamped = new TweenFunc(QuinticEaseInOutUnclamped));
                case TweenfType.SineEaseIn:
                    return _sineEaseInUnclamped ?? (_sineEaseInUnclamped = new TweenFunc(SineEaseInUnclamped));
                case TweenfType.SineEaseOut:
                    return _sineEaseOutUnclamped ?? (_sineEaseOutUnclamped = new TweenFunc(SineEaseOutUnclamped));
                case TweenfType.SineEaseInOut:
                    return _sineEaseInOutUnclamped ?? (_sineEaseInOutUnclamped = new TweenFunc(SineEaseInOutUnclamped));
                case TweenfType.ExpoEaseIn:
                    return _expoEaseInUnclamped ?? (_expoEaseInUnclamped = new TweenFunc(ExpoEaseInUnclamped));
                case TweenfType.ExpoEaseOut:
                    return _expoEaseOutUnclamped ?? (_expoEaseOutUnclamped = new TweenFunc(ExpoEaseOutUnclamped));
                case TweenfType.ExpoEaseInOut:
                    return _expoEaseInOutUnclamped ?? (_expoEaseInOutUnclamped = new TweenFunc(ExpoEaseInOutUnclamped));
                case TweenfType.CircularEaseIn:
                    return _circularEaseInUnclamped ?? (_circularEaseInUnclamped = new TweenFunc(CircularEaseInUnclamped));
                case TweenfType.CircularEaseOut:
                    return _circularEaseOutUnclamped ?? (_circularEaseOutUnclamped = new TweenFunc(CircularEaseOutUnclamped));
                case TweenfType.CircularEaseInOut:
                    return _circularEaseInOutUnclamped ?? (_circularEaseInOutUnclamped = new TweenFunc(CircularEaseInOutUnclamped));
                default: return Mathf.LerpUnclamped;
            }
        }
    }

    public static IEnumerable<float> EnumberableTween(float duration,
        TweenfType tweenType,
        bool unclamped = false,
        bool ignoreTimeScale = false)
    {
        float dInverse = 1f / duration;
        var tweenFunc = Tweenf.GetTweenFunction(tweenType, !unclamped);
        if (!ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
                yield return tweenFunc(0f, 1f, t * dInverse);
            yield return 1f;
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
                yield return tweenFunc(0f, 1f, t * dInverse);
            yield return 1f;
        }
    }

	#endregion

}

public enum TweenfType
{
	QuadraticEaseIn = 0,
	QuadraticEaseOut,
	QuadraticEaseInOut,
	CubicEaseIn,
	CubicEaseOut,
	CubicEaseInOut,
	QuarticEaseIn,
	QuarticEaseOut,
	QuarticEaseInOut,
	QuinticEaseIn,
	QuinticEaseOut,
	QuinticEaseInOut,
	SineEaseIn,
	SineEaseOut,
	SineEaseInOut,
	ExpoEaseIn,
	ExpoEaseOut,
	ExpoEaseInOut,
	CircularEaseIn,
	CircularEaseOut,
	CircularEaseInOut
}

public static class TweenfExtensions
{

    public static void CrossFadeColor(this Graphic graphic,
        Color targetColor,
        float duration,
        bool ignoreTimeScale,
        bool useAlpha,
        TweenfType tweenType)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, false);
        graphic.StartCoroutine(CrossFadeColor(graphic, targetColor, duration, ignoreTimeScale, useAlpha, tweenf));
    }

    private static IEnumerator CrossFadeColor(Graphic graphic,
        Color targetColor,
        float duration,
        bool ignoreTimeScale,
        bool useAlpha,
        Tweenf.TweenFunc tweenFunc)
    {
        Color startColor = graphic.color;
        if (!useAlpha)
            targetColor.a = startColor.a;
        float dInverse = 1f / duration;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                float a = tweenFunc(0f, 1f, t * dInverse);
                graphic.color = Color.LerpUnclamped(startColor, targetColor, a);
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                float a = tweenFunc(0f, 1f, t * dInverse);
                graphic.color = Color.LerpUnclamped(startColor, targetColor, a);
                yield return null;
            }
        }
        graphic.color = targetColor;
    }

    public static void CrossFadeAlpha(this Graphic graphic,
        float alpha,
        float duration,
        bool ignoreTimeScale,
        TweenfType tweenType)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, false);
        graphic.StartCoroutine(CrossFadeAlpha(graphic, alpha, duration, ignoreTimeScale, tweenf));
    }

    private static IEnumerator CrossFadeAlpha(Graphic graphic,
        float alpha,
        float duration,
        bool ignoreTimeScale,
        Tweenf.TweenFunc tweenFunc)
    {
        float initialAlpha = graphic.color.a;
        Color color = graphic.color;
        float dInverse = 1f / duration;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                color.a = tweenFunc(initialAlpha, alpha, t * dInverse);
                graphic.color = color;
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                color.a = tweenFunc(initialAlpha, alpha, t * dInverse);
                graphic.color = color;
                yield return null;
            }
        }
        color.a = alpha;
        graphic.color = color;
    }

    public static IEnumerator Move(this Transform transform,
        Vector3 targetPosition,
        float duration,
        TweenfType tweenType,
        bool unclamped = false,
        bool ignoreTimeScale = false)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, !unclamped);
        float dInverse = 1f / duration;
        Vector3 initialPosition = transform.position;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        transform.position = targetPosition;
    }

    public static IEnumerator MoveLocal(this Transform transform,
        Vector3 targetPosition,
        float duration,
        TweenfType tweenType,
        bool unclamped = false,
        bool ignoreTimeScale = false)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, !unclamped);
        float dInverse = 1f / duration;
        Vector3 initialPosition = transform.localPosition;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.localPosition = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.localPosition = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        transform.position = targetPosition;
    }

    public static IEnumerator MoveAnchoredPosition(this RectTransform transform,
        Vector2 targetPosition,
        float duration,
        TweenfType tweenType,
        bool unclamped = false,
        bool ignoreTimeScale = false)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, !unclamped);
        float dInverse = 1f / duration;
        Vector2 initialPosition = transform.anchoredPosition;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.anchoredPosition = Vector2.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.anchoredPosition = Vector2.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        transform.anchoredPosition = targetPosition;
    }

    public static IEnumerator MoveAnchoredPosition3D(this RectTransform transform,
        Vector3 targetPosition,
        float duration,
        TweenfType tweenType,
        bool unclamped = false,
        bool ignoreTimeScale = false)
    {
        var tweenf = Tweenf.GetTweenFunction(tweenType, !unclamped);
        float dInverse = 1f / duration;
        Vector3 initialPosition = transform.anchoredPosition3D;
        if (ignoreTimeScale)
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.anchoredPosition3D = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.deltaTime)
            {
                float a = tweenf(0f, 1f, t * dInverse);
                transform.anchoredPosition3D = Vector3.LerpUnclamped(initialPosition, targetPosition, a);
                yield return null;
            }
        }
        transform.anchoredPosition3D = targetPosition;
    }

}
