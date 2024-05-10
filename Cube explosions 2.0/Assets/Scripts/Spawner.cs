using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 200f;

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
                     MakeExplosion(objectHit);
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

    private void MakeExplosion(Transform explosionCenter)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(explosionCenter))
        {
            float explosionModifier = explodableObject.transform.localScale.x;
            float distance = Vector3.Distance(explodableObject.position, explosionCenter.position);

            if (distance > 0)
            {
                float adjustedForce = _explosionForce / (distance * distance) / explosionModifier;
                Vector3 direction = explodableObject.position - explosionCenter.position;

                explodableObject.AddForce(direction * adjustedForce, ForceMode.Impulse);
            }
        }
    }

    private List<Rigidbody> GetExplodableObjects(Transform center)
    {
        float detectionRadius = _explosionRadius / center.localScale.x;
                                  
        Collider[] hits = Physics.OverlapSphere(center.position, detectionRadius, _layerMask);

        List<Rigidbody> cubes = new();

        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }

    private float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }
}
