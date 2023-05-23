using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonControl : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isPausing = true;
    public void UpdateButtonImage()
    {
        Image image = gameObject.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprites[(isPausing) ? 1 : 0];
        }
    }
}
