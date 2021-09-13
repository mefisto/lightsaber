using UnityEngine;

[CreateAssetMenu(menuName = "Create Lightsaber Data", fileName = "Lightsaber Data", order = 0)]
public class LightsaberData : ScriptableObject
{
    [Range(0.1f, 5f)] public float radius;

    [Range(0.1f, 30f)] public float laserLength;

    [Range(0.1f, 3f)] public float handleLength;

    public float Length => laserLength + handleLength;

    private void Reset()
    {
        radius = 0.5f;
        laserLength = 12;
        handleLength = 2;
    }
}