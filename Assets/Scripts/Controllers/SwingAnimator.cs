using System.Collections;
using UnityEngine;

public class SwingAnimator : MonoBehaviour
{
    [SerializeField] private Transform swordA;
    [SerializeField] private Transform swordB;
    [SerializeField] private LightsaberSwingData swingA;
    [SerializeField] private LightsaberSwingData swingB;
    [SerializeField] private GameEvent animateSimulationEvent;
    [SerializeField] private CollisionPredictionResultEvent collisionPredictionResultEvent;
    [SerializeField] [Range(0.01f, 10f)] private float speedMultiplier = 1f;
    [SerializeField] [Range(0.0f, 10f)] private  float animationResetDuration = 0.5f;
    [SerializeField] [Range(0.0f, 10f)] private float waitAfterCollisionTime = 0.5f;
    [SerializeField] [Range(0.0f, 10f)] private float waitAfterSwingTime = 0.5f;
    [SerializeField] private GameObject particlePrefab;
    
    private CollisionPredictionResult predictionResult=new CollisionPredictionResult();
    private bool collisionTimePassed;
    private bool isAnimating;

    private void OnEnable()
    {
        animateSimulationEvent.AddListener(OnAnimateSimulation);
        collisionPredictionResultEvent.AddListener(OnCollisionPredictionResult);
    }

    private void OnDisable()
    {
        animateSimulationEvent.RemoveListener(OnAnimateSimulation);
        collisionPredictionResultEvent.RemoveListener(OnCollisionPredictionResult);
    }

    private void OnAnimateSimulation()
    {
        if (isAnimating)
            return;
        var maxDuration = Mathf.Max(swingA.duration, swingB.duration);
        var delay = waitAfterSwingTime;
        collisionTimePassed = false;
        var postDelayA = maxDuration - swingB.duration;
        var postDelayB = maxDuration - swingA.duration;
        if (predictionResult.hasCollision)
        {
            delay = waitAfterCollisionTime;
            StartCoroutine(CollisionTimer(maxDuration * predictionResult.time));
        }

  
        StartCoroutine(WaitForAnimations(maxDuration+animationResetDuration));
        StartCoroutine(Animate(swordA, swingA,postDelayA+delay));
        StartCoroutine(Animate(swordB, swingB,postDelayB+delay));
    }

    private IEnumerator Animate(Transform target, LightsaberSwingData data,float postDelay)
    {
        var fromRot = Quaternion.Euler(data.fromRotation.RuntimeValue);
        var toRot = Quaternion.Euler(data.toRotation.RuntimeValue);
        float timer = 0;
        var totalDuration = data.duration*speedMultiplier;
        while (timer <= totalDuration && !collisionTimePassed)
        {
            timer += Time.deltaTime;
            target.localRotation = Quaternion.Lerp(fromRot, toRot, timer / totalDuration);
            yield return null;
        }

        if (postDelay > 0)
            yield return new WaitForSeconds(postDelay*speedMultiplier);
        timer = 0;
        totalDuration = animationResetDuration*speedMultiplier;
        var currentRot = target.localRotation;
        while (timer<=totalDuration)
        {
            timer += Time.deltaTime;
            target.localRotation = Quaternion.Lerp(currentRot, fromRot, timer / totalDuration);
            yield return null;
        }
    }
    
    private IEnumerator WaitForAnimations(float duration)
    {
        isAnimating = true;
        yield return new WaitForSeconds(duration*speedMultiplier);
        isAnimating = false;
    }

    private IEnumerator CollisionTimer(float duration)
    {
        yield return new WaitForSeconds(duration*speedMultiplier);
        collisionTimePassed = true;
        Instantiate(particlePrefab, predictionResult.collisionPosition, Quaternion.identity);
        yield return null;
    }

    private void OnCollisionPredictionResult(CollisionPredictionResult value)
    {
        predictionResult= value;
    }
}