using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class trackManager : MonoBehaviour
{
    // Sound stuff
    public GameObject audioManager;
    public AudioClip spinSound;

    //public stuff I dont care about
    public TMP_Text textMesh;
    public GameObject[] arrows;
    
    public int rows = 1;
    

    //spin animation settings
    public int spinCount = 100;
    public int spinFrames = 250;
    public double frameStep = 1;
    private static bool spinning = false;
    private bool won = false;
    private double currentFrame = 0;
    private float spins = 0;
    private float spinAmt = 0.5f;
    private int size = 2;


    int lastPercent = 0;
   


    private int arrowWinIndicatorOn = 0;
    private SlotDisplayHelper[,] rowManager;
    private int arrowHelper = 0;

    //random object related stuff
    public GameObject[] spinnerItems;
    public double[] chances = new double[1];
    private double[] ranges;
    private double totalChance;

    //valeus stuff
    public int[] values;

    

    void Start()
    {
        SlotObjectHelper.setup(spinnerItems, chances, values);


        textMesh.text = moneyManager.getBalance().ToString();
        won = false;
        moneyFall.doFall = false;

        rowManager = new SlotDisplayHelper[rows, spinCount + 50];
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, 0);
        //initalisze somestarting stuff
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < spinCount + 50; i++)
            {

                //pick a random game object
                SlotDisplayHelper temp = SlotObjectHelper.randomObject();
                temp.cloned = Instantiate(temp.gameObject, this.transform, worldPositionStays: false);
                temp.cloned.transform.localScale = new Vector3(size, size, size);
                temp.cloned.transform.position = new Vector2((j * (size * 1.5f) - (2 * (rows - 1))) + gameObject.transform.position.x, i * size - spinCount * size);
                rowManager[j, i] = temp;
            }
        }
    }

   
    private void FixedUpdate()
    {

        
        if (Input.GetAxis("Submit") > 0 && !spinning)
        {
            moneyFall.doFall = false;
            currentFrame = 0;
            won = false;

            if (!moneyManager.chargeAccount())
            {
                textMesh.text = moneyManager.getBalance().ToString();
                return;

            }
            textMesh.text = moneyManager.getBalance().ToString();
            //destory previous spin.
            for (int i = 0; i < rowManager.GetLength(0); i++)
            {
                for(int j = 0; j < rowManager.GetLength(1); j++)
                {
                    Destroy(rowManager[i, j].cloned);
                }
            }

            rowManager = new SlotDisplayHelper[rows, spinCount + 50];

            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 0);

            //initalisze somestarting stuff
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < spinCount + 50; i++)
                {

                    //pick a random game object
                    SlotDisplayHelper temp = SlotObjectHelper.randomObject();
                    temp.cloned = Instantiate(temp.gameObject, this.transform, worldPositionStays: false);
                    temp.cloned.transform.localScale = new Vector3(size, size, size);
                    temp.cloned.transform.position = new Vector2((j * (size * 1.5f) - (2 * (rows - 1))) + gameObject.transform.position.x, i * size - spinCount * size);
                    rowManager[j, i] = temp;
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

            int totalWinVlaue = 0;

            for (int j = 0; j < possibleWins.GetLength(0); j++)
            {
                bool winning = true;
                string lastSymbol = "none";
                int tempValue = 0;
                for (int y = 0; y < 3; y++)
                {
                    string thisSymbol = rowManager[y, 50 + spinCount - 3 + possibleWins[j, y]].cloned.name;
                    tempValue = rowManager[y, 50 + spinCount - 3 + possibleWins[j, y]].value;
                    if (lastSymbol == "none")
                    {
                        lastSymbol = thisSymbol;
                    }
                    if (lastSymbol != thisSymbol)
                    {
                        winning = false;
                    }
                }
                if (winning)
                {
                    winRows++;
                    totalWinVlaue += tempValue;
                }
            }
          
            if(winRows > 0)
            {
                won = true;
                moneyManager.givePrize(totalWinVlaue);
                
            }


            spinning = true;
            spins = 0;
            
        }



        if (spinning)
        {
            //the spin distance calculations

            if(!(currentFrame > spinFrames))
            {
                double maxValue = 21.5f * System.Math.Log(spinFrames + 1, System.Math.E);

                //calculate the current value based on the frame
                double currentValue = 21.5f * System.Math.Log(currentFrame + 1, System.Math.E);

                //calculate as a percentage of the max
                double percentOfMax = currentValue / maxValue;

                if (System.Math.Floor(percentOfMax * 75) != lastPercent)
                {
                    lastPercent = (int) System.Math.Floor(percentOfMax * 75);
                    //sounds
                    AudioSource sound = audioManager.AddComponent<AudioSource>(); ;
                    sound.clip = spinSound;
                    sound.Play();
                }

                float location = 0 - (float)(percentOfMax * size * (spinCount -2));

                gameObject.transform.position = new Vector2(gameObject.transform.position.x, location);
                currentFrame++;
                
            } else
            {
                spinning = false;
                textMesh.text = moneyManager.getBalance().ToString();
            }
            // calculate the maxmimum the result at the maximum frame
           

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



        } else if (won)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                if (arrowWinIndicatorOn > 5)
                {
                    Renderer mesh = arrows[i].GetComponent<Renderer>();
                    Color tempcolor = mesh.material.color;
                    tempcolor.a = 1f;
                    mesh.material.color = tempcolor;
                }
                else
                {
                    Renderer mesh = arrows[i].GetComponent<Renderer>();
                    Color tempcolor = mesh.material.color;
                    tempcolor.a = 0.25f;
                    mesh.material.color = tempcolor;
                }
             
            }
            moneyFall.doFall = true;
            

            
            arrowWinIndicatorOn++;
            if (arrowWinIndicatorOn > 10)
            {
                arrowWinIndicatorOn = 0;
            }


        }
        else
        {
            for (int i = 0; i < arrows.Length; i++) {
                Renderer mesh = arrows[i].GetComponent<Renderer>();
                Color tempcolor = mesh.material.color;
                tempcolor.a = 0.25f;
                mesh.material.color = tempcolor;
               
            }
        }


    }

}
