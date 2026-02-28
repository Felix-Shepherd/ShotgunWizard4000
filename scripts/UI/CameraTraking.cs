using UnityEngine;

public class CameraTraking : MonoBehaviour
{
    private Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public int FOA;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); 
        
    }
}
