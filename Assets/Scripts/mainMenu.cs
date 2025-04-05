using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    Controls controls;
    sceneManager sceneManager;

    private void Awake()
    {
        sceneManager = GetComponent<sceneManager>();
        controls = new Controls();

        controls.movement.space.performed += ctx => Play();
        controls.movement.shop.performed += ctx => shop();
    }
    
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }

    void Play()
    {
        sceneManager.Game();
    }

    void shop(){

    }
}
