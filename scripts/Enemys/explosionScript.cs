using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{

    
    [SerializeField] AudioClip explosionSound;
    private List<GameObject> objectsHit = new List<GameObject>();
    public int damage = 2;
    public float force = 20f;

    void Start()
    {
        soundManagerScript.instance.playSoundClip(explosionSound, this.transform, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Player")
        {

            if (!objectsHit.Contains(collision.gameObject))
            {
                PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
                playerData.TakeDamage(damage);
                objectsHit.Add(collision.gameObject);
            }
            Vector2 recoilDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerData>().rb.AddForce(recoilDirection * force, ForceMode2D.Force);
        }   
    }
}
