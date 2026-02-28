using System;
using System.Collections;
using UnityEngine;

public class slugData : MonoBehaviour
{
    public int damage = 5;
    private Rigidbody2D rb;
    private Collider2D slugCollider;
    public GameObject slug;
    private float timer = 4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        slugCollider = GetComponent<Collider2D>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            mobInfo _mobInfo = collision.gameObject.GetComponent<mobInfo>();
            _mobInfo.takeDamage(damage);
            Destroy(gameObject);
            Debug.Log("Hit Enemy");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        StartCoroutine(destroyTimer());
    }

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
