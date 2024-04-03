using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(MouseInput))]
public class CounterView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private MouseInput _mouseInput;
    
    private bool _iswork = true;
    private int _currentCount = 0;
    private float _timerScale = 0.5f;
    private Coroutine _countCoroutine;
    
    private void OnEnable()
    {
        _mouseInput.MouseClicked += ToggleCounter;
    }
    private void OnDisable()
    {
        _mouseInput.MouseClicked -= ToggleCounter;
    }
    
    private void Start()
    {
        _countCoroutine = StartCoroutine(Count());
    }
    
    private void ToggleCounter()
    {
        if (_iswork) 
        {
            StopCoroutine(_countCoroutine);
        }
        else
        {
            _countCoroutine = StartCoroutine(Count());
        }
        
        _iswork = !_iswork;
    }
    
    private IEnumerator Count()
    {
        var wait = new WaitForSeconds(_timerScale);
    
        for (int i = _currentCount; i >= 0; i++)
        {
            DisplayCount(i);
            _currentCount = i;
            yield return wait;
        }
    }

    private void DisplayCount(int count)
    {
        _text.text = count.ToString();
    }
}
