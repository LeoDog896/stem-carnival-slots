using System.Collections;
using UnityEngine.Networking;
using System.Timers;
using UnityEngine;
using System.Threading;
using System;
using Unity.VisualScripting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class BioTrack
{
    private static bool active = false;
    private static string player = "";
    private static string urlPart = "";
    private static int gameId = 0;
    public static string gameUrl { get; set;  }

    private static bool paused = false;
    public static int activeSessionId { get; set; }
    private static int maxPlayerCount = 0;

    private static JoinRequestList joinRequests;
    private static List<Action<List<JoinRequestUser>>> joinFunctionList = new List<Action<List<JoinRequestUser>>>();
    private static List<Action<bool>> endFunctionList = new List<Action<bool>>();
    public static BioTrackAttachable Attached { get; set; }
    public static void Init(string url, int gameId, int maxPlayers = 0)
    {
        active = false;
        maxPlayerCount = maxPlayers;
        player = "";
        urlPart = url;
        BioTrack.gameId = gameId;
        gameUrl = urlPart + "/game/" + gameId;


    }

    public static void OnStart(Action<List<JoinRequestUser>> function)
    {
        joinFunctionList.Add(function);
    }
    public static void OnFinish(Action<bool> function)
    {
        endFunctionList.Add(function);
    }

    public static void StartGame()
    {
        paused = true;
        string ids = "?";
        List<JoinRequestUser> reqUserList = new();
        joinRequests.joinRequests.ForEach((item) =>
        {
            ids += "id=" + item.id.ToString() + "&";
            reqUserList.Add(item.user);
        });

        Attached.acknowledger(ids);

        //call the start functions
        joinFunctionList.ForEach((item) =>
        {
            
            item(reqUserList);
        });

    }

    public static void FinishGame(int score, bool doContinue, object data)
    {
        endFunctionList.ForEach((item) =>
        {
            item(doContinue);
        });
        Attached.Finisher(score, doContinue, data);
    }

    

    public static IEnumerator FetchData()
    {

        if (paused)
        {
            yield break;
        }
        using (UnityWebRequest webRequest = UnityWebRequest.Get(gameUrl + "/queue/all"))
        {
            webRequest.certificateHandler = new CertificateShenanigans();
            yield return webRequest.SendWebRequest();
            string rawApiResponse = webRequest.downloadHandler.text;
            JoinRequestList data = JsonConvert.DeserializeObject<JoinRequestList>(rawApiResponse);
            joinRequests = data;
            if(data.joinRequests.Count >= maxPlayerCount)
            {
                StartGame();
            }

        }
    }

    public static IEnumerator AcknowledgeJoin(string ids)
    {
        string url = gameUrl + "/ack" + ids;
        url = url.Substring(0, url.Length - 1);
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "{}"))
        {

            webRequest.certificateHandler = new CertificateShenanigans();
            yield return webRequest.SendWebRequest();
            string rawApiResponse = webRequest.downloadHandler.text;
            Debug.Log(rawApiResponse);
            Session session = JsonConvert.DeserializeObject<Session>(rawApiResponse);
            BioTrack.activeSessionId = session.id;

        }
    }

    public static IEnumerator FinishRequest(int score, bool doContinue, object data)
    {

        if(!doContinue)
        {
            paused = false;
        }

        string url = gameUrl + "/finish?score=" + score + "&finished=" + doContinue.ToString() + "&data=" + JsonConvert.SerializeObject(data);
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "{}"))
        {
            webRequest.certificateHandler = new CertificateShenanigans();
            yield return webRequest.SendWebRequest();
        }
    }




}

    public class CertificateShenanigans : CertificateHandler
    
        {
        //https://stackoverflow.com/questions/55112733/unitywebrequest-change-to-https
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }







