using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonControl : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isPausing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateButtonImage()
    {
        Image image = gameObject.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprites[(isPausing) ? 1 : 0];
        }
    }
}
