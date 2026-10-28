using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rock : MonoBehaviour
{
    [SerializeField] private float pushSpeed = 3f; // Igual a la velocidad del jugador

    private Rigidbody2D rb;
    private ContactFilter2D filter;
    private readonly RaycastHit2D[] hits = new RaycastHit2D[8];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        filter = new ContactFilter2D();
        filter.useTriggers = false; // Ignora espirales y estrellas
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (!collision.collider.TryGetComponent(out PlayerMovement player)) return;

        Vector2 dir = player.Direction;
        if (dir == Vector2.zero) return;

        // El jugador debe estar detrás de la roca, mirando hacia ella
        Vector2 diff = rb.position - collision.collider.attachedRigidbody.position;
        if (Vector2.Dot(diff, dir) <= 0.1f) return;

        // Y alineado con la roca en el otro eje (evita empujar desde una esquina)
        Vector2 perpendicular = new Vector2(Mathf.Abs(dir.y), Mathf.Abs(dir.x));
        float offset = Mathf.Abs(Vector2.Dot(diff, perpendicular));
        if (offset > 0.6f) return;

        float distance = pushSpeed * Time.fixedDeltaTime;

        // Si hay una pared u otra roca delante, no se mueve
        int count = rb.Cast(dir, filter, hits, distance + 0.02f);
        for (int i = 0; i < count; i++)
        {
            if (!hits[i].collider.CompareTag("Player")) return;
        }

        rb.MovePosition(rb.position + dir * distance);
    }
}