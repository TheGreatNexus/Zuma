using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Vector3 mousePos = new Vector3();
    Transform target; //Assign to the object you want to rotate
    Vector3 objPos = new Vector3();
    float angle;


    void Start()
    {
        target = this.transform;
    }
    void SetPlayerPosition()
    {
            mousePos = Input.mousePosition;
            mousePos.z = 0; //The distance between the camera and object
            objPos = Camera.main.WorldToScreenPoint(target.position);
            mousePos.x = mousePos.x - objPos.x;
            mousePos.y = mousePos.y - objPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle + 90)));
    }


    void FixedUpdate()
    {
        SetPlayerPosition();
    }

}
