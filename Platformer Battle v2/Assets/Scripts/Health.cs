using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _value;
    
    private bool IsAlive => _value > 0;
    
    public void TakeDamage(int damage)
    {
        _value -= damage;
        
        if (IsAlive == false)
        {
            Die();
        }
    }

    public void TakeCure(int value)
    {
        _value += value;
    }

    private void Die()
    {
        gameObject.SetActive(false);    
    }
}
