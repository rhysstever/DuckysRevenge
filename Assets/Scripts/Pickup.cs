using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerGun gun;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "pickup")
        {
            GameManager.instance.EquipPistolSFX();
            GameManager.instance.ActivateHelpUI();
            gun.ActivateGun(GetComponent<SpriteRenderer>().flipX);
            collision.gameObject.SetActive(false);
        }
    }
}
