using UnityEngine;

public class LightsaberSetup : MonoBehaviour
{
    [SerializeField] private LightsaberData lightsaberData;
    [SerializeField] private LightsaberSwingData swingData;

    private Transform handle;
    private Transform laser;

    private void Awake()
    {
        var trans = transform;
        handle = trans.GetChild(0);
        laser = trans.GetChild(1);
        handle.localScale =
            new Vector3(lightsaberData.radius * 2.5f, lightsaberData.handleLength, lightsaberData.radius * 2.5f);
        laser.localScale = new Vector3(lightsaberData.radius*2f, lightsaberData.laserLength, lightsaberData.radius*2f);
        laser.localPosition = new Vector3(0, lightsaberData.handleLength, 0);
        trans.Rotate(swingData.fromRotation.Value);
    }
}