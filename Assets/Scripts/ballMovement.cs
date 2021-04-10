﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ballMovement : MonoBehaviour
{
    public Path path;

    List<Vector2> pathCurve = new List<Vector2>();
    int headPos = 0;
    Vector2[] points;

    Rigidbody2D rb2D;
    public string color;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        SetPath();
        
    }

    private void MoveHead(int pathIndex)
    {
        if(this.tag == "head" && pathIndex <= pathCurve.Count)
        {
            transform.DOMoveX((points[pathIndex + 1].x), 1);
            transform.DOMoveX((points[pathIndex + 1].y), 1);
            headPos++;
        }
        else if(pathIndex <= pathCurve.Count)
        {
            headPos++;
        }
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
    void Update()
    {
        MoveHead(headPos);
        //Debug.Log("lol " + points[pathIndex + 1]);

    }
}
