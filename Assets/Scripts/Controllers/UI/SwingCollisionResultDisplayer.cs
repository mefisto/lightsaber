using TMPro;
using UnityEngine;

public class SwingCollisionResultDisplayer : MonoBehaviour
{
    [SerializeField] private CollisionPredictionResultEvent collisionResultEvent;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(collisionResultEvent!=null)
            collisionResultEvent.AddListener(OnCollisionResult);
    }

    private void OnDisable()
    {
        if(collisionResultEvent!=null)
            collisionResultEvent.RemoveListener(OnCollisionResult);
    }

    private void OnCollisionResult(CollisionPredictionResult result)
    {
        text.SetText(result.hasCollision ? "Will collide" : "Will Not Collide");
    }
}