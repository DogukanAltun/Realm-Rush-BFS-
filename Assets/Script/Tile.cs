using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] GameObject Defender;
    [SerializeField] private bool isPlaceable = false;
    [SerializeField] private int Cost = 50;
    pathfinder pathfinder;
    float wait = 0.2f;
    Bank bank;
    public bool IsPlaceable { get { return isPlaceable; } }
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    void Awake()
    {
     gridManager = FindObjectOfType<GridManager>();   
     pathfinder = FindObjectOfType<pathfinder>();
    }
    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
        bank = FindObjectOfType<Bank>();
    }
    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            if (bank.currentCurrency >= Cost)
            {
                Debug.Log(transform.name);
                StartCoroutine(Build());

                Instantiate(Defender, transform.position,Quaternion.identity);

                isPlaceable = false;
                bank.currentCurrency -= Cost;
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
            else
            {
                Debug.Log("You Dont Have Enough Money");
                return;
            }
        }
    }

    IEnumerator Build()
    {
       foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(wait);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
