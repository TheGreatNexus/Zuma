using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
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

    void OnTriggerEnter(Collider other)
    {   
        Debug.Log("test");
        ballList = GameManager.getBallList();
        if (other.transform.localPosition.x <= other.transform.InverseTransformPoint(this.transform.position).x)
        {
            Instantiate(RedBall, new Vector3(other.transform.localPosition.x,other.transform.localPosition.y+1,other.transform.localPosition.z), Quaternion.identity);
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
