using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination; // Objeto vacío en la casilla START

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out Rigidbody2D rb))
        {
            rb.position = destination.position;
            rb.linearVelocity = Vector2.zero; // En Unity 2022 o anterior: rb.velocity
        }
    }
}
