using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour
{
    public GameObject trailDotPrefab;
    [Range(2, 30)] public int trailDotNumber = 10;
    [Range(1f,100f)] public float trailDotSpacing = 5f;
    [Range(0f, 0.15f)] public float trailDotMinScale = 0.05f;
    [Range(0.15f, 0.3f)] public float trailDotMaxScale = 0.2f;
    private Transform[] trailDotList;
    private Transform[] trailOldDotList;
    public Vector2 startPoint;
    private GameObject trailDotParent;
    private GameObject trailOldDotParent;
    // Start is called before the first frame update
    void Awake()
    {
        startPoint = Vector2.zero;
        trailDotParent = new GameObject("TrailDotParent");
        trailOldDotParent = new GameObject("TrailOldDotParent");
        trailDotList = new Transform[trailDotNumber];
        trailOldDotList = new Transform[trailDotNumber];
        for(int i = 0; i < trailDotNumber; i++)
        {
            GameObject tmp = Instantiate(trailDotPrefab, trailDotParent.transform);
            GameObject tmpOld = Instantiate(trailDotPrefab, trailOldDotParent.transform);
            trailDotList[i] = tmp.transform;
            trailOldDotList[i] = tmpOld.transform;
            float scale = trailDotMaxScale - (trailDotMaxScale - trailDotMinScale) * (i / (float)(trailDotNumber - 1));
            trailDotList[i].localScale = Vector3.one * scale;
            trailOldDotList[i].localScale = Vector3.one * scale * 0.5f;
        }
        HideTrail();
        HideOldTrail();
    }
    public void ShowTrail()
    {
        trailDotParent.SetActive(true);
    }
    public void ShowOldTrail()
    {
        trailOldDotParent.SetActive(true);
    }
    public void HideTrail()
    {
        trailDotParent.SetActive(false);
    }
    public void HideOldTrail()
    {
        trailOldDotParent.SetActive(false);
    }
    public void UpdateTrail(Vector2 force)
    {
        for(int i = 0; i < trailDotNumber; i++)
        {
            trailDotList[i].position = startPoint + force * trailDotSpacing * (i + 1) / 5000f;
        }
    }
    public void UpdateOldTrail()
    {
        for (int i = 0; i < trailDotNumber; i++)
        {
            trailOldDotList[i].position = trailDotList[i].position;
        }
    }
}
