using Unity.VisualScripting;
using UnityEngine;

public class jumpPadScript : MonoBehaviour
{
    public float jumpForce = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("enemy"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            Debug.Log("jumped");
        }
    }

}
