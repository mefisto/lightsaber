using UnityEngine;

public static class MathHelper
{
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return RotatePointAroundPivot(point, pivot, Quaternion.Euler(angles));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }

    //Find two closest point between two infinite line. Resulting points may be out of given line segments.  
    public static void ClosestPointsOnTwoLines(out Vector3 pointOnA, out Vector3 pointOnB, Vector3 pointA,
        Vector3 directionA,
        Vector3 pointB, Vector3 directionB)
    {
        var n = Vector3.Cross(directionA, directionB);
        var u = Vector3.Cross(n, pointA - pointB) / Vector3.Dot(n, n);
        pointOnA = pointA - directionA * Vector3.Dot(directionB, u);
        pointOnB = pointB - directionB * Vector3.Dot(directionA, u);
    }

    //Find is given point is exactly on line segment 
    public static bool IsPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        var lineVec = linePoint2 - linePoint1;
        var pointVec = point - linePoint1;
        var dot = Vector3.Dot(pointVec, lineVec);
        return dot > 0 && pointVec.magnitude <= lineVec.magnitude;
    }
}