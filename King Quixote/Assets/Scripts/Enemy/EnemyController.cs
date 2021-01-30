using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed;

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

    }

    // Update is called once per frame
    void Update()
    {
        //decision making
        EnemyUpdate();

        //move

        //attack
        if(actions.Capacity != 0)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                actions[0].Activate();
            }
        }
    }

    private void EnemyUpdate()
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
