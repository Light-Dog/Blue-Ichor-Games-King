using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitParticle : MonoBehaviour
{
    public GameObject particle1;
    public GameObject particle2;
    public GameObject particle3;
    public GameObject particle4;
    public GameObject particle5;
    private GameObject newParticle;
    public int numberOfParticles = 5;
    private int chosenParticle;
    private int previousParticle;
    public float tinyTimerMax = 0.2f;
    //private float tinyTimer = 0f;
    public float smallTimerMax = 1.0f;
    private float smallTimer = 0f;
    public float bigTimerMax = 2.0f;
    public float bigTimer = 0f;
    private bool usingBigTimer = true;
    public bool launched = false;



    // Start is called before the first frame update
    void Start()
    {
        GetRandomParticle();
        //SpawnParticle();


    }

    void GetRandomParticle()
    {
        chosenParticle = Random.Range(1, numberOfParticles);

        if (chosenParticle == 1)
        {
            newParticle = particle1;
        }
        if (chosenParticle == 2)
        {
            newParticle = particle2;
        }
        if (chosenParticle == 3)
        {
            newParticle = particle3;
        }
        if (chosenParticle == 4)
        {
            newParticle = particle4;
        }
        if (chosenParticle == 5)
        {
            newParticle = particle5;
        }

        if (chosenParticle == previousParticle)
        {
            GetRandomParticle();
        }


    }

    void SpawnParticle()
    {
        Instantiate(newParticle, transform);
        GetRandomParticle();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AnimationCycle>().currentFrame == gameObject.GetComponent<AnimationCycle>().maxFrame)
        {
            if (launched == false)
            {
                Instantiate(particle1, transform);
                Instantiate(particle2, transform);
                Instantiate(particle3, transform);
                Instantiate(particle4, transform);
                Instantiate(particle5, transform);
                Instantiate(newParticle, transform);
                Instantiate(newParticle, transform);
                launched = true;
            }
        }
        else
        {
            if (launched == true)
            {
                launched = false;
            }
        }

        /*
        if (usingBigTimer == false)
        {
            if (smallTimer < smallTimerMax)
            {
                smallTimer += Time.deltaTime;

                if (tinyTimer < tinyTimerMax)
                {
                    tinyTimer += Time.deltaTime;
                }
                else
                {
                    tinyTimer = 0f;
                    GetRandomParticle();
                    SpawnParticle();
                }
            }
            else
            {
                smallTimer = 0f;
                usingBigTimer = true;
            }
        }

        else
        {
            if (bigTimer < bigTimerMax)
            {
                bigTimer += Time.deltaTime;
            }
            else
            {
                bigTimer = 0f;
                usingBigTimer = false;
            }
        }*/
    }
}
