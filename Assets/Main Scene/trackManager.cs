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
    public int rows = 1;
    private int size = 2;
    public GameObject[] spinnerItems = new GameObject[1];

    public GameObject[] initializedItems;

    public GameObject[] lastRow;

    void Start()
    {
        initializedItems = new GameObject[rows * (spinCount + 50)];
        lastRow = new GameObject[rows];
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
                initializedItems[(j * (spinCount + 50)) + i] = cloned;
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
            for (int i = initializedItems.Length - 1; i >= 0; i--)
            {
                Destroy(initializedItems[i]);
            }

            initializedItems = new GameObject[rows * (spinCount + 50)];

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < spinCount + 50; i++)
                {
                    
                    //pick a random game object
                    GameObject spinnerObject = spinnerItems[Random.Range(0, spinnerItems.Length)];
                    GameObject cloned = Instantiate(spinnerObject, this.transform, worldPositionStays: false);
                    cloned.transform.localScale = new Vector3(size, size, size);
                    cloned.transform.position = new Vector2((j * (size * 1.5f) - (2 * (rows - 1))) + gameObject.transform.position.x, i * size);
                    if(i == spinCount)
                    {
                        lastRow[j] = cloned;
                    }
                    initializedItems[(j * (spinCount + 50)) + i] = cloned;


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
                    int index = possibleWins[j, y] + (y * (spinCount + 50)) + spinCount - 1;
                    string thisSymbol = initializedItems[index].name;
                    if (lastSymbol == "none")
                    {
                        lastSymbol = thisSymbol;
                    }
                    if(lastSymbol != thisSymbol)
                    {
                        winning = false;
                    }
                }
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

            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.75f);
            spins += 0.25f;

            if (spins >= spinCount)
            {
                spinning = false;
                textMesh.text = "Credits: " + moneyManager.getBalance();

            }



        }


    }

}
