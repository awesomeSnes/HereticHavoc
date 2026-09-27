using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float height;
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] float shotDelay;
    [SerializeField] Slider healthBar;
    [SerializeField] Image nextSpell;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float invincibilityTime;
    [SerializeField] GameObject camera;
    [SerializeField] List<GameObject> spellList;
    [SerializeField] GameObject wand;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] LevelController levelController;
    [SerializeField] GameObject currentSpell;
    [SerializeField] List<GameObject> currentSpellList;
    List<GameObject> tempSpellList;
    Vector2 moveInput;
    Vector2 cameraInput;

    private int spellIndex;
    public int score = 0;
    private float maxTurnAngle = 90.0f;
    private float minTurnAngle = -90.0f;
    private float rotX;
    private bool invincible;
    private bool canShoot = true;

    void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UpdateScore();
        UpdateShotList();
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Turn();
    }

    public void UpdateScore(int points)
    {
        score += points;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        levelController.UpdateLevel();
    }

    //Updates movement when wsad is pressed  
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //Moves the player
    void Run()
    {
        Vector3 playerVelocity = (moveInput.x * moveSpeed * transform.right) + (moveInput.y * moveSpeed * transform.forward);
        rb.linearVelocity = playerVelocity;
    }

    //Rotates the camera
    void Turn()
    {
    
    float y = Input.GetAxis("Mouse X") * turnSpeed;
    
    //caps the up and down looking
    rotX += Input.GetAxis("Mouse Y") * turnSpeed;
    rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    
    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + y, 0);
    camera.transform.localEulerAngles = new Vector3(-rotX, 0, 0);
    wand.transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }

    //Creates Spells
    void OnShoot()
    {
        if(canShoot)
        {

            Instantiate(currentSpell, wand.transform.position, wand.transform.rotation);
            StartCoroutine(Reload());
            PickNextShot();
        }
    }

    //Loads the next spell
    void PickNextShot()
    {
        if(spellIndex >= currentSpellList.Count)
        {
            UpdateShotList();
            return;
        }
        currentSpell = currentSpellList[spellIndex];
        nextSpell.sprite = currentSpell.GetComponent<Spell>().spellIcon; 
        spellIndex++;
    }

    //Creates a list of random spells
    void UpdateShotList()
    {
        tempSpellList = new List<GameObject>();
        tempSpellList.AddRange(spellList);
        tempSpellList.AddRange(spellList);
        currentSpellList = new List<GameObject>();
        for(int i = 0; i < tempSpellList.Count; i++)
        {
            if(i==0)
            {
                currentSpellList.Add(tempSpellList[i]);
            }
            currentSpellList.Insert(Random.Range(0, currentSpellList.Count), tempSpellList[i]);
        }
        currentSpellList.RemoveAt(currentSpellList.Count - 1);
        currentSpellList.RemoveAt(currentSpellList.Count - 1);
        spellIndex = 0;
        PickNextShot();
    }

    public void Hurt(int damage)
    {
        if(invincible){ Debug.Log("Invincible"); return;}
        hurtSound.Play();
        health -= damage;
        UpdateHealthBar();
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        StartCoroutine(IFrames());
    }

    void UpdateHealthBar()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }



}
