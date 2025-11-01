using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.LightAnchor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private Vector3 minPosition;
    private Vector3 maxPosition;

    public void Init (Vector3 _minPosition, Vector3 _maxPosition)
    {
        minPosition = _minPosition;
        maxPosition = _maxPosition;
    }

    private void Update ()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey (KeyCode.LeftArrow)) moveDirection += Vector3.left;
        if (Input.GetKey (KeyCode.RightArrow)) moveDirection += Vector3.right;
        if (Input.GetKey (KeyCode.UpArrow)) moveDirection += Vector3.up;
        if (Input.GetKey (KeyCode.DownArrow)) moveDirection += Vector3.down;

        moveDirection = moveDirection.normalized * speed * Time.smoothDeltaTime;

        Vector3 expectedPosition = transform.position + moveDirection;

        expectedPosition.x = Mathf.Clamp (expectedPosition.x, minPosition.x, maxPosition.x);
        expectedPosition.y = Mathf.Clamp (expectedPosition.y, minPosition.y, maxPosition.y);

        transform.position = expectedPosition;

        Collider2D[] collisions = Physics2D.OverlapCircleAll (transform.position, transform.lossyScale.x);

        foreach (Collider2D collision in collisions)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController> ();

            if (enemy != null && enemy.Alive)
            {
                enemy.Kill ();
            }
        }
    }
}
