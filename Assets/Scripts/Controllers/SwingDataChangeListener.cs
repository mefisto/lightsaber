using UnityEngine;

public class SwingDataChangeListener : MonoBehaviour
{
    [SerializeField] private LightsaberSwingData swing;
    [SerializeField] private FloatGameEvent xRotationChangedEvent;
    [SerializeField] private GameEvent calculatePredictionEvent;

    private void OnEnable()
    {
        xRotationChangedEvent.AddListener(OnXRotationChanged);
    }

    private void OnDisable()
    {
        xRotationChangedEvent.RemoveListener(OnXRotationChanged);
    }

    private void OnXRotationChanged(float value)
    {
        swing.fromRotation.RuntimeValue.x = value;
        swing.toRotation.RuntimeValue.x = value;
        calculatePredictionEvent.Raise();
    }
}