using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public enum Status
    {
        None,
        Connect_TimeOut,
        Connected_TimeOut,
        CloseRoom
    }
    public Status errorStatus= Status.None;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerChase == null)
            return;
        switch (errorStatus)
        {
            case Status.Connect_TimeOut:
                errorStatus = Status.None;
                break;
            case Status.Connected_TimeOut:
                errorStatus = Status.None;
                break;
            case Status.CloseRoom:
                errorStatus = Status.None;
                break;
            default:
                break;
        }
    }
    private void WriteString()
    {
        if (GameManager.instance.playerChase == null)
            return;
        //GameManager.instance.playerChase.StatusUI
    }
}
