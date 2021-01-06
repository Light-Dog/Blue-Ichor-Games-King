using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTaken : MonoBehaviour
{
    public int health;
    public Image healthBar;

    private void Start()
    {
        health = 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponentInParent<WeaponAttack>() != null)
        {
            print("HA GOT IT");
            gameObject.GetComponent<DrawCollider>().hurtboxCollision();

            gameObject.GetComponentInParent<DamageAnim>().take_damage();
            health -= other.gameObject.GetComponentInParent<WeaponAttack>().damage;
        }


    }

    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject.transform.parent.gameObject);

        //Self damage player
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (healthBar != null)
            {
                healthBar.fillAmount -= .21f;
                gameObject.GetComponent<DrawCollider>().hurtboxCollision();

                if (healthBar.fillAmount <= 0.0f)
                    healthBar.fillAmount= .05f;
            }
        }
    }
}
