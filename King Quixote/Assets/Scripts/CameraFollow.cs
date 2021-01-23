using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject myPlayer;
    public float lerpSpeed = 5f;
    public float xOffset = -5f;
    private float myZ;

    // Start is called before the first frame update
    void Start()
    {
        myZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (myCamera != null)
        {
            gameObject.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(myPlayer.transform.position.x - xOffset, myPlayer.transform.position.y, myPlayer.transform.position.z), lerpSpeed);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, myZ);
        }
    }
}
