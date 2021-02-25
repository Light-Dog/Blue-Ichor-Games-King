using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject Projectile;
    private bool shot = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AnimationCycle>())
        {
            if (gameObject.GetComponent<AnimationCycle>().currentFrame == 2)
            {
                if (shot == false)
                {
                    Instantiate(Projectile);
                    shot = true;
                }
            }

            if (gameObject.GetComponent<AnimationCycle>().currentFrame == 1)
            {
                shot = false;
            }
        }
    }
}
