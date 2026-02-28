using UnityEngine;

public class playerPickup : MonoBehaviour
{

    private weaponItem nearbyWeapon;
    public weaponManager weaponManager; 

    void Update()
    {
        if (nearbyWeapon != null)
        {
            weaponManager.EquipWeapon(nearbyWeapon);
            Debug.Log("pickedup");
        }
        
    }

        
    void OnTriggerEnter2D(Collider2D other)
    { 

        weaponItem item = other.GetComponent<weaponItem>();
        if (item != null)
        {
            nearbyWeapon = item;
            Debug.Log("inrange");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        weaponItem item = other.GetComponent<weaponItem>();
        if (item != null && item == nearbyWeapon)
        {
            nearbyWeapon = null;

        }
    }
}
