using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyPather pathfinding;

    /*
    //current node
    Node currentNode = null;
    //node map
    NodeMap moveMap = null;
    //path
    public List<Node> path;
    public Node goal = null;
    */

    [Header("Enemy Variables")]
    public int health = 10;
    public List<EnemyAction> actions;
    EnemyAction currentAction = null;

    public bool moving = false;
    public bool drawCollider = false;

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
        //decision making
        EnemyStatusUpdate();

        moving = pathfinding.moving;

        if(pathfinding.InAttackRange())
        {
            if(currentAction)
            {
                if (!currentAction.IsActive())
                    currentAction = null;
            }
            else
            {
                if (actions.Capacity != 0)
                {
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
        if(health <= 0)
        {
            Destroy(gameObject);
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
}
