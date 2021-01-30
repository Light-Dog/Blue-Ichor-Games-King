using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string weaponName;

    public List<AttackAction> attacks;
    public List<ComboAction> combos;
    public BlockAction block;

    public int damage = 0;
    public float animationSpeed = 0.2f;

    float timer = 0.0f;
    int currentFrame = 0;
    WeaponAction currentAction = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float WeaponCheck()
    {
        //check should look for attack input, combos, blocking
        //function to check for inputs, bool to track if an action is running
        //while an attack is playing, check for combos
        //needs to output energy cost
        //holding block should drain stamina
        float weaponCost = 0.0f;

        if(currentAction == null)
            weaponCost = ActionStart();
        else
        {
            if(currentAction.actionType == WeaponAction.typeOfAction.Attack)
                ActionUpdate();

            if (CheckCooldown())
                ResetActions();
            
        }

        return weaponCost;
    }

    public bool ActionCheck()
    {
        if (currentAction == null)
            return true;

        return false;
    }

    private float ActionStart()
    {
        //check for attacks and all combos that start with that attack
        foreach(AttackAction attack in attacks)
        {
            if(attack.AttackStart())
            {
                //attack action is active
                currentAction = attack;
                currentFrame = 0;
                foreach(ComboAction combo in combos)
                {
                    if (combo.ContinueCombo(attack.attackButton, currentFrame))
                        print("Combo Started");
                }

                return attack.energyCost;
            }
        }

        //check for block
        if(block.BlockCheck())
        {
            currentAction = block;
            return block.energyCost;
        }
        

        return 0.0f;
    }

    private void ActionUpdate()
    {
        foreach (AttackAction attack in attacks)
        {
            if (attack.AttackCheck())
            {
                foreach(ComboAction combo in combos)
                {
                    if(combo.ComboEnabled())
                    {
                        combo.ContinueCombo(attack.attackButton, currentFrame);

                        if (combo.CheckComboComplete())
                            currentAction.CancelAction();
                    }
                }
            }
        }
    }

    private void ResetActions()
    {
        currentFrame = 0;
        currentAction = null;

        foreach (AttackAction attack in attacks)
            attack.CancelAction();

        foreach (ComboAction combo in combos)
            combo.ResetCombo();
    }

    private bool CheckCooldown()
    {
        bool cool = true;
        foreach(AttackAction attack in attacks)
        {
            if (attack.CheckActive())
                cool = false;
        }
        foreach(ComboAction combo in combos)
        {
            if (combo.CheckActive())
                cool = false;
        }
        if (block.CheckButtonHold())
            cool = false;

        return cool;
    }

    public bool TimerUpdate()
    {
        if (timer < animationSpeed)
            timer += Time.deltaTime;
        else
        {
            currentFrame++;
            timer = 0.0f;
            return true;
        }

        return false;
    }
}
