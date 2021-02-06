using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed;
    float moveSmoothing = .05f;
    Rigidbody2D body;
    Vector3 vecRef = Vector3.zero;

    //current node
    Node currentNode = null;
    //node map
    NodeMap moveMap = null;
    //path
    public List<Node> path;
    public Node goal = null;

    [Header("Enemy Variables")]
    public int health = 10;
    public List<EnemyAction> actions;

    public bool moving = false;
    public bool drawCollider = false;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody2D>())
            body = gameObject.GetComponent<Rigidbody2D>();

        moveMap = FindObjectOfType<NodeMap>();
        currentNode = moveMap.FindClosesttNode(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //decision making
        EnemyStatusUpdate();
        Move();

        //move
        //Create move controller class that has a list of move nodes as a path to follow
        //might look into giving enemies rigidbodies
        if (Input.GetKeyDown(KeyCode.N))
        {
            moving = !moving;
            //FindObjectOfType<PlayerController>().transform;
        }

        //attack
        if (actions.Capacity != 0)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                actions[0].Activate();
                goal = moveMap.GeneratePath(FindObjectOfType<PlayerController>().GetComponent<Transform>(), currentNode, path);
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

    private void Move()
    {
        if(moving && body)
        {
            Vector3 targetVelocity = new Vector2(moveSpeed * 1f, body.velocity.y);
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref vecRef, moveSmoothing);
        }
        else
        {
            if(body)
                body.velocity = Vector3.zero;
        }
    }
}
