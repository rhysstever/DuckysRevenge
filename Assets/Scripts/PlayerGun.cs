using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    ShakeBehavior shake;
    // Start is called before the first frame update

    bool gunActive = false;

    public PlayerGun gun;

    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public GameObject muzzleFlashPrefab;

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
        if (gunActive)
        {
            if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
            {
                Shoot();
            }
        }

    }

    public void Shoot()
    {
        shake.TriggerShake(.25f, 5f);
        GameManager.instance.GunshotSFX();
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = bulletSpawner.transform.position;
        b.transform.right = bulletSpawner.transform.right;
        GameObject m = Instantiate(muzzleFlashPrefab);
        m.transform.position = transform.parent.transform.position;
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            b.GetComponent<PlayerBullet>().speed = -b.GetComponent<PlayerBullet>().speed;
            m.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            b.GetComponent<SpriteRenderer>().flipX = true;
            
        }

        GetComponentInParent<PlayerMovement>().Recoil();

        
        
        
    }

    public void ActivateGun(bool flipped)
    {
        if (flipped)
        {
            GetComponent<Flippable>().Flip();
        }
        gameObject.SetActive(true);
        gunActive = true;
        
    }

    public void DeactivateGun()
    {
        gameObject.SetActive(false);
        gunActive = false;
    }
}
