using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> _path = new List<Waypoint>();
    float _waitTime = 1f;
    private void Start()
    {
        FindPath();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject waypoint in waypoints)
        {
            _path.Add(waypoint.GetComponent<Waypoint>());
        }
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
                _travelPercent += Time.deltaTime;
                transform.position = Vector3.Lerp(_startPose, _endPose, _travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
