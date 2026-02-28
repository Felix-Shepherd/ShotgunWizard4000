using UnityEngine;

public class headTracking : MonoBehaviour
{
    private Vector3 direction;
    private float realDr;
    public float offset = 0f;
    

    void Update()
    {
        TrackMouse();
    }

    void TrackMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        direction = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y).normalized;
        realDr = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - offset;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, realDr));

        Vector3 hScale = transform.localScale;
        if (direction.x < 0)
        {
            hScale.y = -Mathf.Abs(hScale.y);
        }
        else
        {
            hScale.y = Mathf.Abs(hScale.y);
        }
        transform.localScale = hScale;
    }
}
