using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private float startRadius;
    [SerializeField] private float endRadius;

    [SerializeField] private float zoneTime = 60;

    [SerializeField] private float elapsedTime = 0f;

    private void Start()
    {
        transform.localScale = Vector2.one * startRadius;
    }

    private void FixedUpdate()
    {
        if (elapsedTime < zoneTime) 
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / zoneTime;

            float currentRadius = Mathf.Lerp(startRadius, endRadius, t);

            transform.localScale = Vector2.one * currentRadius;
        }
    }
}
