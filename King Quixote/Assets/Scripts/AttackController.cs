using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public List<WeaponAttack> attacks;
    int num_attacks = 0;

    public Image energyBar;
    float energyPercent = 1.0f;
    float timer = 0f;

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

                    //attack 1
                    if (i == 0)
                        energyPercent -= .1f;
                    //attack 2
                    if (i == 1)
                        energyPercent -= .2f;
                }
            }
            
        }

        energyBar.fillAmount = energyPercent;

        //4 energy per second back
        if (timer >= 1.0f)
        {
            timer = 0.0f;

            if(energyPercent < 1.0f)
                energyPercent += .04f;
        }
        else
            timer += Time.deltaTime;
    }
}
