using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Manager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;
    public int Rows = 20, Cols = 20;

    void Start()
    {
        grid = new GameGrid(Cell);
        grid.PopulateGrid();
        grid.DrawGrid();

        SpawnStart();
        SpawnEnd();

        grid.UpdateGridColour();

        BeginAlgorithm();
    }

    public void SpawnStart()
    {
        grid.UpdateGrid(0, 0, 2);
    }

    public void SpawnEnd()
    {
        grid.UpdateGrid(Rows - 1, Cols - 1, 3);
    }




    public List<Positions> GetNeighbors(Positions currentCell)
    {
        List<Positions> neighbors = new List<Positions>();

        if (grid.WithinGrid(currentCell.Row - 1, currentCell.Col) && grid.GetCell(currentCell.Row - 1, currentCell.Col) != 1)
        {
            neighbors.Add(new Positions(currentCell.Row - 1, currentCell.Col));
        }

        if (grid.WithinGrid(currentCell.Row + 1, currentCell.Col) && grid.GetCell(currentCell.Row + 1, currentCell.Col) != 1)
        {
            neighbors.Add(new Positions(currentCell.Row + 1, currentCell.Col));
        }

        if (grid.WithinGrid(currentCell.Row, currentCell.Col + 1) && grid.GetCell(currentCell.Row, currentCell.Col) != 1)
        {
            neighbors.Add(new Positions(currentCell.Row, currentCell.Col + 1));
        }

        if (grid.WithinGrid(currentCell.Row, currentCell.Col - 1) && grid.GetCell(currentCell.Row, currentCell.Col - 1) != 1)
        {
            neighbors.Add(new Positions(currentCell.Row, currentCell.Col - 1));
        }

        return neighbors;
    }

    public void Astar(GameGrid grid, Positions start, Positions end)
    {
        bool[,] closed = new bool[Rows, Cols];

        Cell[,] cells = new Cell[Rows, Cols];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                cells[i, j].f = double.MaxValue;
                cells[i, j].g = double.MaxValue;
                cells[i, j].h = double.MaxValue;
                cells[i, j].parentI = -1;
                cells[i, j].parentJ = -1;
            }
        }

        cells[start.Row, start.Col].f = 0.0;
        cells[start.Row, start.Col].g = 0.0;
        cells[start.Row, start.Col].h = 0.0;
        cells[start.Row, start.Col].parentI = start.Row;
        cells[start.Row, start.Col].parentJ = start.Col;

        SortedSet<(double, Positions)> openList = new SortedSet<(double, Positions)>(Comparer<(double, Positions)>.Create((a, b) => a.Item1.CompareTo(b.Item1)));

        openList.Add((0.0, new Positions(start.Row, start.Col)));

        print("asd");

/*        bool complete = false;

        while (openList.Count > 0)
        {
        }*/


    }


    public void BeginAlgorithm()
    {
        Astar(grid, new Positions(0, 0), new Positions(19, 19));

    }
}

