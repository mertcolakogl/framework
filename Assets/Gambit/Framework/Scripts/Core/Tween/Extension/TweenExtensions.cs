// TweenExtensions.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Gambit.Framework.Scripts.Core.Tween.Extension
{
    public static class TweenExtensions
    {
        private static TweenBuilder StaticFromStaticTo<T1, T2>(T1 obj, T2 from, T2 to, Action<T2> e,
            Func<T2, T2, float, T2> lerpFunc)
        {
            var tweenBuilder = TweenBase.Create().OnTweenAction((t, ease) => { e.Invoke(lerpFunc.Invoke(from, to, ease)); });
            return tweenBuilder;
        }

        private static TweenBuilder DynamicFromStaticTo<T1, T2>(T1 obj, Func<T2> fromFunc, T2 to, Action<T2> e,
            Func<T2, T2, float, T2> lerpFunc)
        {
            var isFromInit = false;
            var from = default(T2);
            var tweenBuilder = TweenBase.Create().OnTweenAction((t, ease) =>
            {
                if (!isFromInit)
                {
                    isFromInit = true;
                    from = fromFunc.Invoke();
                }

                e.Invoke(lerpFunc.Invoke(from, to, ease));
            });
            return tweenBuilder;
        }

        private static TweenBuilder DynamicFromDynamicTo<T1, T2>(T1 obj, Func<T2> fromFunc, Func<T2> toFunc,
            Action<T2> e, Func<T2, T2, float, T2> lerpFunc)
        {
            var oldEase = 0f;
            var tweenBuilder = TweenBase.Create().OnTweenAction((t, ease) =>
            {
                if (!(t > 0)) oldEase = ease;
                e.Invoke(1f - oldEase != 0f
                    ? lerpFunc.Invoke(fromFunc.Invoke(), toFunc.Invoke(), (ease - oldEase) / (1f - oldEase))
                    : lerpFunc.Invoke(fromFunc.Invoke(), toFunc.Invoke(), ease));
                oldEase = ease;
            });

            return tweenBuilder;
        }

        /// <summary>
        ///     Transform move. From and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenMove(this Transform t, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(t, from, to, p => t.position = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform move. From is declared as dynamic at the beginning, and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenMove(this Transform t, Vector3 to)
        {
            return DynamicFromStaticTo(t, () => t.position, to, p => t.position = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform move dynamically. If from and to position changing in tween, use this function.
        ///     But this func is more expensive than TweenMove()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenMoveDynamic(this Transform t, Func<Vector3> fromFunc, Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, p => t.position = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local move. From and To declared as static localPosition
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalMove(this Transform t, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(t, from, to, p => t.localPosition = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local move. From is declared as dynamic at the beginning, and To declared as static localPosition
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalMove(this Transform t, Vector3 to)
        {
            return DynamicFromStaticTo(t, () => t.localPosition, to, p => t.localPosition = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local move dynamically. If from and to localPosition changing in tween, use this function.
        ///     But this func is more expensive than TweenLocalMove()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalMoveDynamic(this Transform t, Func<Vector3> fromFunc, Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, lp => t.localPosition = lp, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform rotate. From and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotate(this Transform t, Quaternion from, Quaternion to)
        {
            return StaticFromStaticTo(t, from, to, r => t.rotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform rotate. From and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotate(this Transform t, Quaternion to)
        {
            return DynamicFromStaticTo(t, () => t.rotation, to, r => t.rotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform rotate dynamically. If from and to Rotation changing in tween, use this function.
        ///     But this func is more expensive than TweenRotate()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotateDynamic(this Transform t, Func<Quaternion> fromFunc, Func<Quaternion> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, r => t.rotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform local rotate. From and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotate(this Transform t, Quaternion from, Quaternion to)
        {
            return StaticFromStaticTo(t, from, to, r => t.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform local rotate. From and To declared as static position
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotate(this Transform t, Quaternion to)
        {
            return DynamicFromStaticTo(t, () => t.localRotation, to, r => t.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform local rotate dynamically. If from and to Rotation changing in tween, use this function.
        ///     But this func is more expensive than TweenLocalRotate()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotateDynamic(this Transform t, Func<Quaternion> fromFunc, Func<Quaternion> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, r => t.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     Transform euler angles rotation. From and To declared as static eulerAngles
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotateEuler(this Transform t, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(t, from, to, r => t.eulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform euler angles rotation. From is declared as dynamic at the beginning, and To declared as static
        ///     eulerAngles
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotateEuler(this Transform t, Vector3 to)
        {
            return DynamicFromStaticTo(t, () => t.eulerAngles, to, r => t.eulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform euler angles rotation dynamically. If from and to eulerAngles changing in tween, use this function.
        ///     But this func is more expensive than TweenRotateEuler()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotateEulerDynamic(this Transform t, Func<Vector3> fromFunc, Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, r => t.eulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local euler angles rotation. From and To declared as static localEulerAngles
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotateEuler(this Transform t, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(t, from, to, r => t.localEulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local euler angles rotation. From is declared as dynamic at the beginning, and To declared as static
        ///     localEulerAngles
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotateEuler(this Transform t, Vector3 to)
        {
            return DynamicFromStaticTo(t, () => t.localEulerAngles, to, r => t.localEulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform local euler angles rotation dynamically. If from and to localEulerAngles changing in tween, use this
        ///     function.
        ///     But this func is more expensive than TweenLocalRotateEuler()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenLocalRotateEulerDynamic(this Transform t, Func<Vector3> fromFunc,
            Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, r => t.localEulerAngles = r, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform localScale. From and To declared as static localScale
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenScale(this Transform t, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(t, from, to, s => t.localScale = s, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform localScale.  From is declared as dynamic at the beginning, and To declared as static localScale
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenScale(this Transform t, Vector3 to)
        {
            return DynamicFromStaticTo(t, () => t.localScale, to, ls => t.localScale = ls, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     Transform localScale dynamically. If from and to localScale changing in tween, use this function.
        ///     But this func is more expensive than TweenScale()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenScaleDynamic(this Transform t, Func<Vector3> fromFunc, Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(t, fromFunc, toFunc, ls => t.localScale = ls, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position. From and To declared as static anchoredPosition
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMove(this RectTransform rt, Vector2 from, Vector2 to)
        {
            return StaticFromStaticTo(rt, from, to, p => rt.anchoredPosition = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position. From is declared as dynamic at the beginning, and To declared as static
        ///     anchoredPosition
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMove(this RectTransform rt, Vector2 to)
        {
            return DynamicFromStaticTo(rt, () => rt.anchoredPosition, to, p => rt.anchoredPosition = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position dynamically. If from and to anchoredPosition changing in tween, use this function.
        ///     But this func is more expensive than TweenAnchoredMove()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMoveDynamic(this RectTransform rt, Func<Vector2> fromFunc, Func<Vector2> toFunc)
        {
            return DynamicFromDynamicTo(rt, fromFunc, toFunc, p => rt.anchoredPosition = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position 3D. From and To declared as static anchoredPosition3D
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMove3D(this RectTransform rt, Vector3 from, Vector3 to)
        {
            return StaticFromStaticTo(rt, from, to, p => rt.anchoredPosition3D = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position 3D. From is declared as dynamic at the beginning, and To declared as static
        ///     anchoredPosition3D
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMove3D(this RectTransform rt, Vector3 to)
        {
            return DynamicFromStaticTo(rt, () => rt.anchoredPosition3D, to, p => rt.anchoredPosition3D = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform anchored position 3D dynamically. If from and to anchoredPosition3D changing in tween, use this
        ///     function.
        ///     But this func is more expensive than TweenAnchoredMove3D()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAnchoredMove3DDynamic(this RectTransform rt, Func<Vector3> fromFunc, Func<Vector3> toFunc)
        {
            return DynamicFromDynamicTo(rt, fromFunc, toFunc, p => rt.anchoredPosition3D = p, Vector3.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform pivot. From and To declared as static pivot
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenPivot(this RectTransform rt, Vector2 from, Vector2 to)
        {
            return StaticFromStaticTo(rt, from, to, p => rt.pivot = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform pivot. From is declared as dynamic at the beginning, and To declared as static pivot
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenPivot(this RectTransform rt, Vector2 to)
        {
            return DynamicFromStaticTo(rt, () => rt.pivot, to, p => rt.pivot = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform pivot dynamically. If from and to pivot changing in tween, use this function.
        ///     But this func is more expensive than TweenPivot()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenPivotDynamic(this RectTransform rt, Func<Vector2> fromFunc, Func<Vector2> toFunc)
        {
            return DynamicFromDynamicTo(rt, fromFunc, toFunc, p => rt.pivot = p, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform local rotation. From and To declared as static localRotation
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotate(this RectTransform rt, Quaternion from, Quaternion to)
        {
            return StaticFromStaticTo(rt, from, to, r => rt.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     RectTransform local rotation. From is declared as dynamic at the beginning, and To declared as static localRotation
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotate(this RectTransform rt, Quaternion to)
        {
            return DynamicFromStaticTo(rt, () => rt.localRotation, to, r => rt.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     RectTransform local rotation dynamically. If from and to localRotation changing in tween, use this function.
        ///     But this func is more expensive than TweenRotate()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenRotateDynamic(this RectTransform rt, Func<Quaternion> fromFunc, Func<Quaternion> toFunc)
        {
            return DynamicFromDynamicTo(rt, fromFunc, toFunc, r => rt.localRotation = r, Quaternion.SlerpUnclamped);
        }

        /// <summary>
        ///     RectTransform size delta. From and To declared as static sizeDelta
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSizeDelta(this RectTransform rt, Vector2 from, Vector2 to)
        {
            return StaticFromStaticTo(rt, from, to, s => rt.sizeDelta = s, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform size delta. From is declared as dynamic at the beginning, and To declared as static sizeDelta
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSizeDelta(this RectTransform rt, Vector2 to)
        {
            return DynamicFromStaticTo(rt, () => rt.sizeDelta, to, s => rt.sizeDelta = s, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     RectTransform size delta dynamically. If from and to sizeDelta changing in tween, use this function.
        ///     But this func is more expensive than TweenSizeDelta()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSizeDeltaDynamic(this RectTransform rt, Func<Vector2> fromFunc, Func<Vector2> toFunc)
        {
            return DynamicFromDynamicTo(rt, fromFunc, toFunc, s => rt.sizeDelta = s, Vector2.LerpUnclamped);
        }

        /// <summary>
        ///     Material color. From and To declared as static color
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColor(this Material m, Color from, Color to)
        {
            return StaticFromStaticTo(m, from, to, c => m.color = c, Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material color. From is declared as dynamic at the beginning, and To declared as static color
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColor(this Material m, Color to)
        {
            return DynamicFromStaticTo(m, () => m.color, to, c => m.color = c, Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material color dynamically. If from and to color changing in tween, use this function.
        ///     But this func is more expensive than TweenColor()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColorDynamic(this Material m, Func<Color> fromFunc, Func<Color> toFunc)
        {
            return DynamicFromDynamicTo(m, fromFunc, toFunc, c => m.color = c, Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material color by property name. From and To declared as static color
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColor(this Material m, string propertyName, Color from, Color to)
        {
            return StaticFromStaticTo(m, from, to, c => m.SetColor(propertyName, c), Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material color by property name. From is declared as dynamic at the beginning, and To declared as static color
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColor(this Material m, string propertyName, Color to)
        {
            return DynamicFromStaticTo(m, () => m.GetColor(propertyName), to, c => m.SetColor(propertyName, c), Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material color by property name dynamically. If from and to color changing in tween, use this function.
        ///     But this func is more expensive than TweenColor()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenColorDynamic(this Material m, string propertyName, Func<Color> fromFunc, Func<Color> toFunc)
        {
            return DynamicFromDynamicTo(m, fromFunc, toFunc, c => m.SetColor(propertyName, c), Color.LerpUnclamped);
        }

        /// <summary>
        ///     Material alpha. From and To declared as static alpha
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAlpha(this Material m, float from, float to)
        {
            return StaticFromStaticTo(m, from, to, a =>
            {
                var c = m.color;
                c.a = a;
                m.color = c;
            }, Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Material alpha. From is declared as dynamic at the beginning, and To declared as static alpha
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAlpha(this Material m, float to)
        {
            return DynamicFromStaticTo(m, () => m.color.a, to, a =>
            {
                var c = m.color;
                c.a = a;
                m.color = c;
            }, Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Material alpha dynamically. If from and to alpha changing in tween, use this function.
        ///     But this func is more expensive than TweenAlpha()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenAlphaDynamic(this Material m, Func<float> fromFunc, Func<float> toFunc)
        {
            return DynamicFromDynamicTo(m, fromFunc, toFunc, a =>
            {
                var c = m.color;
                c.a = a;
                m.color = c;
            }, Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Material float property. From and To declared as static float
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenFloat(this Material m, string propertyName, float from, float to)
        {
            return StaticFromStaticTo(m, from, to, f => m.SetFloat(propertyName, f), Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Material float property. From is declared as dynamic at the beginning, and To declared as static float
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenFloat(this Material m, string propertyName, float to)
        {
            return DynamicFromStaticTo(m, () => m.GetFloat(propertyName), to, f => m.SetFloat(propertyName, f), Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Material float property dynamically. If from and to float changing in tween, use this function.
        ///     But this func is more expensive than TweenFloat()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenFloatDynamic(this Material m, string propertyName, Func<float> fromFunc, Func<float> toFunc)
        {
            return DynamicFromDynamicTo(m, fromFunc, toFunc, f => m.SetFloat(propertyName, f), Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Slider value. From and To declared as static value
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSlider(this Slider s, float from, float to)
        {
            return StaticFromStaticTo(s, from, to, v => s.value = v, Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Slider value. From is declared as dynamic at the beginning, and To declared as static value
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSlider(this Slider s, float to)
        {
            return DynamicFromStaticTo(s, () => s.value, to, v => s.value = v, Mathf.LerpUnclamped);
        }

        /// <summary>
        ///     Slider value dynamically. If from and to value changing in tween, use this function.
        ///     But this func is more expensive than TweenSlider()
        ///     Note: If you use this extension, do not modify tween with SetTweenAction()
        /// </summary>
        public static TweenBuilder TweenSliderDynamic(this Slider s, Func<float> fromFunc, Func<float> toFunc)
        {
            return DynamicFromDynamicTo(s, fromFunc, toFunc, v => s.value = v, Mathf.LerpUnclamped);
        }
    }
}