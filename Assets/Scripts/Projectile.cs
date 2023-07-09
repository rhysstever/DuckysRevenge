using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Set on creation
    public GameObject source;

    // Set at Start
    private float bounds;

    public GameObject impactParticles;
    // Start is called before the first frame update
    void Start()
    {
        bounds = 500.0f;
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
        // Find the base game object of what the projectile collided with, excluding managers
        GameObject baseGameObj = collision.gameObject;
        while(baseGameObj.transform.parent != null && baseGameObj.transform.parent.tag != "Manager")
        {
            baseGameObj = baseGameObj.transform.parent.gameObject;
        }

        // Do nothing if the projectile collides with what shot it
        // OR if the source is not set
        if(source == null || baseGameObj == source) return;

        switch(baseGameObj.tag)
        {
            case "Archer":
                //Destroy(baseGameObj);
                //Destroy(gameObject);
                baseGameObj.GetComponent<Archer>().Die();
                break;
            case "Player":
                //Debug.Log("Ouch!");
                //Destroy(baseGameObj);
                baseGameObj.GetComponentInChildren<PlayerMovement>().Die();
                GameManager.instance.ArcherDeathSFX();
                Destroy(gameObject);
                break;
            case "Projectile":
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                GameManager.instance.ArrowImpactSFX();
                GameObject p = Instantiate(impactParticles);
                p.transform.position = collision.contacts[0].point;
                p.transform.right= collision.contacts[0].normal;
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D()
    {
        Debug.Log("I hit an archer");
    }
}
