using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float height;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject spell;
    [SerializeField] GameObject wand;

    Vector2 moveInput;
    Vector2 cameraInput;

    public float maxTurnAngle = 90.0f;
    public float minTurnAngle = -90.0f;
    private float rotX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Turn();
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
        //rb.linearVelocity = playerVelocity;
        transform.Translate(playerVelocity * Time.deltaTime, Space.World);
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
        Instantiate(spell, wand.transform.position, wand.transform.rotation);
    }



}
