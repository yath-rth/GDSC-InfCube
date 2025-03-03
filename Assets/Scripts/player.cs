using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Controls controls;
    CharacterController cc;
    Transform playerMesh;

    [SerializeField, Range(0, 100)] float speed, gravity, rotationSpeed;

    int side = 0;
    Vector3 velocity, move;
    Quaternion rotation;

    void Awake()
    {
        controls = new Controls();

        controls.movement.left.performed += ctx => left();
        controls.movement.right.performed += ctx => right();

        cc = GetComponent<CharacterController>();

        playerMesh = transform.GetChild(0).transform;
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
            if (!cc.isGrounded) velocity.y -= gravity * Time.deltaTime;
            else velocity.y = 0;

            move = new Vector3(side * speed, velocity.y, speed);
            cc.Move(move * Time.deltaTime);

            rotation = Quaternion.Euler(0, side * 45f, 0);
            playerMesh.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    void left()
    {
        side = -1;
    }

    void right()
    {
        side = 1;
    }
}
