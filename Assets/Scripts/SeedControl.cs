using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedControl : MonoBehaviour
{
    private float rotateSpeed = 6f;
    private Rigidbody2D rigidbody2d;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<-20f
            ||transform.position.x>20f
            ||transform.position.y<-20f
            ||transform.position.y>20f)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        rigidbody2d.rotation += rotateSpeed;
    }
    public void Launch(Vector2 force)
    {
        rigidbody2d.AddForce(force);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        TreeTrunkControl treeTrunkControl = other.collider.GetComponent<TreeTrunkControl>();
        if (treeTrunkControl != null)
        {
            Debug.Log("seed hit treetrunk");
            treeTrunkControl.GrowSeed();
        }
    }
}
