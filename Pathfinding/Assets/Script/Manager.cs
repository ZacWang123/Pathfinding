using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Manager : MonoBehaviour
{
    public GameObject cellSprite;
    public GameGrid grid;
    public int Rows = 20, Cols = 20;

    void Start()
    {
        grid = new GameGrid(cellSprite);

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
        grid.UpdateGrid(10, 10, 3);
    }

    public void Astar(GameGrid grid, Positions start, Positions end)
    {
        bool[,] closedList = new bool[Rows, Cols];


        Cell[,] cells = new Cell[Rows, Cols];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                cells[i, j] = new Cell();
                cells[i, j].f = double.MaxValue;
                cells[i, j].g = double.MaxValue;
                cells[i, j].h = double.MaxValue;
               
     
                cells[i, j].parentI = -1;
                cells[i, j].parentJ = -1;
            }
        }

        int x = start.Row;
        int y = start.Col;

        cells[x, y].f = 0.0;
        cells[x, y].g = 0.0;
        cells[x, y].h = 0.0;
        cells[x, y].parentI = x;
        cells[x, y].parentJ = y;

        SortedSet<(double, Positions)> openList = new SortedSet<(double, Positions)>(Comparer<(double, Positions)>.Create((a, b) => a.Item1.CompareTo(b.Item1)));
      
        openList.Add((0.0, new Positions(start.Row, start.Col)));



        bool complete = false;

        while (openList.Count > 0)
        {
            (double f, Positions position) p = openList.Min;
            openList.Remove(p);

            x = p.position.Row;
            y = p.position.Col;
            closedList[x,y] = true;

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {

                    if ((i == 0 && j == 0) || (i == 1 && j == 1) || (i == -1 && j == -1) || (i == -1 && j == 1) || (i == 1 && j == -1))
                    {
                        continue;
                    }
                    /*                    if (i == 0 && j == 0)
                                        {
                                            continue;
                                        }*/

                    int newX = x + i;
                    int newY = y + j;

/*                    if (newX == 10 && newY == 11 || newX == 10 && newY == 9 || newX == 11 && newY == 10 || newX == 9 && newY == 10)
                    {
                        print(cells[])
                    }*/



                    if (grid.WithinGrid(newX, newY)) {
/*                        print("for" + x + "and" + y + ": " + newX + " " + newY);*/
                        if (newX == end.Row && newY == end.Col) {
                            cells[newX, newY].parentI = x;
                            cells[newX, newY].parentJ = y;
                            print("The destination cell is found");
/*                            TracePath(cells, end);*/
                            complete = true;
                            return;
                        }

                        if (!closedList[newX,newY] && grid.GetCell(newX, newY) == 0) {
                            /*                            print("for" + x + " " + y + ": " + newX + " " + newY);*/
                            /*                            double gNew = cells[x, y].g + 1.0;*/
                            double gNew = cells[x, y].g + 1.0;
                            double hNew = Math.Sqrt(Math.Pow(newX - end.Row, 2) + Math.Pow(newY - end.Col, 2));
/*                            double hNew = Math.Abs(newX - end.Row) + Math.Abs(newY - end.Col);*/
                            double fNew = gNew + hNew;
                            if (!(x == 0 && y == 0)) {
                                grid.UpdateGrid(x, y, 5);
                            }

                            if (cells[newX, newY].f == double.MaxValue || cells[newX, newY].f > fNew) {
                                openList.Add((fNew, new Positions(newX, newY)));
                                cells[newX, newY].f = fNew;
                                grid.VisualGrid[newX, newY].textComponent.text = Mathf.RoundToInt((float)fNew).ToString();
                                cells[newX, newY].g = gNew;
                                cells[newX, newY].h = hNew;
                                cells[newX, newY].parentI = x;
                                cells[newX, newY].parentJ = y;
                            }
                        }
                    }
                }
            }
        }
        if (!complete)
            print("Failed to find the Destination Cell");
    }

/*    public static void TracePath(Cell[,] cells, Positions end)
    {
        print(" The Path is ");
        int row = end.Row;
        int col = end.Col;

        Stack<Positions> Path = new Stack<Positions>();

        while (!(cells[row, col].parentI == row && cells[row, col].parentJ == col))
        {
            Path.Push(new Positions(row, col));
            int temp_row = cells[row, col].parentI;
            int temp_col = cells[row, col].parentJ;
            row = temp_row;
            col = temp_col;
        }

        Path.Push(new Positions(row, col));
        while (Path.Count > 0)
        {
            Positions p = Path.Peek();
            Path.Pop();
            Console.Write(" -> ({0},{1}) ", p.Row, p.Col);
        }
    }*/

    public void BeginAlgorithm()
    {
        Astar(grid, new Positions(0,0), new Positions(10, 10));

    }

    public void Update() {
        grid.UpdateGridColour();
    }

}

