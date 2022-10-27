using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 3f)]  float speed = 1f;
    [SerializeField] int heal;
    int StartHeal = 15;
    int Bonus = 5;
    int HitPoint = 1;
    Enemy enemy;
    GridManager gridManager;
    pathfinder pathfinder;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<pathfinder>();
    }
    void OnEnable()
    {
        heal = StartHeal;
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(PathNames());

    }
    void ReturnToStart()
    {
        gameObject.SetActive(true);
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
    void OnParticleCollision(GameObject other)
    {
        heal -= HitPoint;
        if(heal <= 0)
        {
            enemy.GoldReward();
            gameObject.SetActive(false);
            StartHeal += Bonus;
        }
    }
    
    IEnumerator PathNames()
    {
        for(int i = 1; i< path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime* speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        enemy.GoldPenalty();
        gameObject.SetActive(false);
    }
}
