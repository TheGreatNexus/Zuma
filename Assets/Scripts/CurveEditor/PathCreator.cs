using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    //Couleur settings
    public Color anchorColor = Color.red;
    public Color controlColor = Color.white;
    public Color segmentColor = Color.green;
    public Color selectedSegmentColor = Color.blue;
    public float anchorDiameter = 0.1f;
    public float controlDiameter = 0.07f;
    public bool displayControlPoints = true;

    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    public Path GetPath
    {
        get
        {
            return path;
        }
    }
    void Reset()
    {
        CreatePath();
    }
}
