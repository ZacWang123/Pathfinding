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
                if (num < 2) {
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
                        Cell.material.color = new Color32(124, 252, 0, 255);
                        break;
                    case 3:
                        Cell.material.color = new Color32(255, 0, 0, 255);
                        break;
                    case 4:
                        Cell.material.color = new Color32(255, 255, 0, 255);
                        break;
                }
            }
        }
    }

    public void UpdateGrid(int row, int col, int ID) {
        Grid[row, col] = ID;
    }

    public bool WithinGrid(int row, int col) {
        if (!(row >= 0 && row < Rows && col >= 0 && col < Cols)) {
            return false;
        }
        return true;
    }

    public int GetCell(int row, int col) {
        return Grid[row, col];
    }
}
