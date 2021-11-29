using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody thisRigidbody;
    PlayerInputActions playerInputActions;
    public float speed;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Pause.started += context => Pause();
        playerInputActions.Player.Enable();
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        
    }
    public void MovePlayer()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 directionVector = new Vector3( inputVector.x, inputVector.y, 0 );
        thisRigidbody.AddForce(directionVector * speed, ForceMode.Force);
    }
    void Pause()
    {
        if (!GameManager.isDead) FindObjectOfType<UIStage>().PauseScreen(GameManager.IsGameActive);

    }

}
