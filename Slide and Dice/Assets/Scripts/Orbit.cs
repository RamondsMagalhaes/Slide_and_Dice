using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Orbit : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }

    void faceMouse()
    {
        if (GameManager.controller == 0)
        {
            Vector2 mousePosition = playerInputActions.Player.Orbit.ReadValue<Vector2>();
            //Debug.Log(mousePosition);
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position;

            transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 4);
            //transform.up = direction;
        }
        else
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(v, h, 0);
            if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
            {
                transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 8);
            }      
            Debug.Log(direction);

        }

    }
}
