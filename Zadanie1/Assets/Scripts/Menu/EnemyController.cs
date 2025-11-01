using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] SpriteRenderer spriteRenderer;

    private Vector3 minPosition;
    private Vector3 maxPosition;

    public bool Alive { private set; get; } = true;

    public void Init (Vector3 _minPosition, Vector3 _maxPosition)
    {
        minPosition = _minPosition;
        maxPosition = _maxPosition;

        Vector2 startPosition = Vector2.zero;
        startPosition.x = Mathf.Lerp(minPosition.x, maxPosition.x, Random.value);
        startPosition.y = Mathf.Lerp(maxPosition.y, minPosition.y, Random.value);

        transform.position = startPosition;
    }

    public void RunAway(Vector3 playerPosition)
    {
        if (!Alive)
        {
            return;
        }

        Vector3 runDirection = (transform.position - playerPosition).normalized * speed * Time.smoothDeltaTime;
        Vector3 expectedPosition = transform.position + runDirection;

        expectedPosition.x = Mathf.Clamp(expectedPosition.x, minPosition.x, maxPosition.x);
        expectedPosition.y = Mathf.Clamp(expectedPosition.y, minPosition.y, maxPosition.y);

        transform.position = expectedPosition;
    }

    public void Kill ()
    {
        if (!Alive) return;

        Alive = false;
        Color color = spriteRenderer.color;
        color.a = 0.7f;

        spriteRenderer.color = color;
        
    }
}
