using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public List<WeaponAttack> attacks;
    int num_attacks = 0;

    bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        num_attacks = attacks.Capacity;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            print("attacking!");
            attacking = false;
            for (int i = 0; i < num_attacks; i++)
            {
                if (attacks[i].attackTrigger())
                    attacking = true;
            }
        }
        else
        {
            for (int i = 0; i < num_attacks; i++)
            {
                if (Input.GetKeyUp(attacks[i].attackButton))
                {
                    print("Button Pushed: " + attacks[i].attackButton.ToString());
                    attacks[i].attackWithWeapon();
                    attacking = true;
                    
                }
            }
            
        }

        
    }
}
