using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pressToStart : MonoBehaviour
{

    private bool showingTitle = true;

    public string titleSceneName;
    public string gameSceneName;

    public void Start()
    {
        BioTrack.OnStart((List<JoinRequestUser> users) =>
        {
            moneyManager.SetBalance(users[0].score);
            SceneManager.LoadScene(gameSceneName);
        });
    }

    public void showTitle()
    {
        SceneManager.LoadScene(titleSceneName);
        
    }
}
