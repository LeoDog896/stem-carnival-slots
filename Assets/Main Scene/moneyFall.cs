using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moneyFall : MonoBehaviour
{   

    public static bool doFall = true;
    int fallCount = 0;
    int itemCount = 100;
    int clonedItems = 0;

    bool hidden = true;
    GameObject[] items;
    public GameObject[] fallingObject;
    // Start is called before the first frame update
    void Start()
    {
        items = new GameObject[itemCount];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(doFall)
        {
            if(hidden)
            {
                for(int i = 0; i < itemCount; i++)
                {
                    Debug.Log("Item Cloned!");
                    items[i] = Instantiate(fallingObject[new System.Random().Next(0, fallingObject.Length)]);
                }
            }
            hidden = false;
        } else
        {
            if(!hidden)
            {
                for (int i = items.Length - 1; i > -1; i--)
                {
                    Destroy(items[i]);
                }
                hidden = true;
            }
            
        }
    }
}
