using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] GameObject RedBall;
    [SerializeField] GameObject YellowBall;
    [SerializeField] GameObject GreenBall;
    [SerializeField] GameObject BlueBall;
    Vector3 mousePos = new Vector3();
    Transform target; //Assign to the object you want to rotate
    Vector3 objPos = new Vector3();
    float angle;
    Vector3 ballToShootPos;
    Vector3 reloadBallPos;
    GameObject ballToShoot;
    GameObject reloadBall;


    void Start()
    {
        ballToShootPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, 0);
        reloadBallPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, 0);
        target = this.transform;
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                ballToShoot = Instantiate(RedBall, ballToShootPos, Quaternion.identity);
                break;
            case 2:
                ballToShoot = Instantiate(YellowBall, ballToShootPos, Quaternion.identity);
                break;
            case 3:
                ballToShoot = Instantiate(GreenBall, ballToShootPos, Quaternion.identity);
                break;
            case 4:
                ballToShoot = Instantiate(BlueBall, ballToShootPos, Quaternion.identity);
                break;
        }
        random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                reloadBall = Instantiate(RedBall, reloadBallPos, Quaternion.identity);
                break;
            case 2:
                reloadBall = Instantiate(YellowBall, reloadBallPos, Quaternion.identity);
                break;
            case 3:
                reloadBall = Instantiate(GreenBall, reloadBallPos, Quaternion.identity);
                break;
            case 4:
                reloadBall = Instantiate(BlueBall, reloadBallPos, Quaternion.identity);
                break;
        }
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

    void SetBallsRotation()
    {
        ballToShoot.transform.rotation = this.transform.rotation;
        reloadBall.transform.rotation = this.transform.rotation;
    }
    void Update()
    {
        reloadBallPos = new Vector3(0, this.transform.localPosition.y + .75f, 0);
        ballToShootPos = new Vector3(0, this.transform.localPosition.y - .8f, 0);
        ballToShoot.transform.position = transform.TransformPoint(ballToShootPos);
        reloadBall.transform.position = transform.TransformPoint(reloadBallPos);
        if(Input.GetMouseButtonDown(0)){
            ClickedAction();
        }
    }

    void FixedUpdate()
    {
        SetPlayerPosition();
        SetBallsRotation();
    }

    void ClickedAction()
    {
        ballToShoot.AddComponent<Collider>();
        ballToShoot = reloadBall;
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                reloadBall = Instantiate(RedBall, reloadBallPos, Quaternion.identity);
                break;
            case 2:
                reloadBall = Instantiate(YellowBall, reloadBallPos, Quaternion.identity);
                break;
            case 3:
                reloadBall = Instantiate(GreenBall, reloadBallPos, Quaternion.identity);
                break;
            case 4:
                reloadBall = Instantiate(BlueBall, reloadBallPos, Quaternion.identity);
                break;
        }
    }

}
