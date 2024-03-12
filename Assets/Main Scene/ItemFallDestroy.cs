using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFallDestroy : MonoBehaviour
{
    // Start is called before the first frame update
   

        // Start is called before the first frame update
        public static bool destory = false;

        void getReady(Rigidbody2D rigidBody)
        {
            rigidBody.SetRotation(Random.rotation);
            rigidBody.position = new Vector2(Random.Range(-10, 10), Random.Range(10, 14));
            rigidBody.velocity = new Vector2(0f, Random.Range(-5f, -6f));
            rigidBody.angularVelocity = Random.Range(-100f, 100f);
        }
        void Start()
        {
            Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
            getReady(rigidBody);
        }

        // Update is called once per frame
        void Update()
        {
            Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody.position.y < -10)
            {
                Destroy(gameObject);

            }
    
    }

}
