using UnityEngine;

public class weaponItem : MonoBehaviour
{
    public string weaponName;
    public bool isEquipped = false;

    public void OnDrop()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    public void pickedUp()
    {
        isEquipped = true;
    }
    public void dropped()
    {
        isEquipped = false;
    }
}
