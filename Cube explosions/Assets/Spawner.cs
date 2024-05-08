using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _explosionForce = 2f;
    [SerializeField] private float _explosionRadius = 5f;

    private Ray _ray;
    private Cube _cube;
    private Vector3 _spawnScale;
    private float _spawnChance;
    private float _minCubes = 2; 
    private float _maxCubes = 7;
    
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform.TryGetComponent(out _cube) == false)
                {
                    return;
                }
                
                Transform objectHit = hit.transform;
                
                _spawnChance = _cube.GetSpawnChanсe();
                _spawnScale = _cube.GetSpawnScale();
                
                float random = GetRandom(0, 101);
                
                 if (_spawnChance >= random )
                 {
                     SpawnCubes(objectHit.position, _spawnScale);
                     Destroy(objectHit.gameObject);
                }
                 else
                 {
                     Destroy(objectHit.gameObject);
                 }
            }
        }
    }

    private void SpawnCubes(Vector3 spawnPosition, Vector3 spawnScale)
    {
        float numberCubes = GetRandom(_minCubes,_maxCubes);

        for (int i = 0; i < numberCubes; i++)
        {
            Cube cube = Instantiate(_cube, spawnPosition, Quaternion.identity);
                
                cube.SetScale(spawnScale);
                cube.SetSpawnChance(_spawnChance);
                cube.SetRandomColor();
                
                cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _cube.transform.position, _explosionRadius);
        }
    }
    
    private float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }
}
