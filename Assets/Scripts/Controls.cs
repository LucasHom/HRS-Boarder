using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    //[SerializeField] InputAction PlayerInput;
    private bool pressingLeft;
    private bool pressingRight;
    private bool pressingDown;

    private bool pressingLeftDown;

    // Start is called before the first frame update
    void Start()
    {
        pressingLeftDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLeftPressed(InputAction.CallbackContext action)
    {
        pressingLeft = action.ReadValueAsButton();

        if (action.started || action.performed)
        {
            pressingLeftDown = !pressingLeftDown;
        }
        //Debug.Log(pressingLeft);
    }

    public void onRightPressed(InputAction.CallbackContext action)
    {
        pressingRight = action.ReadValueAsButton();
        //Debug.Log(pressingRight);
    }

    public void onDownPressed(InputAction.CallbackContext action)
    {
        pressingDown = action.ReadValueAsButton();
        //Debug.Log(pressingDown);
    }


    public bool isRotatingLeft()
    {
        if (pressingLeft)
        {
            return true;
        }
        return false;
    }
    public bool isRotatingRight()
    {
        if (pressingRight)
        {
            return true;
        }
        return false;
    }
    public bool isTucking()
    {
        if (pressingDown)
        {
            return true;
        }
        return false;
    }

}
