using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CickandDrag : MonoBehaviour
{
    public float TurnSpeed = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(transform.position, -Vector3.up, TurnSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, TurnSpeed * Input.GetAxis("Mouse Y"));
        }
    }
}
