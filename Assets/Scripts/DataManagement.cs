using UnityEngine;
using System;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Parameters
{
    public string numTarget;
    public string stimulus;
    public string distanceToTarget;
    public string score;
    public string hit;
}
public class DataManagement : MonoBehaviour
{
    public DataManagement dbmgr;
    public List<string> userIDs;

    public List<KeyValuePair<string, List<Parameters>>> data = new List<KeyValuePair<string, List<Parameters>>>();

    DatabaseReference dbRef;

    public void Awake()
    {
        dbmgr = this;
        DontDestroyOnLoad(gameObject);
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public void RecieveData() {

        StartCoroutine(RecieveDataEnum());
        
    }

    IEnumerator RecieveDataEnum()
    {
        var serverData = dbRef.Child("users").GetValueAsync();
        yield return new WaitUntil(() => serverData.IsCompleted);

        Debug.Log("Data received successfully!");

        DataSnapshot snapshot = serverData.Result;

        foreach (DataSnapshot user in snapshot.Children)
        {
            userIDs.Add(user.Key);
            foreach (DataSnapshot Mode in user.Children)
            {
                string mode = Mode.Key;
                List<Parameters> paramList = new List<Parameters>();
                foreach (DataSnapshot target in Mode.Children)
                {
                    Parameters param = new Parameters();
                    param.numTarget = target.Key;
                    param.distanceToTarget = target.Child("distanceToTarget").Value.ToString();
                    param.score = target.Child("score").Value.ToString();
                    param.hit = target.Child("hit").Value.ToString();
                    paramList.Add(param);
                    param = null;
                }
                data.Add(new KeyValuePair<string, List<Parameters>>(mode, paramList));
            }

        }
    }
}
