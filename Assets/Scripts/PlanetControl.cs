using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour
{
    public int plantedTreeNumber = 0;
    public GameObject treeTrunkPrefab;
    private Rigidbody2D rigidbody2d;
    public float rotateSpeed = 0.1f;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rigidbody2d.rotation += (rigidbody2d.position.x > 0) ? (rotateSpeed) : (-rotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        SeedControl seedControl = other.GetComponent<SeedControl>();
        if(seedControl != null)
        {
            /*
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            RaycastHit2D hit = Physics2D.Raycast(rb.position, rb.velocity.normalized, 0.8f, LayerMask.GetMask("Planet"));
            if (hit)
            {
                PlantTree(hit.point, hit.normal);
                Debug.DrawRay(rb.position, rb.velocity.normalized * 0.5f, Color.green, 5f, false);
                Debug.DrawRay(hit.point, hit.normal, Color.red, 5f, false);
                Destroy(other.gameObject);
            
            }
            */
            Vector2 hitPoint = other.ClosestPoint(transform.position);
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector2 hitNormal = rb.position - hitPoint;
            PlantTree(hitPoint, hitNormal);
            Destroy(other.gameObject);
        }
    }
    public void PlantTree(Vector2 point,Vector2 normal)
    {
        plantedTreeNumber += 1;
        GameObject treeTrunk = Instantiate(treeTrunkPrefab, this.transform);
        treeTrunk.transform.position = point;
        treeTrunk.transform.up = normal;
    }
}
