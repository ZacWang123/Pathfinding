using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameGrid
{
    public int Rows = 20, Cols = 20;
    public int[,] Grid;
    public GameObject GridCell;
    public Renderer[,] VisualGrid;

    public GameGrid(GameObject cell) {
        Grid = new int[Rows, Cols];
        GridCell = cell;
    }

    public void PopulateGrid() {
        for (int i = 0; i < Rows; i++) {
            for (int j = 0; j < Cols; j++)
            {
                int num = Random.Range(0, 10);
                if (num < 3) {
                    Grid[i, j] = 1;
                }
            }
        }
    }

    public void DrawGrid()
    {
        VisualGrid = new Renderer[Rows, Cols];

        for (int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Cols; cols++)
            {
                GameObject Cell = Object.Instantiate(GridCell, new Vector2(rows, cols), Quaternion.identity);
                VisualGrid[rows, cols] = Cell.GetComponent<Renderer>();
            }
        }
    }

    public void UpdateGridColour()
    {
        for (int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Cols; cols++)
            {
                Renderer Cell = VisualGrid[rows, cols];

                switch (Grid[rows, cols])
                {
                    case 0:
                        Cell.material.color = new Color(105, 105, 105);
                        break;
                    case 1:
                        Cell.material.color = new Color(0, 0, 0);
                        break;
                    case 2:
                        Cell.material.color = new Color(255, 192, 203);
                        break;
                    case 3:
                        Cell.material.color = new Color(255, 255, 0);
                        break;
                }
            }
        }
    }
}
