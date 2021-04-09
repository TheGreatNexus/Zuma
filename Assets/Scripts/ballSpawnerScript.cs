using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class ballSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject RedBall;
    [SerializeField] GameObject YellowBall;
    [SerializeField] GameObject GreenBall;
    [SerializeField] GameObject BlueBall;
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                Instantiate(RedBall, this.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(YellowBall, this.transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(GreenBall, this.transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(BlueBall, this.transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(RedBall, this.transform.position, Quaternion.identity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("here");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exited");
        string color = other.GetComponent<ballMovement>().color;
        int random = Random.Range(0, 11);
        if (random <= 7)
        {
            switch (color)
            {
                case "red":
                    Instantiate(RedBall, this.transform.position, Quaternion.identity);
                    break;
                case "blue":
                    Instantiate(BlueBall, this.transform.position, Quaternion.identity);
                    break;
                case "yellow":
                    Instantiate(YellowBall, this.transform.position, Quaternion.identity);
                    break;
                case "green":
                    Instantiate(GreenBall, this.transform.position, Quaternion.identity);
                    break;
            }
        }
        if (random == 8)
        {
            switch (color)
            {
                case "red":
                    Instantiate(YellowBall, this.transform.position, Quaternion.identity);
                    break;
                case "blue":
                    Instantiate(RedBall, this.transform.position, Quaternion.identity);
                    break;
                case "yellow":
                    Instantiate(GreenBall, this.transform.position, Quaternion.identity);
                    break;
                case "green":
                    Instantiate(BlueBall, this.transform.position, Quaternion.identity);
                    break;
            }
        }
        if (random == 9)
        {
            switch (color)
            {
                case "red":
                    Instantiate(GreenBall, this.transform.position, Quaternion.identity);
                    break;
                case "blue":
                    Instantiate(YellowBall, this.transform.position, Quaternion.identity);
                    break;
                case "yellow":
                    Instantiate(BlueBall, this.transform.position, Quaternion.identity);
                    break;
                case "green":
                    Instantiate(RedBall, this.transform.position, Quaternion.identity);
                    break;
            }
        }
        if (random == 10)
        {
            switch (color)
            {
                case "red":
                    Instantiate(BlueBall, this.transform.position, Quaternion.identity);
                    break;
                case "blue":
                    Instantiate(GreenBall, this.transform.position, Quaternion.identity);
                    break;
                case "yellow":
                    Instantiate(RedBall, this.transform.position, Quaternion.identity);
                    break;
                case "green":
                    Instantiate(YellowBall, this.transform.position, Quaternion.identity);
                    break;
            }
        }

    }
}
