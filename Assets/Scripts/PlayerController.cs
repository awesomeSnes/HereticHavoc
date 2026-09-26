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

    Vector2 moveInput;
    Vector2 cameraInput;

    public float maxTurnAngle = 90.0f;
    public float minTurnAngle = -90.0f;
    private float rotX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Turn();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector3 playerVelocity = (moveInput.x * moveSpeed * transform.right) + (moveInput.y * moveSpeed * transform.forward);
        //rb.linearVelocity = playerVelocity;
        transform.Translate(playerVelocity * Time.deltaTime, Space.World);
    }

    void Turn()
    {
    float y = Input.GetAxis("Mouse X") * turnSpeed;
    rotX += Input.GetAxis("Mouse Y") * turnSpeed;
    rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + y, 0);
    camera.transform.localEulerAngles = new Vector3(-rotX, 0, 0);
    }



}
