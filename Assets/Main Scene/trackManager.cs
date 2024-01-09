using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackManager : MonoBehaviour
{   
    // Start is called before the first frame update

    private static bool spinning = false;
    public int spinCount = 100;
    private int spins = 0;
    public int rows = 1;

    public GameObject[] spinnerItems = new GameObject[1];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Submit") > 0 && !spinning)
        {
            //start the spin
            spinning = true;
            spins = 0;

            //generate the wheel!
            for(int i = 0; i < spinCount + 25; i++)
            {
                //pick a random game object
                GameObject spinnerObject = spinnerItems[Random.Range(0, spinnerItems.Length)];
                Debug.Log(spinnerObject);
                GameObject cloned = Instantiate(gameObject);
            }
        }

        if(spinning)
        {   
            if(spins >= spinCount)
            {
                spinning = false;
            }
            spins++;
        }
    }
}
