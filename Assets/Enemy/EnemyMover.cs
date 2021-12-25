using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> _path = new List<Waypoint>();
    float _waitTime = 1f;
    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in _path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
