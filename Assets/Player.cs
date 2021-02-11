using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTurn;
    public int peiceValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //No Need to update if not 
        if (!isTurn)
        {
            return;
        }

        Board boardObj = GameObject.Find("Board").GetComponent(typeof(Board)) as Board;
        int[,] board = boardObj.board;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Material peiceMat = peiceValue == 1 ? boardObj.PlayerOneMat : boardObj.PlayerTwoMat;

        //Find Hover Object
        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject peice = hit.transform.parent.transform.gameObject;
            Peice p = peice.GetComponent(typeof(Peice)) as Peice;
            int column = (p.column < 0) ? 0 : ((p.column > 6) ? 6 : p.column);

            if (column >= 0 || column <= 6)
            {
                //Reset Hover Object
                for (int c = 0; c < boardObj.columns; c++)
                {
                    if (board[0, c] != 0)
                    {
                        //Debug.Log("Continuing");
                        continue;
                    }
                    GameObject hObj = GameObject.Find("Peice " + c);
                    Peice hPeice = hObj.GetComponent(typeof(Peice)) as Peice;
                    hPeice.mat = boardObj.backMat;
                }
            }

            if (board[0, column] == 0)
            {
                GameObject hoverObject = GameObject.Find("Peice " + column);
                Peice hoverPeice = hoverObject.GetComponent(typeof(Peice)) as Peice;
                hoverPeice.mat = peiceMat;
            }

            //If Left Click
            if (Input.GetMouseButtonDown(0))
            {
                //Handled in Board.cs
                boardObj.PlacePeice(column, peiceValue);



                boardObj.UpdateTurns(this.name);

            }

        }





    }
}
