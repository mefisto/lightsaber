using UnityEngine;

public class XAxisRangeRotator : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private FloatGameEvent rotationChangedEvent;
    [SerializeField] private FloatVariable minAngle;
    [SerializeField] private FloatVariable maxAngle;
    [SerializeField] private FloatGameEvent afterXAngleChangeEvent;
    private Quaternion maxRot;

    private Quaternion minRot;

    private void Start()
    {
        var current = targetTransform.localRotation.eulerAngles;
        minRot = Quaternion.Euler(minAngle.Value, current.y, current.z);
        maxRot = Quaternion.Euler(maxAngle.Value, current.y, current.z);
    }

    private void OnEnable()
    {
        rotationChangedEvent.AddListener(OnRotationChange);
    }

    private void OnDisable()
    {
        rotationChangedEvent.RemoveListener(OnRotationChange);
    }

    private void OnRotationChange(float value)
    {
        var rot = Quaternion.Lerp(minRot, maxRot, value);
        targetTransform.localRotation = rot;
        afterXAngleChangeEvent.Raise(Mathf.Lerp(minAngle.Value,maxAngle.Value,value));
    }
}