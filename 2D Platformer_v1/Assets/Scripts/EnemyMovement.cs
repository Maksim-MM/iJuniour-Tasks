using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    
    private int _currentWaypoint = 0;
    
    private Quaternion _turnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion _turnRight = Quaternion.identity;

    private void FixedUpdate()
    {
        RouteMove();
    }

    private void RouteMove()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (++_currentWaypoint) % _waypoints.Length;
            Rotate(_waypoints[_currentWaypoint].position - transform.position);
        }

        transform.position = Vector2.MoveTowards(
            transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
    
    private void Rotate(Vector2 direction)
    {
        float velocityX = direction.x;
        transform.localRotation = velocityX > 0 ? _turnLeft : _turnRight;
    }
}
