using UnityEditor;
using UnityEngine;

public class SwingVisualizer : MonoBehaviour
{
    [SerializeField] private LightsaberData lightsaberData;
    [SerializeField] private LightsaberSwingData swingData;
    [SerializeField] private Color color;

    private bool isRunning=false;

    private void Start()
    {
        isRunning = true;
    }

    private void OnDrawGizmos()
    {
        var trans = transform;

        var fromRotValue = isRunning ? swingData.fromRotation.RuntimeValue : swingData.fromRotation.Value;
        var toRotValue = isRunning ? swingData.toRotation.RuntimeValue : swingData.toRotation.Value;
        

        var topPoint = Vector3.up * lightsaberData.Length;
        var fromPoint = MathHelper.RotatePointAroundPivot(topPoint, Vector3.zero, fromRotValue);
        var toPoint = MathHelper.RotatePointAroundPivot(topPoint, Vector3.zero, toRotValue);
        var midPoint = MathHelper.RotatePointAroundPivot(topPoint, Vector3.zero,
            Quaternion.Lerp(Quaternion.Euler(fromRotValue), Quaternion.Euler(toRotValue), 0.5f));

        var fromPointWorld = trans.TransformPoint(fromPoint);
        var toPointWorld = trans.TransformPoint(toPoint);
        var midPointWorld = trans.TransformPoint(midPoint);
        var currentColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawSphere(fromPointWorld, lightsaberData.radius / 4f);
        Gizmos.DrawSphere(toPointWorld, lightsaberData.radius / 4f);
        Gizmos.DrawSphere(midPointWorld, lightsaberData.radius / 4f);
        Gizmos.color = currentColor;

        var fromRot = Quaternion.Euler(fromRotValue);
        var toRot = Quaternion.Euler(toRotValue);
        var angle = Quaternion.Angle(fromRot, toRot);
        var center = trans.position;
        var dirOfPointRelativeToCenter = (fromPointWorld - center).normalized;
        Handles.DrawWireArc(center, trans.up, dirOfPointRelativeToCenter, -angle, lightsaberData.Length);
    }
}