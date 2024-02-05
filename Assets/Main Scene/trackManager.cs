using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class trackManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text textMesh;
    private static bool spinning = false;
    public int spinCount = 100;
    private float spins = 0;
    private float spinAmt = 0.5f;
    public int rows = 1;
    private int size = 2;
    public GameObject[] spinnerItems = new GameObject[1];

    public GameObject[] arrows;
    private int arrowHelper = 0;

    private GameObject[,] rowManager;

    void Start()
    {

        rowManager = new GameObject[rows, spinCount + 50];

        //initalisze somestarting stuff
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < spinCount + 50; i++)
            {

                //pick a random game object
                GameObject spinnerObject = spinnerItems[Random.Range(0, spinnerItems.Length)];
                GameObject cloned = Instantiate(spinnerObject, this.transform, worldPositionStays: false);
                cloned.transform.localScale = new Vector3(size, size, size);
                cloned.transform.position = new Vector2((j * (size * 1.5f) - (2 * (rows - 1))) + gameObject.transform.position.x, i * size - spinCount * size);
                rowManager[j, i] = cloned;
            }
        }
    }

   
    private void FixedUpdate()
    {



        if (Input.GetAxis("Submit") > 0 && !spinning)
        {
            if (!moneyManager.chargeAccount())
            {
                textMesh.text = "Credits: " + moneyManager.getBalance();
                return;

            }
            textMesh.text = "Credits: " + moneyManager.getBalance();
            //start the spin
            for (int i = 0; i < rowManager.GetLength(0); i++)
            {
                for(int j = 0; j < rowManager.GetLength(1); j++)
                {
                    Destroy(rowManager[i, j]);
                }
            }

            rowManager = new GameObject[rows, spinCount + 50];

            //initalisze somestarting stuff
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < spinCount + 50; i++)
                {

                    //pick a random game object
                    GameObject spinnerObject = spinnerItems[Random.Range(0, spinnerItems.Length)];
                    GameObject cloned = Instantiate(spinnerObject, this.transform, worldPositionStays: false);
                    cloned.transform.localScale = new Vector3(size, size, size);
                    cloned.transform.position = new Vector2((j * (size * 1.5f) - (2 * (rows - 1))) + gameObject.transform.position.x, i * size - spinCount * size);
                    rowManager[j, i] = cloned;
                }
            }

            int winRows = 0;
            
            int[,] possibleWins =
            {
                {0,0,0},
                {0,1,2 },
                {1,1,1 },
                {2,2,2 },
                {2,1,0 }
            };


            for (int j = 0; j < possibleWins.GetLength(0); j++)
            {
                bool winning = true;
                string lastSymbol = "none";
                for (int y = 0; y < 3; y++)
                {
                    string thisSymbol = rowManager[y, 50 + spinCount - 3 + possibleWins[j, y]].name;
                    Debug.Log(thisSymbol);
                    if (lastSymbol == "none")
                    {
                        lastSymbol = thisSymbol;
                    }
                    if (lastSymbol != thisSymbol)
                    {
                        winning = false;
                    }
                }
                Debug.Log("---+---");
                if (winning)
                {
                    winRows++;
                }
            }
          
            if(winRows > 0)
            {
                Debug.Log("won in " + winRows + " rows!");
                moneyManager.givePrize(winRows);
                
            }


            spinning = true;
            spins = 0;
            
        }

        if (spinning)
        {

            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - spinAmt);
            spins += spinAmt/(size);

            if(arrowHelper > 29)
            {
                arrowHelper = -1;
            }
            arrowHelper++;
            for(int i = 0; i < arrows.Length; i++)
            {
                if (i == arrowHelper/10) {
                    Renderer mesh = arrows[i].GetComponent<Renderer>();
                    Color tempcolor = mesh.material.color;
                    tempcolor.a = 1f;
                    mesh.material.color = tempcolor;
                } else
                {
                    Renderer mesh = arrows[i].GetComponent<Renderer>();
                    Color tempcolor = mesh.material.color;
                    tempcolor.a = 0.25f;
                    mesh.material.color = tempcolor;
                }
            }


            if (spins >= spinCount - 2)
            {
                spinning = false;
                textMesh.text = "Credits: " + moneyManager.getBalance();

            }



        }


    }

}
