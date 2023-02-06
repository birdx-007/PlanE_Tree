using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundControl : MonoBehaviour
{
    private Image background;
    public Color startColor = Color.white;
    public Color endColor = Color.black;
    private AnimationCurve gradientCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.25f, 1));
    private Vector2Int imageSize;
    private Texture2D texture2D;
    void Awake()
    {
        background = GetComponent<Image>();
        imageSize = new Vector2Int(1, Screen.height);
        texture2D = new Texture2D(imageSize.x, imageSize.y);
    }

    void Start()
    {
        Initiate();
    }
    public void Initiate()
    {
        UpdateBackground(0f);
    }
    public void UpdateBackground(float cleanliness)
    {
        cleanliness = Mathf.Clamp01(cleanliness);
        gradientCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.25f + cleanliness * 0.75f, 1));
        //遍历像素点
        for (int y = 0; y < imageSize.y; y++)
        {
            Color pixelColor = GetColorByCurve(1.0f * y / imageSize.y);
            texture2D.SetPixel(1, y, pixelColor);
        }
        texture2D.Apply();
        //创建Sprite
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, 1, imageSize.y), new Vector2(0.5f, 0.5f));
        background.sprite = sprite;
        
    }
    private Color GetColorByCurve(float ratio)
    {
        float curveValue = gradientCurve.Evaluate(ratio);
        curveValue = Mathf.Clamp01(curveValue);
        return Color.Lerp(startColor, endColor, curveValue);
    }
}
