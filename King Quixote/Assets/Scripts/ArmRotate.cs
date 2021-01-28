using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotate : MonoBehaviour
{
    public GameObject Pos1Obj;
    public GameObject Pos2Obj;
    private Transform Pos1;
    private Transform Pos2;
    public int currentPos = 1;
    public float swingSpeed = 0.1f;
    public float screenCenter = 256f;

    // Start is called before the first frame update
    void Start()
    {
        Pos1 = Pos1Obj.transform;
        Pos2 = Pos2Obj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y > screenCenter)
        {
            currentPos = 1;
            Debug.Log("A "+Input.mousePosition.y);
        }
        else
        {
            currentPos = 2;
            Debug.Log("B "+Input.mousePosition.y);
        }

        if (currentPos == 1)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Pos1.localRotation, swingSpeed);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Pos2.localRotation, swingSpeed);
        }

    }
    /*
    void SetAngle()
    {
        if (currentPos == 1)
        {
            currentPos += 1;
        }
        else
        {
            currentPos = 1;
        }
    }*/
}
