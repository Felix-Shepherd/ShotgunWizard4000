using UnityEngine;

public class weaponDirection : MonoBehaviour
{
   
    private Vector3 direction;
    private float realDr;
    public Transform tr;
    public float offset;
    private Transform weapontr;
    private weaponManager weaponMANAGERSCRIPT;


    void Start() {
        weaponMANAGERSCRIPT = GetComponentInParent<weaponManager>();
        if (weaponMANAGERSCRIPT == null){
            Debug.Log("aids");
        }
    }


    void Update()
    {
        if (tr.childCount > 0)
        {
            weapontr = tr.GetChild(0);
        }

        if (weapontr != null)
        {
            lookatmehector();
        }
    }

    void lookatmehector()
    {
        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        direction = new Vector2(( mouseWorldPosition.x - weapontr.position.x), ( mouseWorldPosition.y - weapontr.position.y)).normalized;
        
        realDr = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - offset;
        
        
        weapontr.rotation = Quaternion.Euler(new Vector3(0, 0, realDr));

        Vector3 wScale = weapontr.localScale;
        if (direction.x < 0)
        {
            wScale.y = -Mathf.Abs(wScale.y);
        }
        else
        {
            wScale.y = Mathf.Abs(wScale.y);
        }
        weapontr.localScale = wScale;
    }
    
    
    
}
