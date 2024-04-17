using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action MoneyCollected;
  
    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            MoneyCollected?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}
