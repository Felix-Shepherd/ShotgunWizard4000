using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;


public class PlayerData : MonoBehaviour
{
    
    [SerializeField] AudioClip damagedSound;
    [SerializeField] AudioClip reloadSound;

    public Rigidbody2D rb;
    public Collider2D col;
    public int maxHealth = 5;
    public int currentHealth = 5; 

    public int maxShells = 5;
    public int currentShells = 5;
    
    private float reloadHoldTime = 0f;
    private float reloadDuration = 0.5f;
    
    public bool IsMoving;
    private heartManager heartManager;
    public shellContainer shellContainer;
    public bool reloading = false;
    // coin related shit
    public float coins = 0;

    public TextMeshProUGUI coinText;

    private bool ignorePlatforms = false;

    public GameObject deathScreen;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        heartManager = FindFirstObjectByType<heartManager>();
        heartManager.UpdateHealth();
        shellContainer = FindFirstObjectByType<shellContainer>();
        shellContainer.updateShells();
    }

    void Update()
    {
        IsMoving = rb.linearVelocityX > 0.01f || rb.linearVelocityX < -0.01f;
        reload();
        if (currentShells >= maxShells)
        {
            reloading = false;
            reloadHoldTime = 0f;
        }
        whenToPhase();

        if (currentHealth <= 0)
        {
            die();
        }
        
        //Debug.Log(" DONT LOOK OH PLEASE GOD! DONT LOOK OH PLEASE GOD! DONT LOOK OH PLEASE GOD! DONT LOOK OH PLEASE GOD! DONT LOOK OH PLEASE GOD! DONT LOOK PLEASE GOD!");


    }
    void FixedUpdate()
    {
        if (ignorePlatforms) // might be better do do this apon contact condiditon 
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("platform"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("platform"), false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        heartManager.UpdateHealth();
        soundManagerScript.instance.playSoundClip(damagedSound, this.transform, 1f);
    }

        public void useShell()
    {
        if (currentShells > 0)
        {
            currentShells--;
            shellContainer.updateShells();
        }
    }
    public void increaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        heartManager.UpdateHealth();
    }

    public void addShell(int amount)
    {
        currentShells += amount;
        soundManagerScript.instance.playSoundClip(reloadSound, this.transform, 1f);
        if (currentShells > maxShells)
        {
            currentShells = maxShells;
        }
        shellContainer.updateShells();
    }

    void reload()
    {
        if (currentShells < maxShells && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                reloadHoldTime = 0f;
            }
        }
        
        if (reloading)
        {
            reloadHoldTime += Time.deltaTime;
            if (reloadHoldTime >= reloadDuration)
            {
                currentShells += 1;
                soundManagerScript.instance.playSoundClip(reloadSound, this.transform, 0.25f);
                shellContainer.updateShells();
                reloadHoldTime = 0f;
                
                if (currentShells >= maxShells)
                {
                    reloading = false;
                    currentShells = maxShells;
                }
            }
        }
        
        shellContainer.toggleReloadSymbol(reloading);
    }


    public void addCoins(float ammount)
    {
        coins += ammount;
    }
    public void subtractCoins(float ammount)
    {
        coins -= ammount;
    }

    public void updateCoinUI()
    {
        coinText.text = Mathf.FloorToInt(FindFirstObjectByType<PlayerData>().coins).ToString();
    }

    private void whenToPhase()
    {
        if (rb.linearVelocityY > 0.5 || Input.GetKey(KeyCode.S))
        {
            ignorePlatforms = true;
        }
        else
        {
            ignorePlatforms = false;
        }
    }


    public void die()
    {
        Time.timeScale = 0f;
        deathScreen.SetActive(true);
    }
}
