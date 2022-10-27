using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinatValues : MonoBehaviour
{

    private Color defaultColor = Color.white;
    private Color blockedColor = Color.blue; 
    private Color exploredColor = Color.yellow;
    private Color pathColor = new Color(1f,0.5f,0f);
    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

     void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        } 
        PlaceColors();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void DisplayCoordinates()
    {
        if(gridManager == null)
        {
            return;
        }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void PlaceColors()
    {
        if(gridManager == null)
        {
            return;
        }
        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            return ;
        }
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        if (node.isPath)
        {
            label.color = pathColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
