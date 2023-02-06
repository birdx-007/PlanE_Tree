using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public GameObject saplingPrefab;
    private GameObject saplingParent;
    public static int saplingNumber = 0;
    public BackgroundControl backgroundControl;
    // Start is called before the first frame update
    void Awake()
    {
        saplingParent = new GameObject("SaplingParent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initiate()
    {
        for (int i = 0; i < saplingParent.transform.childCount; i++)
        {
            Destroy(saplingParent.transform.GetChild(i).gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        PlanetControl planetControl = collider.GetComponent<PlanetControl>();
        if (planetControl != null && planetControl.gameObject.transform.position.y <= -5f)
        {
            Debug.Log("planet hit ground");
            if (planetControl.plantedTreeNumber > 0)
            {
                Vector2 saplingPos = new Vector2(collider.transform.position.x, -4.5f);
                GameObject sapling = Instantiate(saplingPrefab, saplingParent.transform);
                sapling.transform.position = saplingPos;
                sapling.transform.localScale = Vector2.one * (0.25f + planetControl.plantedTreeNumber * 0.25f);
                saplingNumber += 1;
                backgroundControl.UpdateBackground(saplingNumber*0.05f);
            }
            Destroy(collider.gameObject);
        }
    }
}
