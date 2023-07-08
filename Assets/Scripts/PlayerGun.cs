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
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        shake.TriggerShake(0.33f, 0.9f);
    }
}
