using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float bounds;

    // Start is called before the first frame update
    void Start()
    {
        bounds = 40.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x != Mathf.Clamp(transform.position.x, -bounds, bounds)
            || transform.position.y != Mathf.Clamp(transform.position.y, -bounds, bounds))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit the player!");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit another projectile!");
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit by another projectile!");
            Destroy(gameObject);
        }
    }
}
