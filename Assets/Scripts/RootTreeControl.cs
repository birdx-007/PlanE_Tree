using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTreeControl : MonoBehaviour
{
    private bool isDragging = false;
    public bool isMouseOnTree = false;
    public TrailControl trail;
    public GameObject seedPrefab;
    private GameObject seedParent;
    public float shootForce = 300f;
    public float shootMaxForce = 500f;
    private Vector2 force;
    private float dragDistance;
    public static int seedLeftNumber;
    private float seedRecoverTime = 30f;
    private float leftTimeUntilNewSeed;
    void Awake()
    {
        seedParent = new GameObject("SeedParent");
        leftTimeUntilNewSeed = seedRecoverTime;
    }

    // Update is called once per frame
    void Update()
    {
        leftTimeUntilNewSeed -= Time.deltaTime;
        if(leftTimeUntilNewSeed <= 0)
        {
            // recover a seed
            seedLeftNumber++;
            leftTimeUntilNewSeed = seedRecoverTime;
        }
        if (ManagerControl.isPlaying && seedLeftNumber > 0)
        {
            if (Input.GetMouseButtonDown(0) && isMouseOnTree)
            {
                isDragging = true;
                OnDragStart();
            }
            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                isDragging = false;
                OnDragEnd();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
    }
    public void Initiate()
    {
        isDragging = false;
        isMouseOnTree = false;
        for (int i = 0; i < seedParent.transform.childCount; i++)
        {
            Destroy(seedParent.transform.GetChild(i).gameObject);
        }
        trail.HideTrail();
        trail.HideOldTrail();
    }
    void Shoot()
    {
        if (seedLeftNumber > 0)
        {
            seedLeftNumber--;
            GameObject seed = Instantiate(seedPrefab, trail.startPoint, Quaternion.identity);
            seed.transform.SetParent(seedParent.transform);
            SeedControl seedControl = seed.GetComponent<SeedControl>();
            seedControl.Launch(force);
            Debug.Log("leftSeed:" + seedLeftNumber);
        }
    }
    void OnDragStart()
    {
        trail.ShowTrail();
        trail.startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnDragEnd()
    {
        trail.HideTrail();
        if (dragDistance < 0.5f)
        {
            return;
        }
        trail.UpdateOldTrail();
        trail.ShowOldTrail();
        Shoot();
    }
    void OnDrag()
    {
        trail.ShowTrail();
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragDistance = Vector2.Distance(trail.startPoint, mousePoint);
        Vector2 forceDirection = (trail.startPoint - mousePoint).normalized;
        force = Mathf.Min(shootMaxForce, shootForce * dragDistance) * forceDirection;
        trail.UpdateTrail(force);
    }
    private void OnMouseEnter()
    {
        isMouseOnTree = true;
    }
    private void OnMouseExit()
    {
        isMouseOnTree = false;
    }
    private void OnMouseOver()
    {
        isMouseOnTree = true;
    }
}
