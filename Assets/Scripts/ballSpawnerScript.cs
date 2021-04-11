using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class ballSpawnerScript : MonoBehaviour
{
    Vector2 m_CurveOriginPos;
    public int ballCount;
    [SerializeField] GameObject RedBall;
    [SerializeField] GameObject YellowBall;
    [SerializeField] GameObject GreenBall;
    [SerializeField] GameObject BlueBall;


    // Start is called before the first frame update
    GameObject ball;
    void Start()
    {
        m_CurveOriginPos = GameObject.Find("Lvl 1 - 1").GetComponent<PathCreator>().path[0];
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                ball = Instantiate(RedBall, m_CurveOriginPos, Quaternion.identity);
                break;
            case 2:
                ball = Instantiate(YellowBall, m_CurveOriginPos, Quaternion.identity);
                break;
            case 3:
                ball = Instantiate(GreenBall, m_CurveOriginPos, Quaternion.identity);
                break;
            case 4:
                ball = Instantiate(BlueBall, m_CurveOriginPos, Quaternion.identity);
                break;
        }
        ball.tag = "headBall";
        EventManager.Instance.Raise(new BallHasBeenAddedToQueueEvent() { ball = ball });
        spawnAllBalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnAllBalls(){
        for (int i = 0; i < ballCount - 1; i++)
        {
            int random = Random.Range(1, 11);
            switch (ball.GetComponent<ballMovement>().color)
            {
                case "red":
                    if (random <= 7)
                    {
                        ball = Instantiate(RedBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 8)
                    {
                        ball = Instantiate(YellowBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 9)
                    {
                        ball = Instantiate(GreenBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else { ball = Instantiate(BlueBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break; }

                case "yellow":
                    if (random <= 7)
                    {
                        ball = Instantiate(YellowBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 8)
                    {
                        ball = Instantiate(GreenBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 9)
                    {
                        ball = Instantiate(BlueBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else { ball = Instantiate(RedBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break; }

                case "green":
                    if (random <= 7)
                    {
                        ball = Instantiate(GreenBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 8)
                    {
                        ball = Instantiate(BlueBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 9)
                    {
                        ball = Instantiate(RedBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else { ball = Instantiate(YellowBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break; }

                case "blue":
                    if (random <= 7)
                    {
                        ball = Instantiate(BlueBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 8)
                    {
                        ball = Instantiate(RedBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else if (random == 9)
                    {
                        ball = Instantiate(YellowBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break;
                    }
                    else { ball = Instantiate(GreenBall, ball.transform.position + new Vector3(0, .5f, 0), Quaternion.identity); break; }
            }
            EventManager.Instance.Raise(new BallHasBeenAddedToQueueEvent() { ball = ball });
        }
    }

}
