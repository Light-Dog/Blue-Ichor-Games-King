using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public List<WeaponController> weapons;
    public int equipedWeapon = 0;

    public Image energyBar;
    float energyPercent = 1.0f;
    float timer = 0f;

    bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking == false)
        {
            float cost = weapons[equipedWeapon].WeaponCheck();
            if (cost > 0.0f)
                attacking = true;
            energyPercent -= cost;
        }
        else
        {
            weapons[equipedWeapon].comboCheck();
            if (weapons[equipedWeapon].WeaponCooldown())
                attacking = false;
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
