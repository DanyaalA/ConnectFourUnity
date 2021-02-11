using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public Material FrontMat;
    public Material backMat;
    public Material PlayerOneMat;
    public Material PlayerTwoMat;
    public GameObject PeicePrefab;
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public bool isPlayerOneTurn;
    public int rows = 6;
    public int columns = 7;
    public int[,] board;
    
    void Start()
    {
        board =  new int[rows, columns];

        Player p1 = PlayerOne.GetComponent(typeof(Player)) as Player;
        Player p2 = PlayerTwo.GetComponent(typeof(Player)) as Player;
        p1.isTurn = true;
        p2.isTurn = false;

        DrawBoard();
    }

    void DrawBoard()
    {
        float x = (float)-9, y = (float)4.5;
        int counter = 0;
        GameObject brd = GameObject.Find("Board");
        foreach (Transform child in brd.transform)
        {
            Destroy(child.gameObject);
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject newPeice = Instantiate(PeicePrefab, GameObject.Find("Board").transform);
                newPeice.name = "Peice " + counter;
                
                Peice p = newPeice.GetComponent(typeof(Peice)) as Peice;
                newPeice.transform.position = new Vector3(x, y, 0);

                p.row = r;
                p.column = c;

                Material mat;
                if (board[r, c] == 0) mat = backMat; else if (board[r, c] == 1) mat = PlayerOneMat; else mat = PlayerTwoMat;
                p.mat = mat;

                x++;
                counter++;
            }
            x = (float)-9;
            y--;
        }
    }
    

    public void UpdateTurns(string name)
    {
        

        Player player3 = GameObject.Find(name).GetComponent(typeof(Player)) as Player;
        player3.isTurn = false;

        string otherName = name == "PlayerOne" ? "PlayerTwo" : "PlayerOne";
        Player player2 = GameObject.Find(otherName).GetComponent(typeof(Player)) as Player;
        player2.isTurn = true;
    }

    public bool PlacePeice(int column, int peiceValue)
    {
      
        for (int i = 5; i >= 0; i--)
        {
            if (board[i, column] == 0)
            {
                board[i, column] = peiceValue;

                DrawBoard();

                return CheckWin(i, column, peiceValue);
            }
        }

        return false;
    }

    bool CheckWin(int rowPlaced, int columnsPlaced, int pValue)
    {
        int count = 0;
        
        //Horizontal Check
        for (int column = 0; column < columns; column++)
        {
            if (board[rowPlaced, column] == pValue) count++;
            else count = 0;

            if (count == 4) Debug.Log("YOU WON");
        }

        //Vertical Check
        for (int row = 0; row < rows; row++)
        {
            if (board[row, columnsPlaced] == pValue) count++;
            else count = 0;

            if (count == 4)
            {
                Debug.Log("YOU WON");
            }
        }


        //Diagnol Right Check
        if (rowPlaced - 3 >= 0 && columnsPlaced + 3 < 7)
        {
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (board[rowPlaced - i, columnsPlaced + i] == pValue) count++;
                else count = 0;

                if (count == 4) Debug.Log("YOU WON DIAGNOL");
            }
        }

        if (rowPlaced + 3 < 6 && columnsPlaced - 3 >= 0)
        {
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (board[rowPlaced + i, columnsPlaced - i] == pValue) count++;
                else count = 0;

                if (count == 4) Debug.Log("YOU WON DIAGONAL");
            }
        }



        //Diaganol Left
        if (rowPlaced + 3 < 6 && columnsPlaced + 3 < 7)
        {
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (board[rowPlaced + i, columnsPlaced + i] == pValue) count++;
                else count = 0;

                if (count == 4) Debug.Log("YOU WON DIAGNOL LEFT");
            }
        }

        if (rowPlaced - 3 >= 0 && columnsPlaced - 3 >= 0)
        {
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (board[rowPlaced - i, columnsPlaced - i] == pValue) count++;
                else count = 0;

                if (count == 4) Debug.Log("YOU WON DIAGNOL LEFT V2");
            }
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
