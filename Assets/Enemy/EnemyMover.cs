using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> _path = new List<Tile>();
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

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();

            if (waypoint != null)
            {
                _path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = _path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in _path)
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

        FinishPath();
    }

    private void FinishPath()
    {
        _enemy.PenaltyGold();
        gameObject.SetActive(false);
    }
}
