using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;

    void Start()
    {
        grid = new GameGrid(Cell);
        grid.PopulateGrid();
        grid.DrawGrid();
    }

    void Update()
    {
        grid.UpdateGridColour();
    }
}
