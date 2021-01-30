using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    bool draw = false;
    bool drawAll = false;
    public Color hitboxColor;
    Color savedColor;

    public WeaponAttack weapon = null;
    public ComboScript combo = null;
    public SpriteRenderer hitbox;
    public bool hurtbox = false;
    float timer = 0.0f;

    public WeaponAction action = null;

    public int activeFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        //weapon = gameObject.GetComponent<WeaponAttack>();
        hitbox = gameObject.GetComponent<SpriteRenderer>();

        hitbox.enabled = false;
        savedColor = hitbox.color;

        //activeFrame--;
    }

    // Update is called once per frame
    void Update()
    {


        /*
        //attack draw
        if(Input.GetKeyUp(KeyCode.P))
        {
            draw = !draw;
        }

        //draw all
        if(Input.GetKeyUp(KeyCode.O))
        {
            drawAll = !drawAll;
        }
                
        if (draw == true || drawAll == true)
        {
            if(drawAll)    
                hitbox.enabled = true;

            if (hurtbox == false)
            {
                if(weapon != null)
                {
                    if (weapon.GetCurrentFrame() == activeFrame)
                    {
                        hitbox.enabled = true;

                        hitbox.color = hitboxColor;
                        //print("Color Change");
                    }
                    else
                    {
                        if (!drawAll)
                            hitbox.enabled = false;

                        hitbox.color = savedColor;
                    }
                }
                else if(combo != null)
                {
                    if (combo.GetCurrentFrame() == activeFrame)
                    {
                        hitbox.enabled = true;

                        hitbox.color = hitboxColor;
                        //print("Color Change");
                    }
                    else
                    {
                        if (!drawAll)
                            hitbox.enabled = false;

                        hitbox.color = savedColor;
                    }
                }

                
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
            */
    }

    public void hurtboxCollision()
    {
        if(draw || drawAll)
        {
            hitbox.color = hitboxColor;
            timer = 1f;
        }
    }
}
