using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pressToStart : MonoBehaviour
{

    private bool showingTitle = true;

    public string titleSceneName;
    public string gameSceneName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Submit") > 0 && showingTitle)
        {

            SceneManager.LoadScene(gameSceneName);

        }
    }
}
