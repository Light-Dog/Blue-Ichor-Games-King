using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyPather pathfinding;
    public bool facingRight = false;

    /*
    //current node
    Node currentNode = null;
    //node map
    NodeMap moveMap = null;
    //path
    public List<Node> path;
    public Node goal = null;
    */

    public GameObject blood;
    public int numBleed = 3;
    private float shotForce = 2.5f;

    [Header("Enemy Variables")]
    public int health = 10;
    public List<EnemyAction> actions;
    EnemyAction currentAction = null;

    public bool moving = false;
    public bool drawCollider = false;

    public bool dontDie = false;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<EnemyPather>())
            pathfinding = gameObject.GetComponent<EnemyPather>();

        /*
        moveMap = FindObjectOfType<NodeMap>();
        currentNode = moveMap.FindClosesttNode(gameObject.transform);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Health check
        EnemyStatusUpdate();

        //Check if moving (for animation control)
        moving = pathfinding.moving;

        //Flips Image if needed
        if (pathfinding.direction > 0 && facingRight == false)
            Flip();
        else if (pathfinding.direction < 0 && facingRight == true)
            Flip();

        //Checks attack range
        if(pathfinding.InAttackRange())
        {
            //checks if there is an attack playing
            if(currentAction)
            {
                if (!currentAction.IsActive())
                    currentAction = null;
            }
            else
            {
                //if they have an action
                if (actions.Capacity != 0)
                {
                    //check range of attack
                    if (actions[0].RangeCheck())
                    {
                        actions[0].Activate();
                        currentAction = actions[0];
                    }
                }


            }

        }


    }

    private void EnemyStatusUpdate()
    {
        if(health <= 0 && !dontDie)
        {
            Destroy(gameObject);
        }
    }

    public void Bleed()
    {
        for (int i = 0; i < numBleed; i++)
        {
            Vector2 upShot1 = new Vector2(Random.Range(-0.05f, 0.25f), Random.Range(.65f, 1.2f));
            Vector2 upShot2 = new Vector2(Random.Range(-.25f, 0.05f), Random.Range(.65f, 1.2f));

            GameObject temp1 = Instantiate(blood, gameObject.transform.position, gameObject.transform.rotation);
            GameObject temp2 = Instantiate(blood, gameObject.transform.position, gameObject.transform.rotation);

            temp1.GetComponent<Rigidbody2D>().AddForce(upShot1 * shotForce, ForceMode2D.Impulse);
            temp1.GetComponent<Rigidbody2D>().AddTorque(180, ForceMode2D.Impulse);
            temp1.GetComponent<KillTimer>().StartTimer();

            temp2.GetComponent<Rigidbody2D>().AddForce(upShot2 * shotForce, ForceMode2D.Impulse);
            temp2.GetComponent<Rigidbody2D>().AddTorque(-180, ForceMode2D.Impulse);
            temp2.GetComponent<KillTimer>().StartTimer();
        }
    }

    public bool TimerUpdate()
    {
        if (timer < animationSpeed)
            timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            return true;
        }

        return false;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
