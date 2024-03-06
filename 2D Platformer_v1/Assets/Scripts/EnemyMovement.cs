using UnityEngine;

[RequireComponent ( typeof ( SpriteRenderer ) )]
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    
    private SpriteRenderer _enemyRenderer;
    private int _currentWaypoint = 0;

    private void Start()
    {
        _enemyRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (++_currentWaypoint) % _waypoints.Length;
            FlipRenderSide();
        }

        transform.position = Vector2.MoveTowards(
            transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    private void FlipRenderSide()
    {
        _enemyRenderer.flipX = !_enemyRenderer.flipX;
    }
}
