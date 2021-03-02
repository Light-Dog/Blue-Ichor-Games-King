using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitDamage : MonoBehaviour
{
    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Land") || other.GetComponent<Breakable>()) 
        {
            Destroy(gameObject);
        }
    }
}
