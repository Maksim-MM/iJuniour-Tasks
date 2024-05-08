using UnityEngine;

[RequireComponent ( typeof ( MeshRenderer ) )]
public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;
    private float _scaleRatio = 2f;
    private float _spawnChanсe = 100f;
    private float _spawnChanceRatio = 2f;
    private Vector3 _defaultScale = new Vector3(5, 5, 5);

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        transform.localScale = _defaultScale;
    }

    private void Start()
    {
        SetRandomColor();
    }

    public Vector3 GetSpawnScale()
    {
        return transform.localScale / _scaleRatio;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public float GetSpawnChanсe()
    {
        return _spawnChanсe/ _spawnChanceRatio;
    }

    public void SetSpawnChance(float value)
    {
        _spawnChanсe = value;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
