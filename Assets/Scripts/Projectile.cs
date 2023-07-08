using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Set on creation
    public GameObject source;

    // Set at Start
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
        GameObject baseGameObj = collision.gameObject;
        while(baseGameObj.transform.parent != null && baseGameObj.transform.parent.tag != "Manager")
        {
            baseGameObj = baseGameObj.transform.parent.gameObject;
        }

        switch(baseGameObj.tag)
        {
            case "Archer":
                if(baseGameObj != source)
                {
                    Destroy(baseGameObj);
                    Destroy(gameObject);
                }
                break;
            case "Player":
                Debug.Log("Ouch!");
                Destroy(baseGameObj);
                Destroy(gameObject);
                break;
            case "Projectile":
                Debug.Log("Hit another projectile!");
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Hit " + baseGameObj.tag);
                Destroy(gameObject);
                break;
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
