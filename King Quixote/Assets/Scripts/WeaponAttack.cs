using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    //frames for attack animation
    public Sprite[] attackSprites;
    public int currentSprite = 0;
    public int maxSpriteSize = 0;
    public float animationSpeed = 0.2f;
    public float timer = 0.0f;
    public int activeFrame = 0;

    //2d box collider
    public Collider2D lanceCollider;
    public int damage = 2;

    //player access
    public GameObject player;
    //did attack bool
    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        print("Weapon Start");

        player = GameObject.FindGameObjectWithTag("Player");
        //lanceCollider = gameObject.GetComponent<Collider2D>();

        attack = false;
        lanceCollider.enabled = false;

        maxSpriteSize = attackSprites.Length;

        activeFrame--;
    }

    // Update is called once per frame
    void Update()
    {
        print("Weapon Update");

        //check for button press, and if so set attack to true
        if (Input.GetKeyUp(KeyCode.F) == true)
        {
            attack = true;

            //Pause the idle animation
            player.GetComponent<AnimationCycle>().PauseAnimation(true);

            print("Input Recieved, attacking now");
        }

        //if the player attacks...
        if(attack == true)
        {
            print("Attacking....");

            if(activeFrame == currentSprite)
            {
                //turn on the collider for the lance
                lanceCollider.enabled = true;
            }
            else
            {
                //turn on the collider for the lance
                lanceCollider.enabled = false;
            }

            //play the lance animation
            player.GetComponent<SpriteRenderer>().sprite = attackSprites[currentSprite];

            if (currentSprite < maxSpriteSize)
            {
                if(timer < animationSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    currentSprite++;
                    timer = 0.0f;
                }
            }

            //disabel lance collider after animation finishes
            if (currentSprite == maxSpriteSize)
            {
                currentSprite = 0;
                timer = 0.0f;
                attack = false;
                player.GetComponent<AnimationCycle>().PauseAnimation(false);
            }

        }
    }
}
