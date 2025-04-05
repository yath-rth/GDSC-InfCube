using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static player instance;

    Controls controls;
    CharacterController cc;
    GameManager gameManager;

    [SerializeField] Transform cam;
    [SerializeField, Range(0, 10f)] float minSpeed, maxSpeed;
    [SerializeField, Range(0, 1f)] float speedIncrement;
    [SerializeField, Range(0, 100f)] float gravity, rotationSpeed;

    int side = 0;
    float _speed = 0;
    Vector3 velocity, move, cameraOffset;
    Quaternion rotation;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;

        cc = GetComponent<CharacterController>();
        gameManager = GameManager.instance;

        controls = new Controls();

        controls.movement.left.performed += ctx => left();
        controls.movement.right.performed += ctx => right();
        controls.movement.escape.performed += ctx => gameManager.pauseResume();
        controls.movement.mainMenu.performed += ctx => gameManager.mainMenu();
        controls.movement.space.performed += ctx => gameManager.restart();

        if (cam != null) cameraOffset = cam.position - transform.position;

        _speed = minSpeed;
    }

    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        if (cc != null)
        {
            if (transform.position.y < -1 && gameManager != null) gameManager.GameOver();

            if (!cc.isGrounded) velocity.y -= gravity * Time.deltaTime;
            else velocity.y = 0;

            if (!gameManager.isGameOver)
            {
                _speed += speedIncrement * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, minSpeed, maxSpeed);

                move = new Vector3(side * _speed, velocity.y, _speed).normalized;
                cc.Move(move * Time.deltaTime * _speed);

                rotation = Quaternion.Euler(0, side * 45f, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                if (cam != null) cam.position = transform.position + cameraOffset;
            }
            else
            {
                move = new Vector3(0, velocity.y, 0);
                cc.Move(move * Time.deltaTime);
            }
        }
    }

    public void left()
    {
        side = -1;
    }

    public void right()
    {
        side = 1;
    }
}
