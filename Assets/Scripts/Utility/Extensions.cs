using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Extensions
{
    #region INT

    /// <summary>
    /// int -> return bool 
    /// </summary>
    /// <returns><c>true</c>, if value != 0, <c>false</c> otherwise.</returns>
    /// <param name="value"> int </param>
    public static bool BoolToInt(this int _value)
    {
        if (_value == 0) return false;
        else return true;
    }

    /// <summary>
    /// Gets the minus1or plus1.
    /// </summary>
    /// <returns>The minus1or plus1.</returns>
    /// <param name="_value">Value.</param>
    public static int GetMinus1orPlus1(this int _value)
    {
        if (_value < 0)
        {
            return -1;
        }

        return 1;
    }

    /// <summary>
    /// rand value Shuffle
    /// </summary>
    public static int[] Shuffle(this int[] _value)
    {
        for (int i = 0; i < _value.Length; i++)
        {
            int _rnd = UnityEngine.Random.Range(0, _value.Length);
            int _temp = _value[i];
            _value[i] = _value[_rnd];
            _value[_rnd] = _temp;
        }

        return _value;
    }

    /// <summary>
    /// Follows the score.
    /// </summary>
    /// <returns>The score.</returns>
    /// <param name="_follow">Follow.</param>
    /// <param name="_target">Target.</param>
    public static int FollowScore(this int _follow, int _target)
    {
        if (Mathf.Abs(_target - _follow) > 100000)
            return _follow + (int)Mathf.Sign(_target - _follow) * 56789;
        else if (Mathf.Abs(_target - _follow) > 10000)
            return _follow + (int)Mathf.Sign(_target - _follow) * 6789;
        else if (Mathf.Abs(_target - _follow) > 1000)
            return _follow + (int)Mathf.Sign(_target - _follow) * 789;
        else if (Mathf.Abs(_target - _follow) > 100)
            return _follow + (int)Mathf.Sign(_target - _follow) * 89;
        else if (Mathf.Abs(_target - _follow) > 10)
            return _follow + (int)Mathf.Sign(_target - _follow) * 9;
        else if (Mathf.Abs(_target - _follow) > 0)
            return _follow + (int)Mathf.Sign(_target - _follow);
        else
            return _follow;
    }

    /// <summary>
    /// Withs the comma.
    /// </summary>
    /// <returns>The comma.</returns>
    /// <param name="_value">int Value.</param>
    public static string WithComma(this int _value)
    {
        return string.Format("{0:#,##0}", _value.ToString());
    }

    public static string WithComma(this long _value)
    {
        return string.Format("{0:#,##0}", _value.ToString());
    }

    /// <summary>
    /// Gets the byte string.
    /// </summary>
    /// <returns>The byte string.</returns>
    /// <param name="byteCount">Byte count.</param>
    public static string ToByteString(this int _value)
    {
        string size = "0 B";
        if (_value >= 1073741824.0f)
            size = $"{(double)_value / 1073741824.0f:##.##}GB";
        else if (_value >= 1048576.0f)
            size = $"{(double)_value / 1048576.0f:##.##}MB";
        else if (_value >= 1024.0f)
            size = $"{(double)_value / 1024.0f:##.##}KB";
        else if (_value > 0 && _value < 1024.0f)
            size = $"{_value} B";

        return size;
    }

    #endregion

    #region FLOAT

    public static float Lerp(this float _startValue, float _endValue, float _currentTime, float _duration) { return _endValue * _currentTime / _duration + _startValue; }

    public static float TO_RADIAN(this float value) { return value * (Mathf.PI / 180.0f); }
    public static float TO_DEGREE(this float value) { return value * (180.0f / Mathf.PI); }

    /// <summary>
    /// Gets the byte string.
    /// </summary>
    /// <returns>The byte string.</returns>
    /// <param name="byteCount">Byte count.</param>
    public static string ToByteString(this float _value)
    {
        string size = "0 B";
        if (_value >= 1073741824.0f)
            size = $"{_value / 1073741824.0f:##.##}GB";
        else if (_value >= 1048576.0f)
            size = $"{_value / 1048576.0f:##.##}MB";
        else if (_value >= 1024.0f)
            size = $"{_value / 1024.0f:##.##}KB";
        else if (_value > 0 && _value < 1024.0f)
            size = $"{_value} B";

        return size;
    }

    #endregion

    #region DOUBLE

    public static double Abs(this double _value)
    {
        if (_value < 0)
        {
            _value *= -1;
        }

        return _value;
    }

    /// <summary>
    /// Gets the byte string.
    /// </summary>
    /// <returns>The byte string.</returns>
    /// <param name="byteCount">Byte count.</param>
    public static string ToByteString(this double _value)
    {
        string size = "0 B";
        if (_value >= 1073741824.0f)
            size = $"{_value / 1073741824.0f:##.##}GB";
        else if (_value >= 1048576.0f)
            size = $"{_value / 1048576.0f:##.##}MB";
        else if (_value >= 1024.0f)
            size = $"{_value / 1024.0f:##.##}KB";
        else if (_value > 0 && _value < 1024.0f)
            size = $"{_value} B";

        return size;
    }

    #endregion

    #region VECTOR2

    public static Vector2 rotate(this Vector2 _vec, float _angle)
    {
        float rad = _angle.TO_RADIAN();
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        float newX = _vec.x * cos - _vec.y * sin;
        float newY = _vec.x * sin + _vec.y * cos;
        _vec.x = newX;
        _vec.y = newY;

        return _vec;
    }


    public static Vector2 pX(this Vector2 vec, float x) { vec.x += x; return vec; }
    public static Vector2 pY(this Vector2 vec, float y) { vec.y += y; return vec; }

    public static Vector2 pXY(this Vector2 vec, float x, float y) { vec.x += x; vec.y += y; return vec; }

    public static Vector2 sX(this Vector2 vec, float x) { vec.x = x; return vec; }
    public static Vector2 sY(this Vector2 vec, float y) { vec.y = y; return vec; }

    public static Vector2 sXY(this Vector2 vec, float x, float y) { vec.x = x; vec.y = y; return vec; }

    public static Vector2 Lerp(this Vector2 vec, Vector2 endVector, float currentTime) { return (endVector - vec) * currentTime + vec; }
    public static Vector2 Lerp(this Vector2 vec, Vector2 startVector, Vector2 endVector, float currentTime) { return (endVector - startVector) * currentTime + vec; }

    #endregion

    #region TRANSFORM
    /// <summary>
    /// Transform LocalPosition Extenstions
    /// </summary>
    /// Transform LocalPosition Calculate Extensions.
    /// 'p' is Plus 's' is Set
    public static Transform pLocalX(this Transform t, float x) { t.localPosition = t.localPosition.pX(x); return t; }
    public static Transform pLocalY(this Transform t, float y) { t.localPosition = t.localPosition.pY(y); return t; }
    public static Transform pLocalZ(this Transform t, float z) { t.localPosition = t.localPosition.pZ(z); return t; }

    public static Transform pLocalXY(this Transform t, float x, float y) { t.localPosition = t.localPosition.pXY(x, y); return t; }
    public static Transform pLocalXZ(this Transform t, float x, float z) { t.localPosition = t.localPosition.pXZ(x, z); return t; }
    public static Transform pLocalYZ(this Transform t, float y, float z) { t.localPosition = t.localPosition.pYZ(y, z); return t; }

    public static Transform pLocalXYZ(this Transform t, float x, float y, float z) { t.localPosition = t.localPosition.pXYZ(x, y, z); return t; }
    public static Transform pLocalXYZ(this Transform t, Vector3 pos) { t.localPosition = t.localPosition.pXYZ(pos.x, pos.y, pos.z); return t; }

    public static Transform sLocalX(this Transform t, float x) { t.localPosition = t.localPosition.sX(x); return t; }
    public static Transform sLocalY(this Transform t, float y) { t.localPosition = t.localPosition.sY(y); return t; }
    public static Transform sLocalZ(this Transform t, float z) { t.localPosition = t.localPosition.sZ(z); return t; }

    public static Transform sLocalXY(this Transform t, float x, float y) { t.localPosition = t.localPosition.sXY(x, y); return t; }
    public static Transform sLocalXZ(this Transform t, float x, float z) { t.localPosition = t.localPosition.sXZ(x, z); return t; }
    public static Transform sLocalYZ(this Transform t, float y, float z) { t.localPosition = t.localPosition.sYZ(y, z); return t; }

    public static Transform sLocalXYZ(this Transform t, float x, float y, float z) { t.localPosition = t.localPosition.sXYZ(x, y, z); return t; }
    public static Transform sLocalXYZ(this Transform t, Vector3 pos) { t.localPosition = t.localPosition.sXYZ(pos.x, pos.y, pos.z); return t; }

    public static Transform pWorldX(this Transform t, float x) { t.position = t.position.pX(x); return t; }
    public static Transform pWorldY(this Transform t, float y) { t.position = t.position.pY(y); return t; }
    public static Transform pWorldZ(this Transform t, float z) { t.position = t.position.pZ(z); return t; }

    public static Transform pWorldXY(this Transform t, float x, float y) { t.position = t.position.pXY(x, y); return t; }
    public static Transform pWorldXZ(this Transform t, float x, float z) { t.position = t.position.pXZ(x, z); return t; }
    public static Transform pWorldYZ(this Transform t, float y, float z) { t.position = t.position.pYZ(y, z); return t; }

    public static Transform pWorldXYZ(this Transform t, float x, float y, float z) { t.position = t.position.pXYZ(x, y, z); return t; }
    public static Transform pWorldXYZ(this Transform t, Vector3 pos) { t.position = t.position.pXYZ(pos.x, pos.y, pos.z); return t; }

    public static Transform sWorldX(this Transform t, float x) { t.position = t.position.sX(x); return t; }
    public static Transform sWorldY(this Transform t, float y) { t.position = t.position.sY(y); return t; }
    public static Transform sWorldZ(this Transform t, float z) { t.position = t.position.sZ(z); return t; }

    public static Transform sWorldXY(this Transform t, float x, float y) { t.position = t.position.sXY(x, y); return t; }
    public static Transform sWorldXZ(this Transform t, float x, float z) { t.position = t.position.sXZ(x, z); return t; }
    public static Transform sWorldYZ(this Transform t, float y, float z) { t.position = t.position.sYZ(y, z); return t; }

    public static Transform sWorldXYZ(this Transform t, float x, float y, float z) { t.position = t.position.sXYZ(x, y, z); return t; }
    public static Transform sWorldXYZ(this Transform t, Vector3 pos) { t.position = t.position.sXYZ(pos.x, pos.y, pos.z); return t; }

    #endregion

    #region VECTOR3
    /// <summary>
    ///  Vector3_ Extenstions
    /// </summary>
    /// Vector3 Calculate Extensions.
    /// 'p' is Plus 's' is Set
    public static Vector3 pX(this Vector3 vec, float x) { vec.x += x; return vec; }
    public static Vector3 pY(this Vector3 vec, float y) { vec.y += y; return vec; }
    public static Vector3 pZ(this Vector3 vec, float z) { vec.z += z; return vec; }

    public static Vector3 pXY(this Vector3 vec, float x, float y) { vec.x += x; vec.y += y; return vec; }
    public static Vector3 pXZ(this Vector3 vec, float x, float z) { vec.x += x; vec.z += z; return vec; }
    public static Vector3 pYZ(this Vector3 vec, float y, float z) { vec.y += y; vec.z += z; return vec; }

    public static Vector3 pXYZ(this Vector3 vec, float x, float y, float z) { vec.x += x; vec.y += y; vec.z += z; return vec; }

    public static Vector3 sX(this Vector3 vec, float x) { vec.x = x; return vec; }
    public static Vector3 sY(this Vector3 vec, float y) { vec.y = y; return vec; }
    public static Vector3 sZ(this Vector3 vec, float z) { vec.z = z; return vec; }

    public static Vector3 sXY(this Vector3 vec, float x, float y) { vec.x = x; vec.y = y; return vec; }
    public static Vector3 sXZ(this Vector3 vec, float x, float z) { vec.x = x; vec.z = z; return vec; }
    public static Vector3 sYZ(this Vector3 vec, float y, float z) { vec.y = y; vec.z = z; return vec; }

    public static Vector3 sXYZ(this Vector3 vec, float x, float y, float z) { vec.x = x; vec.y = y; vec.z = z; return vec; }

    public static Vector3 Lerp(this Vector3 vec, Vector3 endVector, float currentTime) { return (endVector - vec) * currentTime + vec; }
    public static Vector3 Lerp(this Vector3 vec, Vector3 startVector, Vector3 endVector, float currentTime) { return (endVector - startVector) * currentTime + vec; }

    #endregion

    #region COLOR

    public static Color pR(this Color color, float r) { color.r += r; return color; }
    public static Color pG(this Color color, float g) { color.g += g; return color; }
    public static Color pB(this Color color, float b) { color.b += b; return color; }
    public static Color pA(this Color color, float a) { color.a += a; return color; }

    public static Color pRG(this Color color, float r, float g) { color.r += r; color.g += g; return color; }
    public static Color pRB(this Color color, float r, float b) { color.r += r; color.b += b; return color; }
    public static Color pRA(this Color color, float r, float a) { color.r += r; color.a += a; return color; }
    public static Color pGB(this Color color, float g, float b) { color.g += g; color.b += b; return color; }
    public static Color pGA(this Color color, float g, float a) { color.g += g; color.a += a; return color; }
    public static Color pBA(this Color color, float b, float a) { color.b += b; color.a += a; return color; }

    public static Color sR(this Color color, float r) { color.r = r; return color; }
    public static Color sG(this Color color, float g) { color.g = g; return color; }
    public static Color sB(this Color color, float b) { color.b = b; return color; }
    public static Color sA(this Color color, float a) { color.a = a; return color; }

    public static Color sRG(this Color color, float r, float g) { color.r = r; color.g = g; return color; }
    public static Color sRB(this Color color, float r, float b) { color.r = r; color.b = b; return color; }
    public static Color sRA(this Color color, float r, float a) { color.r = r; color.a = a; return color; }
    public static Color sGB(this Color color, float g, float b) { color.g = g; color.b = b; return color; }
    public static Color sGA(this Color color, float g, float a) { color.g = g; color.a = a; return color; }
    public static Color sBA(this Color color, float b, float a) { color.b = b; color.a = a; return color; }

    public static Color HexToColor(this string hex)
    {
        string hc = ExtractHexDigits(hex);
        if (hc.Length != 6)
        {
            return Color.clear;
        }

        string r = hc.Substring(0, 2);
        string g = hc.Substring(2, 2);
        string b = hc.Substring(4, 2);
        Color color = Color.clear;
        try
        {
            int ri = int.Parse(r, System.Globalization.NumberStyles.HexNumber);
            int gi = int.Parse(g, System.Globalization.NumberStyles.HexNumber);
            int bi = int.Parse(b, System.Globalization.NumberStyles.HexNumber);
            color = new Color((float)ri / 255, (float)gi / 255, (float)bi / 255);
        }
        catch
        {
            return Color.clear;
        }

        return color;
    }

    public static string ExtractHexDigits(string input)
    {
        Regex isHexDigit = new Regex("[abcdefABCDEF\\d]+");
        string newnum = "";
        foreach (char c in input)
        {
            if (isHexDigit.IsMatch(c.ToString()))
            {
                newnum += c.ToString();
            }
        }

        return newnum;
    }

    #endregion

    #region RECT

    public static Rect pX(this Rect rect, float x) { rect.x += x; return rect; }
    public static Rect pY(this Rect rect, float y) { rect.y += y; return rect; }
    public static Rect pW(this Rect rect, float w) { rect.width += w; return rect; }
    public static Rect pH(this Rect rect, float h) { rect.height += h; return rect; }

    public static Rect pXY(this Rect rect, float x, float y) { rect.x += x; rect.y += y; return rect; }
    public static Rect pXW(this Rect rect, float x, float w) { rect.x += x; rect.width += w; return rect; }
    public static Rect pXH(this Rect rect, float x, float h) { rect.x += x; rect.height += h; return rect; }
    public static Rect pYW(this Rect rect, float y, float w) { rect.y += y; rect.width += w; return rect; }
    public static Rect pYH(this Rect rect, float y, float h) { rect.y += y; rect.height += h; return rect; }
    public static Rect pWH(this Rect rect, float w, float h) { rect.width += w; rect.height += h; return rect; }

    public static Rect sX(this Rect rect, float x) { rect.x = x; return rect; }
    public static Rect sY(this Rect rect, float y) { rect.y = y; return rect; }
    public static Rect sW(this Rect rect, float w) { rect.width = w; return rect; }
    public static Rect sH(this Rect rect, float h) { rect.height = h; return rect; }

    public static Rect sXY(this Rect rect, float x, float y) { rect.x = x; rect.y = y; return rect; }
    public static Rect sXW(this Rect rect, float x, float w) { rect.x = x; rect.width = w; return rect; }
    public static Rect sXH(this Rect rect, float x, float h) { rect.x = x; rect.height = h; return rect; }
    public static Rect sYW(this Rect rect, float y, float w) { rect.y = y; rect.width = w; return rect; }
    public static Rect sYH(this Rect rect, float y, float h) { rect.y = y; rect.height = h; return rect; }
    public static Rect sWH(this Rect rect, float w, float h) { rect.width = w; rect.height = h; return rect; }

    #endregion

    #region Action

    public static void SafeEvent(this System.Action eventAction)
    {
        if (eventAction != null)
            eventAction();
    }

    public static void SafeEvent<T>(this System.Action<T> eventAction, T t)
    {
        if (eventAction != null)
            eventAction(t);
    }

    public static void SafeEvent<T1, T2>(this System.Action<T1, T2> eventAction, T1 t1, T2 t2)
    {
        if (eventAction != null)
            eventAction(t1, t2);
    }

    public static void SafeEvent<T1, T2, T3>(this System.Action<T1, T2, T3> eventAction, T1 t1, T2 t2, T3 t3)
    {
        if (eventAction != null)
            eventAction(t1, t2, t3);
    }

    public static void SafeEvent<T1, T2, T3, T4>(this System.Action<T1, T2, T3, T4> eventAction, T1 t1, T2 t2, T3 t3, T4 t4)
    {
        if (eventAction != null)
            eventAction(t1, t2, t3, t4);
    }

    #endregion

    #region InterSection

    public static Vector3 LineLineIntersection(Vector3 position_1, Vector3 direction_1, Vector3 position_2, Vector3 direction_2)
    {
        Vector3 lineVec3 = position_2 - position_1;
        Vector3 crossVec1and2 = Vector3.Cross(direction_1, direction_2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, direction_2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parrallel
        if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
            return position_1 + (direction_1 * s);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public static Vector2 LineInterSecionPoint(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float S = 0, T = 0;

        if (isLineIntersect(A, B, C, D, ref S, ref T))
        {
            Vector2 P;
            P.x = A.x + S * (B.x - A.x);
            P.y = A.y + S * (B.y - A.y);

            return P;
        }

        return Vector2.zero;
    }

    public static bool isLineIntersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D, ref float S, ref float T)
    {
        if ((A.x == B.x && A.y == B.y) || (C.x == D.x && C.y == D.y))
        {
            return false;
        }

        float denom = crossProduct2Vector(A, B, C, D);

        if (denom == 0)
        {
            return false;
        }

        S = crossProduct2Vector(C, D, C, A) / denom;
        T = crossProduct2Vector(A, B, C, A) / denom;

        return true;
    }

    public static float crossProduct2Vector(Vector2 A, Vector2 B, Vector2 C, Vector3 D)
    {
        return (D.y - C.y) * (B.x - A.x) - (D.x - C.x) * (B.y - A.y);
    }

    #endregion

    #region AudioSource

    public static IEnumerator FadeInOut(this AudioSource _source, float _startVolume, float _endVolume, float _delay, float _fadeTime, System.Action _onComplete)
    {
        if (_source.isPlaying == false)
        {
            _onComplete.SafeEvent();
            yield break;
        }

        yield return new WaitForSeconds(_delay);

        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime / _fadeTime;
            _source.volume = Mathf.Lerp(_startVolume, _endVolume, timer);
            yield return null;
        }

        _source.volume = _endVolume;

        _onComplete.SafeEvent();
    }

    #endregion

    #region Animation

    public static IEnumerator WhilePlaying(this Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }

    #endregion

    #region EaseType

    /// <summary>
    /// Eases the in quart.
    /// </summary>
    /// <returns>The in quart.</returns>
    /// <param name="t">time</param>
    /// <param name="b">begin value</param>
    /// <param name="c">change value</param>
    /// <param name="d">duration</param>
    public static float EaseInQuart(this float t, float b = 0, float c = 1, float d = 1.0f)
    {
        t /= d;
        return c * t * t * t * t + b;
    }

    /// <summary>
    /// Eases the out quart.
    /// </summary>
    /// <returns>The out quart.</returns>
    /// <param name="t">time</param>
    /// <param name="b">begin value</param>
    /// <param name="c">change value</param>
    /// <param name="d">duration</param>
    public static float EaseOutQuart(this float t, float b = 0, float c = 1, float d = 1.0f)
    {
        t /= d;
        t--;

        return -c * (t * t * t * t - 1) + b;
    }

    /// <summary>
    /// Eases the in quad.
    /// </summary>
    /// <returns>The in quad.</returns>
    /// <param name="t">time</param>
    /// <param name="b">begin value</param>
    /// <param name="c">change value</param>
    /// <param name="d">duration</param>
    public static float EaseInQuad(this float t, float b = 0, float c = 1, float d = 1.0f)
    {
        t /= d;
        return c * t * t + b;
    }

    /// <summary>
    /// Eases the out quad.
    /// </summary>
    /// <returns>The out quad.</returns>
    /// <param name="t">time</param>
    /// <param name="b">begin value</param>
    /// <param name="c">change value.</param>
    /// <param name="d">duration</param>
    public static float EaseOutQuad(this float t, float b = 0, float c = 1, float d = 1.0f)
    {
        t /= d;
        return -c * t * (t - 2) + b;
    }
    #endregion

    #region IEnumrable

    public static List<T> Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            var rnd1 = UnityEngine.Random.Range(0, list.Count);
            var rnd2 = UnityEngine.Random.Range(0, list.Count);

            T tmp = list[rnd1];
            list[rnd1] = list[rnd2];
            list[rnd2] = tmp;
        }

        return list;
    }

    #endregion
}