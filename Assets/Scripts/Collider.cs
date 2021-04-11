using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    private bool asEnteredSmth = false;
    List<GameObject> ballList = new List<GameObject>();
    GameObject RedBall;
    GameObject BlueBall;
    GameObject YellowBall;
    GameObject GreenBall;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVect = -transform.up * 10f * Time.fixedDeltaTime;
        transform.position += moveVect;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (asEnteredSmth == false)
        {
            Debug.Log("test");
            ballList = GameManager.getBallList();
            if (other.transform.localPosition.y < other.transform.InverseTransformPoint(this.transform.position).y )
            {
                Debug.Log("coucou on est censé etre en dessous");
                GameObject newBall = Instantiate(RedBall, new Vector3(other.transform.localPosition.x, other.transform.localPosition.y, 0), Quaternion.identity);
                newBall.GetComponent<ballMovement>().setHead(other.GetComponent<ballMovement>().getHead() - 1);
                other.transform.position += new Vector3(0, 0.25f, 0);
                //other.transform.position += new Vector3(0, -0.25f, 0);
                asEnteredSmth = true;
                Destroy(gameObject);
            }
            else if (other.transform.localPosition.y > other.transform.InverseTransformPoint(this.transform.position).y)
            {
                Debug.Log("coucou on est censé etre au dessus");
                GameObject newBall = Instantiate(RedBall, new Vector3(other.transform.localPosition.x, other.transform.localPosition.y - 0.25f, 0), Quaternion.identity);
                newBall.GetComponent<ballMovement>().setHead(other.GetComponent<ballMovement>().getHead() - 1);
                //other.transform.position += new Vector3(0, 0.25f, 0);
                asEnteredSmth = true;
                Destroy(gameObject);
            }
        }
    }

    public void setPrefabs(GameObject redBall, GameObject blueBall, GameObject yellowBall, GameObject greenBall)
    {
        this.BlueBall = blueBall;
        this.RedBall = redBall;
        this.YellowBall = yellowBall;
        this.GreenBall = greenBall;
    }

}
