using System.Collections;
using UnityEngine;

public class LightsaberSetupModifier : MonoBehaviour
{
    [SerializeField] private LightsaberData lightsaberData;
    [SerializeField] private LightsaberSwingData swingData;
    [SerializeField] private Vector3 effectScaleMask=new Vector3(1,0,1);
    [SerializeField] private AnimationCurve effectCurve;
    [SerializeField] private float effectDuration=0.5f;
    private Transform handle;
    private Transform laser;
    private Vector3 laserStartScale;
    private Vector3 laserEndScale;
    private void Awake()
    {
        var trans = transform;
        handle = trans.GetChild(0);
        laser = trans.GetChild(1);
        handle.localScale =
            new Vector3(lightsaberData.radius * 2.5f, lightsaberData.handleLength, lightsaberData.radius * 2.5f);
        laser.localScale = new Vector3(lightsaberData.radius*2f, lightsaberData.laserLength, lightsaberData.radius*2f);
        laser.localPosition = new Vector3(0, lightsaberData.handleLength, 0);
        trans.localRotation=Quaternion.Euler(swingData.fromRotation.RuntimeValue);
    }

    private void Start()
    {

        laserEndScale = laser.localScale;
        laserStartScale = new Vector3(effectScaleMask.x * laserEndScale.x, effectScaleMask.y * laserEndScale.y, effectScaleMask.z * laserEndScale.z);
        StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        float timer = 0;
        while (timer<=effectDuration)
        {

            laser.localScale = Vector3.Lerp(laserStartScale, laserEndScale, effectCurve.Evaluate(timer/effectDuration));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}