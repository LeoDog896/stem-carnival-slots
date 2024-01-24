using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class trackManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_TextEventHandler myText;
    private static bool spinning = false;
    public int spinCount = 100;
    private float spins = 0;
    public int rows = 1;

    public GameObject[] spinnerItems = new GameObject[1];

    public GameObject[] initializedItems;

    public GameObject[] lastRow;

    void Start()
    {
        lastRow = new GameObject[rows];
    }

   
    private void FixedUpdate()
    {

        if (!moneyManager.chargeAccount())
        {
            return;
        }

        if (Input.GetAxis("Submit") > 0 && !spinning)
        {
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
                    cloned.transform.localScale = new Vector3(3, 3, 3);
                    cloned.transform.position = new Vector2(j * 4 - (2 * (rows - 1)), i * 3);
                    if(i == spinCount)
                    {
                        lastRow[j] = cloned;
                    }
                    initializedItems[(j * (spinCount + 50)) + i] = cloned;


                }
            }

            bool winning = true;
            string lastString = lastRow[0].name;
            for(int i = 0; i < lastRow.Length; i++)
            {
                if(lastRow[i].name != lastString)
                {
                    winning = false;
                }
            }

            Debug.Log(winning);
            //generate the wheel!
          

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

            }



        }


    }

}
