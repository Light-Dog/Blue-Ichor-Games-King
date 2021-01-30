using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTaken : MonoBehaviour
{
    public int health;
    public Image healthBar;

    bool damageTaken = false;
    bool shield = false;

    private void Start()
    {
        health = 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponentInParent<WeaponController_old>() != null)
        {
            print("HA GOT IT");
            gameObject.GetComponent<DrawCollider>().hurtboxCollision();

            //take damage
            gameObject.GetComponentInParent<DamageAnim>().take_damage();
            health -= other.gameObject.GetComponentInParent<WeaponController_old>().damage;
        }
    }

    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject.transform.parent.gameObject);

        //Self damage player
        if (damageTaken)
        {
            if (healthBar != null)
            {
                if(shield) 
                    healthBar.fillAmount -= .11f;
                else
                    healthBar.fillAmount -= .21f;

                gameObject.GetComponent<DrawCollider>().hurtboxCollision();
                damageTaken = false;

                if (healthBar.fillAmount <= 0.0f)
                    healthBar.fillAmount= .05f;
            }
        }
    }

    public void ouch()
    {
        damageTaken = true;
    }

    public void shieldOuch()
    {
        damageTaken = true;
        shield = true;
    }
}
