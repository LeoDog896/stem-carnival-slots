using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BioTrackAttachable : MonoBehaviour
{
    //you need one game object with this script per scene!

    private static string bioTrackUrl = "https://localhost:5000"; // no trailing slash

    public int bioTrackGameId = 1;
    public int maxPlayerCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        BioTrack.Init(bioTrackUrl, bioTrackGameId, maxPlayerCount);
        InvokeRepeating("FetchJoinQueue", 0f, 0.25f);
        BioTrack.Attached = this;
    }
    // Update is called once per frame

    void FetchJoinQueue()
    {
        StartCoroutine(BioTrack.FetchData());
    }

    public void acknowledger(string ids)
    {
        StartCoroutine(BioTrack.AcknowledgeJoin(ids));
    }

    public void Finisher(int creditAmount, bool cont, object data)
    {
        StartCoroutine(BioTrack.FinishRequest(creditAmount, cont, data));
    }


}
