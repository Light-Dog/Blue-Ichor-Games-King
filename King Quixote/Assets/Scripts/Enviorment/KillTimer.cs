using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour
{
    public float timer = 0.0f;
    private float walker = 0.0f;
    private bool kill = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kill)
        {
            if (walker < timer)
                walker += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        kill = true;
    }
}
