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
    bool comboStart;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;
    int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        num_attacks = attackList.Capacity;
        comboStart = false;
        currentFrame = 0;
        current_attack = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //returns true if a weapon attack is pushed;
    public float WeaponCheck()
    {
        float cost = 0.0f;
        int i = 0;

        foreach(WeaponAttack attack in attackList)
        {
            //print("Attacking allowed");

            if (Input.GetKeyDown(attack.attackButton))
            {
                //if a combo hasn't started, aka no input in a bit
                if (comboStart == false)
                {
                    //enable all combos that started
                    foreach(ComboScript combo in comboList)
                    {
                        print("Combo check with button " + attack.attackButton.ToString() + " on frame " + attack.GetCurrentFrame());
                        combo.ContinueCombo(attack.attackButton, attack.GetCurrentFrame());
                    }
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

    public bool timerUpdate()
    {
        if (timer < animationSpeed)
            timer += Time.deltaTime;
        else
        {
            currentFrame++;
            print("Current Frame: " + currentFrame);
            timer = 0.0f;
            return true;
        }

        return false;
    }

    public void comboCheck()
    {
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
                        attack.AttackWithWeapon();
                    }
                }
            }
        }
    }

    //returns true if the weapon is no longer attacking
    public bool WeaponCooldown()
    {
        bool cool = true;

        foreach(WeaponAttack attack in attackList)
        {
            //if a weapon  attack is playing
            if (attack.AttackTrigger())
                cool = false;
        }

        if (cool == true)
        {
            comboList[0].comboEnabled = false;
            comboList[0].comboIndex = 0;
        }

        return cool;
    }
}
