using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class InputNavigationManager : MonoBehaviour
{
    public GameObject defaultSelectedButton;

    private Vector3 lastMousePosition;
    private bool usingMouse = false;

    // Update is called once per frame
    void Update()
    {
        DectectinginputMethod();
    }

    private void DectectinginputMethod()
    {
        if (Input.mousePresent && (Input.mousePosition != lastMousePosition))
        {
            if (!usingMouse)
            {
                usingMouse = true;
                EventSystem.current.SetSelectedGameObject(null);
            }

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            if (!usingMouse)
            {
                usingMouse = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        if (isKeyboardOrControllerInput())
        {
            if (usingMouse)
            {
                usingMouse = false;

                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
                }
            }
        }
    }

    private bool isKeyboardOrControllerInput()
    {
        return Input.GetAxisRaw("Vertical") != 0 ||
            Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetButtonDown("Submit") ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow);
    }
}
