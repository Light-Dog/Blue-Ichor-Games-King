using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotate : MonoBehaviour
{
    public GameObject Pos1Obj;
    public GameObject Pos2Obj;
    public GameObject Pos3Obj;
    private Transform Pos1;
    private Transform Pos2;
    private Transform Pos3;

    public int currentPos = 1;
    public float swingSpeed = 0.1f;
    bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        Pos1 = Pos1Obj.transform;
        Pos2 = Pos2Obj.transform;
        Pos3 = Pos3Obj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            if (currentPos == 1)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Pos1.localRotation, swingSpeed);
                if (transform.localRotation == Pos1.localRotation)
                    move = false;
            }
            else if (currentPos == 2)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Pos2.localRotation, swingSpeed);
                if (transform.localRotation == Pos2.localRotation)
                    move = false;
            }
            else if (currentPos == 3)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Pos3.localRotation, swingSpeed);
                if (transform.localRotation == Pos2.localRotation)
                    move = false;
            }
        }

        

    }

    public void MouseToButton(int armPos)
    {
        //set new arm and tell it to move
        move = true;
        currentPos = armPos;
    }
    
}
