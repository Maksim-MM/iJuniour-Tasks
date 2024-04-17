using System;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int _healValue = 10;
    
    public event Action AidKitCollected;
    
    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            AidKitCollected?.Invoke();
            if (player.TryGetComponent(out Health health)) health.TakeCure(_healValue);
            gameObject.SetActive(false);
        }
    }
}
