using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Lightsaber Swing Data", fileName = "Lightsaber Swing Data", order = 0)]
public class LightsaberSwingData : ScriptableObject
{
    public Vector3Variable fromRotation;
    public Vector3Variable toRotation;

    [Range(0.01f, 100f)] public float duration;
    
    private void Reset()
    {
        fromRotation.Value = new Vector3(35f, -45f, 0);
        toRotation.Value = new Vector3(35f, 45f, 0);
        duration = 1;
    }
    
}