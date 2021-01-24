using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string weaponName;
    public List<WeaponAttack> attackList;
    int num_attacks = 0;
    int current_attack = 0;
    public int damage = 2;

    public List<ComboScript> comboList;
    bool comboConfirm = false;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;
    int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        num_attacks = attackList.Capacity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //returns cost of an attack if an attack is pressed
    public float WeaponCheck()
    {
        float cost = 0.0f;
        int i = 0;

        foreach(WeaponAttack attack in attackList)
        {
            //print("Attacking allowed");

            if (Input.GetKeyDown(attack.attackButton))
            {

                //enable all combos that started
                foreach(ComboScript combo in comboList)
                {
                    print("Combo check with button " + attack.attackButton.ToString() + " on frame " + attack.GetCurrentFrame());
                    combo.ContinueCombo(attack.attackButton, attack.GetCurrentFrame());
                }

                print("Button Pushed: " + attack.attackButton.ToString());
                attack.AttackWithWeapon();
                cost = attack.energyCost;
                currentFrame = 0;
                current_attack = i;
            }

            i++;
        }

        return cost;
    }

    //timer for the weapons
    public bool TimerUpdate()
    {
        if (timer < animationSpeed)
            timer += Time.deltaTime;
        else
        {
            currentFrame++;
            //print("Current Frame: " + currentFrame);
            timer = 0.0f;
            return true;
        }

        return false;
    }

    public bool comboCheck()
    {
        int i = 0;

        foreach (WeaponAttack attack in attackList)
        {
            if (Input.GetKeyDown(attack.attackButton))
            {
                foreach (ComboScript combo in comboList)
                {
                    //if a combo is continued correctly, reset frame and execute attack
                    print("Combo check with button " + attack.attackButton.ToString() + " on frame " + currentFrame);
                    if(combo.ContinueCombo(attack.attackButton, currentFrame))
                    {
                        attackList[current_attack].CancelAttack();
                        currentFrame = 0;

                        if (comboConfirm == false)
                        {
                            print("Combo Confirmed");
                            attack.AttackWithWeapon();
                            comboConfirm = true;
                            current_attack = i;
                        }
                        else
                        {
                            print("Combo Finisher");
                            combo.ComboAttack();
                            return true;
                        }

                    }
                }
            }

            i++;
        }

        return false;

    }



    //returns true if the weapon is no longer attacking
    public bool WeaponCooldown()
    {
        bool cool = true;

        foreach(WeaponAttack attack in attackList)
        {
            //if a weapon  attack is playing
            if (attack.AttackTrigger() || comboList[0].ComboTrigger())
                cool = false;
        }

        if (cool == true)
        {
            comboList[0].comboEnabled = false;
            comboList[0].comboIndex = 0;
            comboConfirm = false;
        }

        return cool;
    }
}
