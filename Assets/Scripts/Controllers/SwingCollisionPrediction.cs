using System;
using UnityEngine;

public class SwingCollisionPrediction : MonoBehaviour
{
    [SerializeField] private Transform swordATransform;
    [SerializeField] private Transform swordBTransform;
    [SerializeField] private LightsaberData lightsaberA;
    [SerializeField] private LightsaberSwingData swingA;
    [SerializeField] private LightsaberData lightsaberB;
    [SerializeField] private LightsaberSwingData swingB;
    [SerializeField] private GameEvent calculatePredictionEvent;
    [SerializeField] private CollisionPredictionResultEvent collisionPredictionResultEvent;
    [SerializeField] [Range(0.1f, 45f)] private float angleResolution = 1f;

    private float averageLightsaberRadius;

    private void OnEnable()
    {
        calculatePredictionEvent.AddListener(OnCalculatePrediction);
    }

    private void OnDisable()
    {
        calculatePredictionEvent.RemoveListener(OnCalculatePrediction);
    }

    private void Start()
    {
        //OnCalculatePrediction();
    }

    private void OnDrawGizmos()
    {
        var posA = swordATransform.position;
        var posB = swordBTransform.position;
        var posATop = swordATransform.TransformPoint(Vector3.up * lightsaberA.Length);
        var posBTop = swordBTransform.TransformPoint(Vector3.up * lightsaberB.Length);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(posA, posB);
        Gizmos.DrawLine(posATop, posBTop);
    }

    //todo:uses linear search to calculate prediction we can improve performance with some kind of binary search algorithm.
    private void OnCalculatePrediction()
    {
        averageLightsaberRadius = (lightsaberA.radius + lightsaberB.radius)/2f;
        var posA = swordATransform.position;
        var fromRotALocal = Quaternion.Euler(swingA.fromRotation.RuntimeValue);
        var rotA = swordATransform.rotation * Quaternion.Inverse(fromRotALocal);
        var toRotALocal = Quaternion.Euler(swingA.toRotation.RuntimeValue);
        var posB = swordBTransform.position;
        var fromRotBLocal = Quaternion.Euler(swingB.fromRotation.RuntimeValue);
        var rotB = swordBTransform.rotation * Quaternion.Inverse(fromRotBLocal);
        var toRotBLocal = Quaternion.Euler(swingB.toRotation.RuntimeValue);

        var angleA = Quaternion.Angle(fromRotALocal, toRotALocal)* swingA.duration;
        var angleB = Quaternion.Angle(fromRotALocal, toRotALocal)* swingB.duration;
        var stepCount = Mathf.FloorToInt(Mathf.Max(angleA, angleB) / angleResolution);
        var result = new CollisionPredictionResult();
        float step = 0;
        while (step < stepCount)
        {
            var t = step / stepCount;
            var stepResult = Step(posA, rotA, fromRotALocal, toRotALocal, t, posB, rotB, fromRotBLocal, toRotBLocal,
                result);
            if (stepResult) break;
            step++;
        }
        collisionPredictionResultEvent.Raise(result);
    }

    private bool Step(Vector3 posA, Quaternion rotA, Quaternion fromRotALocal, Quaternion toRotALocal, float t,
        Vector3 posB, Quaternion rotB, Quaternion fromRotBLocal, Quaternion toRotBLocal,
        CollisionPredictionResult result)
    {
 
        var posASwing = StepSwing(posA, rotA, fromRotALocal, toRotALocal, lightsaberA.Length, t);
        var posBSwing = StepSwing(posB, rotB, fromRotBLocal, toRotBLocal, lightsaberB.Length, t);
        Vector3 pointOnA;
        Vector3 pointOnB;
        MathHelper.ClosestPointsOnTwoLines(out pointOnA, out pointOnB, posA, posA - posASwing, posB, posB - posBSwing);
        var distance = Vector3.Distance(pointOnA, pointOnB);
        if (distance <= averageLightsaberRadius)
        {
            var a = MathHelper.IsPointOnLineSegment(posA, posASwing, pointOnA);
            var b = MathHelper.IsPointOnLineSegment(posB, posBSwing, pointOnB);
            if (a && b)
            {
                result.hasCollision = true;
                result.collisionPosition = (pointOnA + pointOnB) / 2f;
                result.time = t;
                return true;
            }
        }

        return false;
    }

    private Vector3 StepSwing(Vector3 pos, Quaternion rot, Quaternion fromRotLocal, Quaternion toRotLocal, float height,
        float t)
    {
        var top = Vector3.up * height;
        var currentRot = Quaternion.Lerp(fromRotLocal, toRotLocal, t);
        var rotPosLocal =MathHelper.RotatePointAroundPivot(top, Vector3.zero, currentRot);
        return rot * rotPosLocal + pos;
    }



}