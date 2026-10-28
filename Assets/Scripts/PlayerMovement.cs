using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;
    public Vector2 Direction => direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Una sola dirección: el eje horizontal tiene prioridad
        direction = x != 0 ? new Vector2(x, 0) : new Vector2(0, y);
    }

    private void FixedUpdate()
    {
        // En Unity 2022 o anterior usa: rb.velocity
        rb.linearVelocity = direction * speed;
    }
}