using UnityEngine;

public class landMineLogic : MonoBehaviour
{
    public GameObject esplosion;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("enemy"))
        {
            Instantiate(esplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
