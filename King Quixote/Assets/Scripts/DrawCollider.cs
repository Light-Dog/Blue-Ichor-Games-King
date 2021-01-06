using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    bool draw = false;
    public Color hitboxColor;
    Color savedColor;

    public WeaponAttack weapon;
    public SpriteRenderer hitbox;
    public bool hurtbox = false;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<WeaponAttack>();
        hitbox = gameObject.GetComponent<SpriteRenderer>();

        hitbox.enabled = false;
        savedColor = hitbox.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            draw = !draw;
        }

        if (draw == true)
        {
            hitbox.enabled = true;

            if(hurtbox == false)
            {
                if (weapon.IsAttacking())
                {
                    hitbox.color = hitboxColor;
                    print("Color Change");
                }
                else
                    hitbox.color = savedColor;
            }
            else
            {
                if(timer > 0.0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    hitbox.color = savedColor;
                }
            }
        }
        else
            hitbox.enabled = false;
    }

    public void hurtboxCollision()
    {
        if(draw)
        {
            hitbox.color = hitboxColor;
            timer = 1f;
        }
    }
}
