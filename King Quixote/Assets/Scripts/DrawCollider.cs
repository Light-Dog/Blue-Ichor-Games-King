using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    public bool draw = false;

    public WeaponAttack weapon;
    public SpriteRenderer hitbox;

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<WeaponAttack>();
        hitbox = gameObject.GetComponent<SpriteRenderer>();

        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            draw = !draw;
        }

        if(draw == true)
        {

            if (weapon.currentSprite == weapon.activeFrame)
                hitbox.enabled = true;
            else
                hitbox.enabled = false;
        }
    }
}
