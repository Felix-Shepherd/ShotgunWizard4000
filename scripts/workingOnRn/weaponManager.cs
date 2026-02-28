using System.Runtime.CompilerServices;
using UnityEngine;

public class weaponManager : MonoBehaviour
{

    public Transform weaponHolder;

    private GameObject currentEquippedWeapon;


    public void EquipWeapon(weaponItem weaponToEquip)
    {

        if (currentEquippedWeapon != null)
        {
            DropWeapon();
        }

        currentEquippedWeapon = weaponToEquip.gameObject;
        currentEquippedWeapon.transform.SetParent(weaponHolder);
        currentEquippedWeapon.transform.localPosition = Vector3.zero;
        currentEquippedWeapon.transform.localRotation = Quaternion.identity;
        currentEquippedWeapon.GetComponent<Collider2D>().enabled = false;
        currentEquippedWeapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        currentEquippedWeapon.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        Destroy(weaponToEquip);
        
    }

    private void DropWeapon()
    {
        if (currentEquippedWeapon == null) return;
        
        currentEquippedWeapon.transform.SetParent(null);
        weaponItem weaponScript = currentEquippedWeapon.GetComponent<weaponItem>();
        if (weaponScript != null)

        currentEquippedWeapon = null;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentEquippedWeapon != null)
        {
            DropWeapon();
        }
    }
    
}
