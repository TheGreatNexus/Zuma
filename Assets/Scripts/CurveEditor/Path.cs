using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector2> points;
    [SerializeField, HideInInspector]
    bool isClosed;
    [SerializeField, HideInInspector]
    bool autoSetControlPoints;

    public Path(Vector2 center)
    {
        points = new List<Vector2>
        {
            center + Vector2.left,
            center + (Vector2.left + Vector2.up)*0.5f,
            center +(Vector2.right + Vector2.down)*0.5f,
            center + Vector2.right
        };
    }
    //Outils de manipulation

    //Reference index de la liste
    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public bool AutoSetControlPoints
    {
        get
        {
            return autoSetControlPoints;
        }

        set
        {
            if(autoSetControlPoints != value)
            {
                autoSetControlPoints = value;
                if (autoSetControlPoints)
                {
                    AutoSetAllControlPoints();
                }
            }
        }
    }

    public bool IsClosed
    {
        get
        {
            return isClosed;
        }

        set
        {
            if(isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);

                    if (autoSetControlPoints)
                    {
                        AutoSetAnchorControlPoints(0);
                        AutoSetAnchorControlPoints(points.Count - 3);
                    }
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);

                    if (autoSetControlPoints)
                    {
                        AutoSetStartAndEndControl();
                    }
                }
            }
        }
    }

    //Recupérer le nombre de points
    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }
    //
    public int NumSegments
    {
        get
        {
            return (points.Count / 3);
        }
    }

    //Methodes
    public void AddSegment(Vector2 anchorPos)
    {
        //Formules - P, un point dans la liste / Pa, position du point d'ancrage - : 
        // (Pn-1 * 2) - Pn-2
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        // (Pn-1 + Pa) / 2
        points.Add((points[points.Count - 1] + anchorPos)* 0.5f);
        // On ajoute le dernier point à à la référence de PA
        points.Add(anchorPos);

        if(autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public void SplitSegment(Vector2 anchorPos, int segmentIndex)
    {
        points.InsertRange(segmentIndex * 3 + 2, new Vector2[] { Vector2.zero, anchorPos, Vector2.zero });
        if(autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(segmentIndex * 3 + 3);
        }
        else
        {
            AutoSetAnchorControlPoints(segmentIndex * 3 + 3);
        }
    }

    public void DeleteSegment(int anchorIndex)
    {
        if (NumSegments > 2 || !isClosed && NumSegments > 1)
        {
            if (anchorIndex == 0)
            {
                if (isClosed)
                {
                    points[points.Count - 1] = points[2];
                }
                points.RemoveRange(0, 3);
            }
            else if (anchorIndex == points.Count - 1 && !isClosed)
            {
                points.RemoveRange(anchorIndex - 2, 3);
            }
            else
            {
                points.RemoveRange(anchorIndex - 1, 3);
            }
        }
    }

    public Vector2[] GetPointInSegment(int index)
    {
        return new Vector2[] { points[index * 3], points[index * 3 + 1], points[index * 3 + 2], points[LoopIndex(index * 3 + 3)] };
    }

    public void MovePoint(int index, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[index];

        if (index % 3 == 0 || !autoSetControlPoints)
        {
            points[index] = pos;
            if (autoSetControlPoints)
            {
                AutoSetAllAffectedControlPoints(index);
            }
            else
            {
                //Permet le mouvement des points de segments lorsque que l'on déplace une "Ancre"
                if (index % 3 == 0)
                {
                    if ((index + 1) < points.Count || isClosed)
                    {
                        points[LoopIndex(index + 1)] += deltaMove;
                    }
                    if (index - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(index - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (index + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? index + 2 : index - 2;
                    int anchorIndex = (nextPointIsAnchor) ? index + 1 : index - 1;
                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float distance = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 direction = (points[LoopIndex(anchorIndex)] - pos).normalized;
                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + direction * distance;
                    }
                }
            }
        }
    }

    public Vector2[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
    {
        List<Vector2> evenlySpacePoints = new List<Vector2>();
        evenlySpacePoints.Add(points[0]);
        Vector2 previousPoint = points[0];
        float distanceSinceLastEvenPoint = 0;

        for(int segIndex = 0; segIndex < NumSegments; segIndex++)
        {
            Vector2[] p = GetPointInSegment(segIndex);
            float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
            float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + controlNetLength / 2f;
            int divisions = Mathf.CeilToInt(estimatedCurveLength * resolution * 10);
            float t = 0;
            while(t <= 1)
            {
                t += 1f / divisions;
                Vector2 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                distanceSinceLastEvenPoint += Vector2.Distance(previousPoint, pointOnCurve);

                while (distanceSinceLastEvenPoint >= spacing)
                {
                    float overshootDistance = distanceSinceLastEvenPoint - spacing;
                    Vector2 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overshootDistance;
                    evenlySpacePoints.Add(newEvenlySpacedPoint);
                    distanceSinceLastEvenPoint = overshootDistance;
                    previousPoint = newEvenlySpacedPoint;
                }
                previousPoint = pointOnCurve;
            }
        }
        return evenlySpacePoints.ToArray();
    }

    void AutoSetAllAffectedControlPoints(int updatedAnchorIndex)
    {
        for(int i = updatedAnchorIndex - 3; i <= updatedAnchorIndex + 3; i += 3)
        {
            if(i >= 0 && i < points.Count || isClosed)
            {
                AutoSetAnchorControlPoints(LoopIndex(i));
            }
        }
        AutoSetStartAndEndControl();
    }

    void AutoSetAllControlPoints()
    {
        for(int i = 0; i < points.Count; i += 3)
        {
            AutoSetAnchorControlPoints(i);
        }
        AutoSetStartAndEndControl();
    }

    void AutoSetAnchorControlPoints(int anchorIndex)
    {
        Vector2 anchorPos = points[anchorIndex];
        Vector2 direction = Vector2.zero;
        float[] neighbourDistances = new float[2];

        if (anchorIndex - 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            direction += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }
        if (anchorIndex + 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            direction -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }

        direction.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;
            if (controlIndex >= 0 && controlIndex <= points.Count || isClosed)
            {
                points[LoopIndex(controlIndex)] = anchorPos + direction * neighbourDistances[i] * 0.5f;
            }
        }
    }

    void AutoSetStartAndEndControl()
    {
        if(!isClosed)
        {
            points[1] = (points[0] + points[2]) * 0.5f;
            points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;
        }
    }

    public List<Vector2> Getlist()
    {
        return this.points;
    }

    int LoopIndex(int index)
    {
        return((index + points.Count) % points.Count);
    }
}
