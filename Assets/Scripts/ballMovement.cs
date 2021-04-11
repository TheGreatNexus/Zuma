using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ballMovement : MonoBehaviour
{
    public Path path;
    List<Vector2> pathCurve = new List<Vector2>();
    List<GameObject> ballList = new List<GameObject>();
    int headPos = 0;
    Vector2[] points;
    public string color;


    // Start is called before the first frame update
    void Start()
    {
        ballList = GameManager.getBallList();
        SetPath();

    }

    private void MoveHead(int pathIndex)
    {
        if (/*this.tag == "headBall" && */pathIndex <= pathCurve.Count)
        {
            Vector3 target = points[pathIndex];
            Vector3 directionToMove = target - transform.position;
            directionToMove = directionToMove.normalized * Time.deltaTime * 0.5f;
            transform.position = transform.position + directionToMove;
            float zRotation = Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg + 90.0f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            if (Mathf.Round(this.transform.position.x) == Mathf.Round(points[pathIndex].x) && Mathf.Round(this.transform.position.y) == Mathf.Round(points[pathIndex].y)) { headPos++; }
        }
        // else if (pathIndex <= pathCurve.Count)
        // {
        //     int targetIndex = ballList.FindIndex(ball => ball == this.gameObject)-1;
        //     Vector3 target = ballList[targetIndex].transform.position;
        //     Vector3 directionToMove = target - transform.position;
        //     directionToMove = directionToMove.normalized * Time.deltaTime * 0.5f;
        //     transform.position = transform.position + directionToMove;
        // }
        //   Debug.Log("Point n° : " + (pathIndex + 1));
        //  Debug.Log("Pos X : " + points[pathIndex + 1].x);
        //   Debug.Log("Pos Y : " + points[pathIndex + 1].y);
    }

    private void SetPath()
    {
        points = GameObject.Find("Lvl 1 - 1").GetComponent<PathCreator>().path.CalculateEvenlySpacedPoints(0.5f, 1f);
        foreach (var point in points)
        {
            pathCurve.Add(point);
        }
        //Debug.Log(pathCurve.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHead(headPos);
        //Debug.Log("lol " + points[pathIndex + 1]);

    }

    public void setHead(int head){
        this.headPos = head;
    }
    public int getHead()
    {
        return this.headPos;
    }
}
