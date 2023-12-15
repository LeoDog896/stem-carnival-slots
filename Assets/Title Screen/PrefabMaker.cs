using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PrefabMaker : MonoBehaviour
{

    public int count = 0;
    public GameObject cloned;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < count; i++)
        {
            GameObject obj = Instantiate(cloned);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
