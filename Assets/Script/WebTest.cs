using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using Unity.VisualScripting;
using TMPro;


public class WebTest : MonoBehaviour
{
    List<List<object>> dataTable = new List<List<object>>(); // object -> Column
    string urlLink = "http://35.247.21.201:3000/api/v1/green/datatable";

    void Start()
    {
        StartCoroutine(GetRequest(urlLink));
    }

    IEnumerator GetRequest(string uri) 
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) //using statement
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log("HTTP Request Success");
                    JsonParsing(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    private void JsonParsing(string jsonText)
    {
        Dictionary<string, object> responJson = Json.Deserialize(jsonText) as Dictionary<string, object>;
        List<object> responData = responJson["dataList"] as List<object>; // object -> Table
        int tableCount = int.Parse(responJson["results"].ToString());

        for (int i = 0; i < tableCount; i++)
        {
            dataTable.Add(responData[i] as List<object>); // Table -> ColumnList
        }

        // Sample
        {
            string buf = (string)getData(1, "Text", 2);
            //text.text = buf.Replace("\\n", "\n");
        }
    }

    public object getData(int table, string field, int column)
    {
        return (dataTable[table][column] as Dictionary<string, object>)[field];
    }
}