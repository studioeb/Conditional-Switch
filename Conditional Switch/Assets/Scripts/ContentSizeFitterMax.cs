/*
Thanks to NeoHun (https://discussions.unity.com/u/Neohun) on Unity Forums for
providing this code (https://discussions.unity.com/t/setting-maximum-width-of-layout-element-with-content-size-fitter/586475/14).
This code is NOT subject to the GPL-3.0 license the rest of the code is since it isn't my code.
*/

using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ContentSizeFitterEx : ContentSizeFitter
{

    // Define the min and max size
    public Vector2 sizeMin = new Vector2(0f, 0f);
    public Vector2 sizeMax = new Vector2(1920f, 1080f);


    public override void SetLayoutHorizontal()
    { // Override for width
        base.SetLayoutHorizontal();
        // get the rectTransform
        var rectTransform = transform as RectTransform;
        var sizeDelta = rectTransform.sizeDelta; // get the size delta
        // Clamp the x value based on the min and max size
        sizeDelta.x = Mathf.Clamp(sizeDelta.x, sizeMin.x, sizeMax.x);
        // set the size with current anchors to avoid possible problems.
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDelta.x);
    }


    public override void SetLayoutVertical()
    { // Override for height
        base.SetLayoutVertical();
        // get the rectTransform
        var rectTransform = transform as RectTransform;
        var sizeDelta = rectTransform.sizeDelta; // get the size delta
        // Clamp the y value based on the min and max size
        sizeDelta.y = Mathf.Clamp(sizeDelta.y, sizeMin.y, sizeMax.y);
        // set the size with current anchors to avoid possible problems.
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDelta.y);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ContentSizeFitterEx))]
public class ContentSizeFitterExEditor : Editor
{
    // override the editor to be able to show the public variables on the inspector.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif