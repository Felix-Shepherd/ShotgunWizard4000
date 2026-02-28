using UnityEngine;

public class corpsScript : MonoBehaviour
{
    public float lifeTime = 8f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
