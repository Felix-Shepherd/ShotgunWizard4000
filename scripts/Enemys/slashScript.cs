using UnityEngine;
using System.Collections.Generic;


public class slashScript : MonoBehaviour
{


    private List<GameObject> objectsHit = new List<GameObject>();
    public int damage = 2;
    public float force = 20f;

    void OnTriggerEnter2D(Collider2D collision)

    {
        Debug.Log("colissoin");
        Debug.Log(collision.gameObject.tag);
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player");

            if (!objectsHit.Contains(collision.gameObject))
            {
                Debug.Log("Hit Player");

                PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
                playerData.TakeDamage(damage);
                objectsHit.Add(collision.gameObject);
            }
            Vector2 recoilDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerData>().rb.AddForce(recoilDirection * force, ForceMode2D.Force);
        }
        
        
    }

    
}
