using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    ShakeBehavior shake;
    // Start is called before the first frame update

    private void Awake()
    {
        shake = FindObjectOfType<ShakeBehavior>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        shake.TriggerShake(0.2f, 1.25f);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down);
        
        if (hitInfo.collider)
        {
            Debug.Log("Hit " + hitInfo.collider);
        }
        else
        {
            Debug.Log("Hit nothing!");
        }
    }
}
