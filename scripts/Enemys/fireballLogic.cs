using UnityEngine;

public class fireballLogic : MonoBehaviour

{



    public GameObject explosion;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "enemy")
        {
            explode();
        }
    
    }


    void explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
