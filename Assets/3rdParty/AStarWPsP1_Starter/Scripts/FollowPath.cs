using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform _goal;
    private float _speed = 5.0f;
    private float _accuracy = 1.0f;
    private float _rotSpeed = 2.0f;
    
    [SerializeField] private GameObject _waypointManager;
    private GameObject[] _waypoints;
    private GameObject _currentNode;
    private int _currentWaypoint;
    private Graph _graph;

    private void Start()
    {
        _waypoints = _waypointManager.GetComponent<WaypointManager>().waypoints;
        _graph = _waypointManager.GetComponent<WaypointManager>().graph;
        _currentNode = _waypoints[7];
    }

    public void GoToHeli()
    {
        _graph.AStar(_currentNode, _waypoints[2]);
        _currentWaypoint = 0;
    }

    public void GoToRuin()
    {
        _graph.AStar(_currentNode, _waypoints[5]);
        _currentWaypoint = 0;
    }
    public void GoToTank()
    {
        _graph.AStar(_currentNode, _waypoints[0]);
        _currentWaypoint = 0;
    }

    
    
    private void LateUpdate()
    {
        if(_graph.getPathLength() == 0 || _currentWaypoint == _graph.getPathLength())
            return;

        _currentNode = _graph.getPathPoint(_currentWaypoint);

        if (Vector3.Distance(
                _graph.getPathPoint(_currentWaypoint).transform.position,
                transform.position) < _accuracy)
        {
            _currentWaypoint++;
        }

        if (_currentWaypoint < _graph.getPathLength())
        {
            _goal = _graph.getPathPoint(_currentWaypoint).transform;
            Vector3 lookAtGoal = new Vector3(_goal.position.x,
                transform.position.y,
                _goal.position.z);

            Vector3 dir = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(dir), _rotSpeed * Time.deltaTime);
            
            transform.Translate(0,0,_speed*Time.deltaTime);
        }
    }
}
