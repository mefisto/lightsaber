using UnityEngine;

public class XAxisRangeRotator : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private FloatGameEvent rotationChangedEvent;
    [SerializeField] private LightsaberSwingData swingData;
    [SerializeField] private FloatVariable minAngle;
    [SerializeField] private FloatVariable maxAngle;
    [SerializeField] private FloatGameEvent afterXAngleChangeEvent;
    [SerializeField] private FloatGameEvent sliderDefaultValueChangeEvent;

    private void Start()
    {
        var current = targetTransform.localRotation.eulerAngles;
        sliderDefaultValueChangeEvent.Raise(Mathf.InverseLerp(minAngle.Value,maxAngle.Value,current.x));
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
        var xAngle= Mathf.Lerp(minAngle.Value, maxAngle.Value, value);
        var rot = Quaternion.Euler(xAngle, swingData.fromRotation.RuntimeValue.y, swingData.fromRotation.RuntimeValue.z);
        targetTransform.localRotation = rot;
        afterXAngleChangeEvent.Raise(xAngle);
    }
}