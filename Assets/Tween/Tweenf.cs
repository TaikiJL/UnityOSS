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

    // To do: lazy initialization

    private static readonly TweenFunc QUADRATIC_EASE_IN         = new TweenFunc(QuadraticEaseIn);
    private static readonly TweenFunc QUADRATIC_EASE_OUT        = new TweenFunc(QuadraticEaseOut);
    private static readonly TweenFunc QUADRATIC_EASE_IN_OUT     = new TweenFunc(QuadraticEaseInOut);
    private static readonly TweenFunc CUBIC_EASE_IN             = new TweenFunc(CubicEaseIn);
    private static readonly TweenFunc CUBIC_EASE_OUT            = new TweenFunc(CubicEaseOut);
    private static readonly TweenFunc CUBIC_EASE_IN_OUT         = new TweenFunc(CubicEaseInOut);
    private static readonly TweenFunc QUARTIC_EASE_IN           = new TweenFunc(QuarticEaseIn);
    private static readonly TweenFunc QUARTIC_EASE_OUT          = new TweenFunc(QuarticEaseOut);
    private static readonly TweenFunc QUARTIC_EASE_IN_OUT       = new TweenFunc(QuarticEaseInOut);
    private static readonly TweenFunc QUINTIC_EASE_IN           = new TweenFunc(QuinticEaseIn);
    private static readonly TweenFunc QUINTIC_EASE_OUT          = new TweenFunc(QuinticEaseOut);
    private static readonly TweenFunc QUINTIC_EASE_IN_OUT       = new TweenFunc(QuinticEaseInOut);
    private static readonly TweenFunc SINE_EASE_IN              = new TweenFunc(SineEaseIn);
    private static readonly TweenFunc SINE_EASE_OUT             = new TweenFunc(SineEaseOut);
    private static readonly TweenFunc SINE_EASE_IN_OUT          = new TweenFunc(SineEaseInOut);
    private static readonly TweenFunc EXPO_EASE_IN              = new TweenFunc(ExpoEaseIn);
    private static readonly TweenFunc EXPO_EASE_OUT             = new TweenFunc(ExpoEaseOut);
    private static readonly TweenFunc EXPO_EASE_IN_OUT          = new TweenFunc(ExpoEaseInOut);
    private static readonly TweenFunc CIRCULAR_EASE_IN          = new TweenFunc(CircularEaseIn);
    private static readonly TweenFunc CIRCULAR_EASE_OUT         = new TweenFunc(CircularEaseOut);
    private static readonly TweenFunc CIRCULAR_EASE_IN_OUT      = new TweenFunc(CircularEaseInOut);

    private static readonly TweenFunc QUADRATIC_EASE_IN_UNCLAMPED           = new TweenFunc(QuadraticEaseInUnclamped);
    private static readonly TweenFunc QUADRATIC_EASE_OUT_UNCLAMPED          = new TweenFunc(QuadraticEaseOutUnclamped);
    private static readonly TweenFunc QUADRATIC_EASE_IN_OUT_UNCLAMPED       = new TweenFunc(QuadraticEaseInOutUnclamped);
    private static readonly TweenFunc CUBIC_EASE_IN_UNCLAMPED               = new TweenFunc(CubicEaseInUnclamped);
    private static readonly TweenFunc CUBIC_EASE_OUT_UNCLAMPED              = new TweenFunc(CubicEaseOutUnclamped);
    private static readonly TweenFunc CUBIC_EASE_IN_OUT_UNCLAMPED           = new TweenFunc(CubicEaseInOutUnclamped);
    private static readonly TweenFunc QUARTIC_EASE_IN_UNCLAMPED             = new TweenFunc(QuarticEaseInUnclamped);
    private static readonly TweenFunc QUARTIC_EASE_OUT_UNCLAMPED            = new TweenFunc(QuarticEaseOutUnclamped);
    private static readonly TweenFunc QUARTIC_EASE_IN_OUT_UNCLAMPED         = new TweenFunc(QuarticEaseInOutUnclamped);
    private static readonly TweenFunc QUINTIC_EASE_IN_UNCLAMPED             = new TweenFunc(QuinticEaseInUnclamped);
    private static readonly TweenFunc QUINTIC_EASE_OUT_UNCLAMPED            = new TweenFunc(QuinticEaseOutUnclamped);
    private static readonly TweenFunc QUINTIC_EASE_IN_OUT_UNCLAMPED         = new TweenFunc(QuinticEaseInOutUnclamped);
    private static readonly TweenFunc SINE_EASE_IN_UNCLAMPED                = new TweenFunc(SineEaseInUnclamped);
    private static readonly TweenFunc SINE_EASE_OUT_UNCLAMPED               = new TweenFunc(SineEaseOutUnclamped);
    private static readonly TweenFunc SINE_EASE_IN_OUT_UNCLAMPED            = new TweenFunc(SineEaseInOutUnclamped);
    private static readonly TweenFunc EXPO_EASE_IN_UNCLAMPED                = new TweenFunc(ExpoEaseInUnclamped);
    private static readonly TweenFunc EXPO_EASE_OUT_UNCLAMPED               = new TweenFunc(ExpoEaseOutUnclamped);
    private static readonly TweenFunc EXPO_EASE_IN_OUT_UNCLAMPED            = new TweenFunc(ExpoEaseInOutUnclamped);
    private static readonly TweenFunc CIRCULAR_EASE_IN_UNCLAMPED            = new TweenFunc(CircularEaseInUnclamped);
    private static readonly TweenFunc CIRCULAR_EASE_OUT_UNCLAMPED           = new TweenFunc(CircularEaseOutUnclamped);
    private static readonly TweenFunc CIRCULAR_EASE_IN_OUT_UNCLAMPED        = new TweenFunc(CircularEaseInOutUnclamped);

    public static TweenFunc GetTweenFunction(TweenfType tweenType, bool clamped = true)
    {
        if (clamped)
        {
            switch (tweenType)
            {
                case TweenfType.QuadraticEaseIn:        return QUADRATIC_EASE_IN;
                case TweenfType.QuadraticEaseOut:       return QUADRATIC_EASE_OUT;
                case TweenfType.QuadraticEaseInOut:     return QUADRATIC_EASE_IN_OUT;
                case TweenfType.CubicEaseIn:            return CUBIC_EASE_IN;
                case TweenfType.CubicEaseOut:           return CUBIC_EASE_OUT;
                case TweenfType.CubicEaseInOut:         return CUBIC_EASE_IN_OUT;
                case TweenfType.QuarticEaseIn:          return QUARTIC_EASE_IN;
                case TweenfType.QuarticEaseOut:         return QUARTIC_EASE_OUT; 
                case TweenfType.QuarticEaseInOut:       return QUARTIC_EASE_IN_OUT;
                case TweenfType.QuinticEaseIn:          return QUINTIC_EASE_IN;
                case TweenfType.QuinticEaseOut:         return QUINTIC_EASE_OUT;
                case TweenfType.QuinticEaseInOut:       return QUINTIC_EASE_IN_OUT;
                case TweenfType.SineEaseIn:             return SINE_EASE_IN;       
                case TweenfType.SineEaseOut:            return SINE_EASE_OUT;    
                case TweenfType.SineEaseInOut:          return SINE_EASE_IN_OUT;
                case TweenfType.ExpoEaseIn:             return EXPO_EASE_IN;       
                case TweenfType.ExpoEaseOut:            return EXPO_EASE_OUT;    
                case TweenfType.ExpoEaseInOut:          return EXPO_EASE_IN_OUT;    
                case TweenfType.CircularEaseIn:         return CIRCULAR_EASE_IN;   
                case TweenfType.CircularEaseOut:        return CIRCULAR_EASE_OUT;
                case TweenfType.CircularEaseInOut:      return CIRCULAR_EASE_IN_OUT;
                default: return Mathf.Lerp;
            }
        }
        else
        {
            switch (tweenType)
            {
                case TweenfType.QuadraticEaseIn:        return QUADRATIC_EASE_IN_UNCLAMPED;
                case TweenfType.QuadraticEaseOut:       return QUADRATIC_EASE_OUT_UNCLAMPED;
                case TweenfType.QuadraticEaseInOut:     return QUADRATIC_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.CubicEaseIn:            return CUBIC_EASE_IN_UNCLAMPED;
                case TweenfType.CubicEaseOut:           return CUBIC_EASE_OUT_UNCLAMPED;
                case TweenfType.CubicEaseInOut:         return CUBIC_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.QuarticEaseIn:          return QUARTIC_EASE_IN_UNCLAMPED;
                case TweenfType.QuarticEaseOut:         return QUARTIC_EASE_OUT_UNCLAMPED;
                case TweenfType.QuarticEaseInOut:       return QUARTIC_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.QuinticEaseIn:          return QUINTIC_EASE_IN_UNCLAMPED;
                case TweenfType.QuinticEaseOut:         return QUINTIC_EASE_OUT_UNCLAMPED;
                case TweenfType.QuinticEaseInOut:       return QUINTIC_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.SineEaseIn:             return SINE_EASE_IN_UNCLAMPED;
                case TweenfType.SineEaseOut:            return SINE_EASE_OUT_UNCLAMPED;
                case TweenfType.SineEaseInOut:          return SINE_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.ExpoEaseIn:             return EXPO_EASE_IN_UNCLAMPED;
                case TweenfType.ExpoEaseOut:            return EXPO_EASE_OUT_UNCLAMPED;
                case TweenfType.ExpoEaseInOut:          return EXPO_EASE_IN_OUT_UNCLAMPED;
                case TweenfType.CircularEaseIn:         return CIRCULAR_EASE_IN_UNCLAMPED;
                case TweenfType.CircularEaseOut:        return CIRCULAR_EASE_OUT_UNCLAMPED;
                case TweenfType.CircularEaseInOut:      return CIRCULAR_EASE_IN_OUT_UNCLAMPED;
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
            {
                yield return tweenFunc(0f, 1f, t * dInverse);
            }
            yield return 1f;
        }
        else
        {
            for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
            {
                yield return tweenFunc(0f, 1f, t * dInverse);
            }
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
        bool ignoreTimeScale,
        TweenfType tweenType,
        bool unclamped = false)
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
        bool ignoreTimeScale,
        TweenfType tweenType,
        bool unclamped = false)
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

    public static IEnumerator Move(this RectTransform transform,
        Vector2 targetPosition,
        float duration,
        bool ignoreTimeScale,
        TweenfType tweenType,
        bool unclamped = false)
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

    public static IEnumerator Move(this RectTransform transform,
        Vector3 targetPosition,
        float duration,
        bool ignoreTimeScale,
        TweenfType tweenType,
        bool unclamped = false)
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
