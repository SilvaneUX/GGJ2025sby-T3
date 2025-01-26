using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            PlayerGrab.Instance.buttonPourPressed = true;
        }
        else
        {
            PlayerGrab.Instance.buttonPourPressed = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
