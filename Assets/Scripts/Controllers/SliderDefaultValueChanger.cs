using UnityEngine;
using UnityEngine.UI;

public class SliderDefaultValueChanger : MonoBehaviour
{
    [SerializeField] private FloatGameEvent defaultValueChangedEvent;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        defaultValueChangedEvent.AddListener(OnDefaultValueChanged);
    }

    private void OnDisable()
    {
        defaultValueChangedEvent.RemoveListener(OnDefaultValueChanged);
    }

    private void OnDefaultValueChanged(float value)
    {
        slider.value = value;
    }
}
