using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundControl : MonoBehaviour
{
    public GameObject saplingPrefab;
    private GameObject saplingParent;
    private List<Transform> saplingTransformList;
    public static int saplingNumber = 0;
    public BackgroundControl backgroundControl;
    public ParticleSystem fallenParticle;
    void Awake()
    {
        saplingParent = new GameObject("SaplingParent");
        saplingTransformList = new List<Transform>();
    }

    public void Initiate()
    {
        saplingTransformList.Clear();
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
                var saplingsNear = saplingTransformList.Where(trans => (Math.Abs(saplingPos.x - trans.position.x) < 0.5f)).ToList();
                if (saplingsNear.Count > 0)
                {
                    saplingsNear.OrderBy(trans => Math.Abs(saplingPos.x - trans.position.x)).First().localScale += (Vector3.right + Vector3.up) * (planetControl.plantedTreeNumber * 0.25f);
                }
                else
                {
                    GameObject sapling = Instantiate(saplingPrefab, saplingParent.transform);
                    sapling.transform.position = saplingPos;
                    sapling.transform.localScale = (Vector3.right + Vector3.up) * (planetControl.plantedTreeNumber * 0.25f);
                    saplingTransformList.Add(sapling.transform);
                }
                saplingNumber += 1;
                backgroundControl.UpdateBackground(saplingNumber * 0.05f);
            }
            Instantiate(fallenParticle, collider.transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
        }
    }
}
