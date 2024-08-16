using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static Action OnMouseButtonPressed;

    public static Action<int> OnDirectionButtonPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonPressed?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnDirectionButtonPressed?.Invoke(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            OnDirectionButtonPressed?.Invoke(1);
        }
    }
}
