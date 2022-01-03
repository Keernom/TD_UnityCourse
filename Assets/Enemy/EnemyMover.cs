using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float _speed = 1f;

    List<Node> _path = new List<Node>();

    Enemy _enemy;
    GridManager _gridManager;
    PathFinder _pathFinder;

    private void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<PathFinder>();
    }

    private void RecalculatePath()
    {
        _path.Clear();

        _path = _pathFinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathFinder.StartCoords);
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            Vector3 _startPose = transform.position;
            Vector3 _endPose = _gridManager.GetPositionFromCoordinates(_path[i]._coordinates);
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
