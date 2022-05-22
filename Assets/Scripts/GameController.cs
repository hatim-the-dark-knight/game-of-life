using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class GameObjectList {
        public List<GameObject> gameObjects;
    }
    public GameObjectList[] cells;

    public GameObject simulatingScreen, cellGameObject;
    Cell cell;
    public int[,] noOfNeighbours = new int[14,24];

    // Start is called before the first frame update
    void Start()
    {
        cell = FindObjectOfType<Cell> ();
        BuildCells ();
    }

    // Update is called once per frame
    void Update()
    {
        noOfNeighbours = CalculateNeighbours ();
    }

    void BuildCells () {
        for (int  row = 0; row < 14; row++) {
            for (int col = 0; col < 24; col++) {
                cells[row].gameObjects[col] = Instantiate (cellGameObject);
                cells[row].gameObjects[col].transform.SetParent (simulatingScreen.transform, false);
            }
        }
    }

    public void GameLogic () {
        for (int row = 1; row < 13; row++) {
            for (int col = 1; col < 23; col++) {
                if ((noOfNeighbours[row, col] > 3 || noOfNeighbours[row, col] < 2) && cells[row].gameObjects[col].GetComponent<Cell> ().status) {
                    cells[row].gameObjects[col].GetComponent<Cell> ().SetCellProps ();
                }
                else if (noOfNeighbours[row, col] == 3 && !cells[row].gameObjects[col].GetComponent<Cell> ().status) {
                    cells[row].gameObjects[col].GetComponent<Cell> ().SetCellProps ();
                }
            }
        }
        
    }

    int[,] CalculateNeighbours () {
        
        for (int  row = 1; row < 13; row++) {
            for (int col = 1; col < 23; col++) {
                noOfNeighbours[row, col] = CalculateNeighboursForEachInwardCell (row, col);
                cells[row].gameObjects[col].GetComponentInChildren<Text> ().text = noOfNeighbours[row, col].ToString ();
            }
        }
        for (int row = 1; row < 13; row++) {
            int col = 0;
            noOfNeighbours[row, col] = CalculateNeighboursForFirstCol (row, col);
            cells[row].gameObjects[col].GetComponentInChildren<Text> ().text = noOfNeighbours[row, col].ToString ();
            col = 23;
            noOfNeighbours[row, col] = CalculateNeighboursForLastCol (row, col);
            cells[row].gameObjects[col].GetComponentInChildren<Text> ().text = noOfNeighbours[row, col].ToString ();
        }

        for (int col = 1; col < 23; col++) {
            int row = 0;
            noOfNeighbours[row, col] = CalculateNeighboursForFirstRow (row, col);
            cells[row].gameObjects[col].GetComponentInChildren<Text> ().text = noOfNeighbours[row, col].ToString ();
            row = 13;
            noOfNeighbours[row, col] = CalculateNeighboursForLastRow (row, col);
            cells[row].gameObjects[col].GetComponentInChildren<Text> ().text = noOfNeighbours[row, col].ToString ();
        }

        noOfNeighbours[0, 0] = CalculateNeighboursFor1stCorner (0, 0);
        cells[0].gameObjects[0].GetComponentInChildren<Text> ().text = noOfNeighbours[0, 0].ToString ();
        noOfNeighbours[0, 23] = CalculateNeighboursFor2ndCorner (0, 23);
        cells[0].gameObjects[23].GetComponentInChildren<Text> ().text = noOfNeighbours[0, 23].ToString ();
        noOfNeighbours[13, 0] = CalculateNeighboursFor3rdCorner (13, 0);
        cells[13].gameObjects[0].GetComponentInChildren<Text> ().text = noOfNeighbours[13, 0].ToString ();
        noOfNeighbours[13, 23] = CalculateNeighboursFor4thCorner (13, 23);
        cells[13].gameObjects[23].GetComponentInChildren<Text> ().text = noOfNeighbours[13, 23].ToString ();

        return noOfNeighbours;
    }

    int CalculateNeighboursForEachInwardCell (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [8];
        n[0] = cells[row-1].gameObjects[col-1].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[2] = cells[row-1].gameObjects[col+1].GetComponent<Cell> ();
        n[3] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        n[4] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        n[5] = cells[row+1].gameObjects[col-1].GetComponent<Cell> ();
        n[6] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        n[7] = cells[row+1].gameObjects[col+1].GetComponent<Cell> ();
        for (int i = 0; i < 8; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursForFirstCol (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [5];
        n[0] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col+1].GetComponent<Cell> ();
        n[2] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        n[3] = cells[row+1].gameObjects[col+1].GetComponent<Cell> ();
        n[4] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        for (int i = 0; i < 5; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursForLastCol (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [5];
        n[0] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col-1].GetComponent<Cell> ();
        n[2] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        n[3] = cells[row+1].gameObjects[col-1].GetComponent<Cell> ();
        n[4] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        for (int i = 0; i < 5; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursForFirstRow (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [5];
        n[0] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        n[1] = cells[row+1].gameObjects[col-1].GetComponent<Cell> ();
        n[2] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        n[3] = cells[row+1].gameObjects[col+1].GetComponent<Cell> ();
        n[4] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        for (int i = 0; i < 5; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursForLastRow (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [5];
        n[0] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col-1].GetComponent<Cell> ();
        n[2] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[3] = cells[row-1].gameObjects[col+1].GetComponent<Cell> ();
        n[4] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        for (int i = 0; i < 5; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursFor1stCorner (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [3];
        n[0] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        n[1] = cells[row+1].gameObjects[col+1].GetComponent<Cell> ();
        n[2] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        for (int i = 0; i < 3; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursFor2ndCorner (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [3];
        n[0] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        n[1] = cells[row+1].gameObjects[col-1].GetComponent<Cell> ();
        n[2] = cells[row+1].gameObjects[col].GetComponent<Cell> ();
        for (int i = 0; i < 3; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursFor3rdCorner (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [3];
        n[0] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col+1].GetComponent<Cell> ();
        n[2] = cells[row].gameObjects[col+1].GetComponent<Cell> ();
        for (int i = 0; i < 3; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    int CalculateNeighboursFor4thCorner (int row, int col) {
        int noOfN = 0;
        Cell[] n = new Cell [3];
        n[0] = cells[row-1].gameObjects[col].GetComponent<Cell> ();
        n[1] = cells[row-1].gameObjects[col-1].GetComponent<Cell> ();
        n[2] = cells[row].gameObjects[col-1].GetComponent<Cell> ();
        for (int i = 0; i < 3; i++) {
            if (n[i].status == true) {
                noOfN++;
            }
        }
        return noOfN;
    }

    public void ResetCells () {
        for (int  row = 0; row < 14; row++) {
            for (int col = 0; col < 24; col++) {
                cell = cells[row].gameObjects[col].GetComponent<Cell> ();
                cell.ResetCell (cells[row].gameObjects[col].GetComponent<Button> ());
            }
        }
    }
}
