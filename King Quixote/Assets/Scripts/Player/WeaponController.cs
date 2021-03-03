using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string weaponName;

    public Sprite[] weaponIdles;
    public Sprite[] weaponRun;
    public Sprite weaponJump;
    public Sprite weaponLand;

    public List<AttackAction> attacks;
    public List<ComboAction> combos;
    public BlockAction block;
    public DashAction dash;
    public CounterAction counter;

    public int damage = 0;
    public float animationSpeed = 0.2f;

    float timer = 0.0f;
    int currentFrame = 0;
    public WeaponAction currentAction = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float WeaponCheck(float energy)
    {
        //check should look for attack input, combos, blocking
        //function to check for inputs, bool to track if an action is running
        //while an attack is playing, check for combos
        //needs to output energy cost
        //holding block should drain stamina
        float weaponCost = 0.0f;

        if(currentAction == null)
            weaponCost = ActionStart(energy);
        else
        {
            if(currentAction.actionType == WeaponAction.typeOfAction.Attack)
                ActionUpdate();

            if(currentAction.actionType == WeaponAction.typeOfAction.Attack || currentAction.actionType == WeaponAction.typeOfAction.Combo || currentAction.actionType == WeaponAction.typeOfAction.Counter)
            {
                if(dash.DashStart(energy))
                {
                    currentAction.CancelAction();
                    currentAction = dash;
                    return dash.energyCost;
                }
            }

            if(currentAction.actionType == WeaponAction.typeOfAction.Block)
            {
                if(counter.CounterStart())
                {
                    currentAction.CancelAction();
                    currentAction = counter;
                    return counter.energyCost;
                    
                }
            }

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

    public bool IsBlocking()
    {
        if(currentAction)
        {
            if (currentAction.actionType == WeaponAction.typeOfAction.Block)
                return true;
        }

        return false;
    }

    public void CancelAction()
    {
        ResetActions();
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------

    private float ActionStart(float energyPercent)
    {
        if(dash.DashStart(energyPercent))
        {
            currentAction = dash;
            return dash.energyCost;
        }

        //check for attacks and all combos that start with that attack
        foreach (AttackAction attack in attacks)
        {
            if(attack.EnergyComapre(energyPercent))
            {
                if (attack.AttackStart())
                {
                    //attack action is active
                    currentAction = attack;
                    currentFrame = 0;
                    foreach (ComboAction combo in combos)
                    {
                        if (combo.ContinueCombo(attack.buttonName, currentFrame))
                            print("Combo Started");
                    }

                    return attack.energyCost;
                }
            }
        }

        //check for block
        if (block.BlockCheck(energyPercent))
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
                foreach (ComboAction combo in combos)
                {
                    if (combo.ComboEnabled())
                    {
                        combo.ContinueCombo(attack.buttonName, currentFrame);

                        if (combo.CheckComboComplete())
                        {
                            currentAction.CancelAction();
                            currentAction = combo;
                        }
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
        foreach (AttackAction attack in attacks)
        {
            if (attack.CheckActive())
                cool = false;
        }
        foreach (ComboAction combo in combos)
        {
            if (combo.CheckActive())
                cool = false;
        }

        if (block.CheckButtonHold())
            cool = false;

        if (dash.CheckActive())
            cool = false;

        if (counter.CheckActive())
            cool = false;

        return cool;
    }
}
