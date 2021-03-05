﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    public float runSpeed = 40.0f;
    public float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    public LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    public LayerMask m_WhatIsBoxes;
    public Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded

    GoToScene sceneUI;

    //-------------------------------------------------------------------------------------------------------------------------------------------

    [Header("Player Stats")]
    public int health = 10;

    public GameObject barImages;
    Image energyBar;
    Image healthBar;

    public GameObject healthImages;
    Image[] hearts;

    public bool dead = false;

    public bool paused = false;

    bool buff = false;

    [Header("Weapons")]
    public List<WeaponController> weapons;
    public int equipedWeapon = 0;

    public GameObject icon;
    public Sprite[] weaponImages;
    

    public float energyPerSecondBack = .1f;

    float energyPercent = 1.0f;
    float timer = 0.0f;

    //-------------------------------------------------------------------------------------------------------------------------------------------

    Vector3 m_Velocity = Vector3.zero;
    Rigidbody2D m_Rigidbody2D;

    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public bool moving = false;
    bool m_Grounded = true;            // Whether or not the player is grounded.
    bool airControl = true;
    bool jump = false;

    public float invincibleTime = .2f;
    bool damaged = false;
    float iFrameTimer = 0.0f;

    public bool drawCollider = false;

    float horizontalMove = 0.0f;


    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        Image[] uiBars = barImages.GetComponentsInChildren<Image>();
        energyBar = uiBars[0];
        healthBar = uiBars[1];

        hearts = healthImages.GetComponentsInChildren<Image>();
        paused = false;
    }

    void Update()
    {
        if(!paused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                health = 0;
            if (Input.GetKeyDown(KeyCode.Backspace) || FindObjectOfType<KingMe>().CoinsCollected())
            {
                BuffPlayer();
            }

            if (InputManager.GetKeyDown("Change Weapon"))
            {
                ChangeWeapon();
            }

            if (!dead)
            {
                GroundCheck();

                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                if (horizontalMove > 0.0f || horizontalMove < 0.0f)
                    moving = true;
                else
                    moving = false;

                if (Input.GetButtonDown("Jump") && m_Grounded)
                {
                    jump = true;
                    gameObject.GetComponent<AnimationCycle>().StartJump();
                }

                if (!m_Grounded && m_Rigidbody2D.velocity.y <= 0f)
                    gameObject.GetComponent<AnimationCycle>().PeakJump();

                if (m_Grounded)
                    gameObject.GetComponent<AnimationCycle>().Landed();

                //if(energyPercent >= .1f)
                energyPercent -= weapons[equipedWeapon].WeaponCheck(energyPercent);

                StatusUpdate();

            }
        }
       


    }

    private void FixedUpdate()
    {
        if(weapons[equipedWeapon].ActionCheck())
        {
            Move(horizontalMove * Time.deltaTime, jump);
            jump = false;
        }
    }

    public int DealDamage()
    {
        return weapons[equipedWeapon].damage;
    }

    public void BuffPlayer()
    {
        if(!buff)
        {
            m_JumpForce *= 1.5f;
            runSpeed *= 1.5f;
            energyPerSecondBack = 50f;
            buff = true;

            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 0, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        float damagePercentage = damage;

        if(damaged == false)
        {
            if (weapons[equipedWeapon].IsBlocking())
                damagePercentage = damagePercentage * weapons[equipedWeapon].block.blockPercentage;

            print("Damage Taken: " + damage);

            health -= (int)damagePercentage;
            damagePercentage = damagePercentage / 10.0f;
            healthBar.fillAmount -= damagePercentage;

            //camShake.Shake(.15f, 4f);

            damaged = true;
        }
    }

    private void StatusUpdate()
    {
        if (energyBar != null)
        {
            energyBar.fillAmount = energyPercent;
        }

        //4 energy per second back
        if (timer >= 1.0f)
        {
            timer = 0.0f;

            if (energyPercent < 1.0f)
                energyPercent += energyPerSecondBack;
        }
        else
            timer += Time.deltaTime;

        if (damaged)
        {
            if (iFrameTimer < invincibleTime)
            {
                iFrameTimer += Time.deltaTime;
            }
            else
            {
                iFrameTimer = 0.0f;
                damaged = false;
            }
        }

        if(health <= 0)
        {
            gameObject.GetComponent<AnimationCycle>().DeathCheck();
            dead = true;

            //Debug.Break();

            print("Trigger Death animation and start transition");
            FindObjectOfType<GoToScene>().DeathToMenu();
        }
        else if(health > 7)
        {
            hearts[0].enabled = true;
            hearts[1].enabled = false;
            hearts[2].enabled = false;
        }
        else if(health <= 7 && health > 3)
        {
            hearts[0].enabled = false;
            hearts[1].enabled = true;
            hearts[2].enabled = false;
        }
        else if(health <= 3)
        {
            hearts[0].enabled = false;
            hearts[1].enabled = false;
            hearts[2].enabled = true;
        }

        float heal = (float)health / 10f;
        healthBar.fillAmount = heal;
    }

    private void ChangeWeapon()
    {
        if (equipedWeapon < 2)
            equipedWeapon++;
        else
            equipedWeapon = 0;

        icon.GetComponent<Image>().sprite = weaponImages[equipedWeapon];
        UpdateSprites();
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------

    //moves the player
    private void Move(float move, bool jump)
    {   
        if (airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
                Flip();            // If the input is moving the player right and the player is facing left...
            else if (move < 0 && m_FacingRight)
                Flip();             // Otherwise if the input is moving the player left and the player is facing right...
        }
        // If the player should jump...
        if (jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void GroundCheck()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //print("Collided with: " + colliders[i].gameObject.name);
                m_Grounded = true;
                //jump = false;
            }
        }
        colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsBoxes);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                print("Collided with: " + colliders[i].gameObject.name);
                m_Grounded = true;
                //jump = false;
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void UpdateSprites()
    {
        //idle
        if(weapons[equipedWeapon].weaponIdles.Length > 0)
            gameObject.GetComponent<AnimationCycle>().idleFrames = weapons[equipedWeapon].weaponIdles;

        //run
        if(weapons[equipedWeapon].weaponRun.Length > 0)
            gameObject.GetComponent<AnimationCycle>().moveFrames = weapons[equipedWeapon].weaponRun;

        //jump & land
        if(weapons[equipedWeapon].weaponJump)
            gameObject.GetComponent<AnimationCycle>().jumping = weapons[equipedWeapon].weaponJump;

        if(weapons[equipedWeapon].weaponLand)
            gameObject.GetComponent<AnimationCycle>().falling = weapons[equipedWeapon].weaponLand;

        gameObject.GetComponent<AnimationCycle>().currentFrame = 0;
    }
}
