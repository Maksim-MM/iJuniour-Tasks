using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action MouseClicked;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseClicked?.Invoke();
        }
    }
}
