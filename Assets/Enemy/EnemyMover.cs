using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> _path = new List<Waypoint>();
    float _waitTime = 1f;
    [SerializeField] [Range(0f, 5f)] float _speed = 1f;

    Enemy _enemy;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();    
    }

    private void FindPath()
    {
        _path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject waypoint in waypoints)
        {
            _path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = _path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in _path)
        {

            Vector3 _startPose = transform.position;
            Vector3 _endPose = waypoint.transform.position;
            float _travelPercent = 0f;

            transform.LookAt(_endPose);

            while (_travelPercent < 1f)
            {
                _travelPercent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(_startPose, _endPose, _travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        _enemy.PenaltyGold();
        gameObject.SetActive(false);
    }
}
