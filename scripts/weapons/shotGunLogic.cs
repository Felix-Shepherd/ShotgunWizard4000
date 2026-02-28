using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class shotGunLogic : MonoBehaviour
{
    public GameObject slugPrefab; 
    public Rigidbody2D slugRb;
    public GameObject player;
    private Vector2 direction;
    public float slugSpeed = 25;
    public float cooldown = 1;
    private bool canFire = true;
    public int slugCount = 5; 
    public float spreadAngle = 10f; 
    public GameObject shotGun;
    public Transform weaponHOlder;
    public float recoil = 0.5f;
    private Vector2 recoilDir;
    private Rigidbody2D playerRb;
    private shotgunAnimation shotgunAnim;
    private PlayerData playerData;
    
    
    [SerializeField] private AudioClip shotgunSound;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        shotgunAnim = GetComponentInChildren<shotgunAnimation>();
        playerData = player.GetComponent<PlayerData>();

    }  
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canFire && playerData.currentShells > 0 && !playerData.reloading)
        {
            playerData.useShell();
            fire();
            shotgunAnim.shootanimation();
        }
    }

    void fire()
    {
        
        float wdeg = shotGun.transform.eulerAngles.z;
        float wradian = wdeg * Mathf.Deg2Rad;
        float x = Mathf.Cos(wradian);
        float y = Mathf.Sin(wradian);
        
        direction = new Vector2(x, y);
        recoilDir = -direction;


        float angleStep = spreadAngle / (slugCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < slugCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            float totalAngle = wdeg + currentAngle;
            float totalRadian = totalAngle * Mathf.Deg2Rad;
            
            Vector2 spreadDirection = new Vector2(Mathf.Cos(totalRadian), Mathf.Sin(totalRadian));
            
            GameObject newSlug = Instantiate(slugPrefab, shotGun.transform.position, Quaternion.identity);
            Rigidbody2D newSlugRb = newSlug.GetComponent<Rigidbody2D>();
            newSlugRb.AddForce(spreadDirection * slugSpeed, ForceMode2D.Impulse);
        }
        soundManagerScript.instance.playSoundClip(shotgunSound, this.transform, 1f);
        
        playerRb.AddForce(recoilDir * recoil, ForceMode2D.Impulse);
        
        canFire = false;
        StartCoroutine(coolDownTime());
}

    IEnumerator coolDownTime()
    {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
    
}
