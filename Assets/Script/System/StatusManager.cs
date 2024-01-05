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
                WriteString("Photon ���ῡ �����ϼ̽��ϴ�.\n ������ ��õ��մϴ�.");
                errorStatus = Status.None;
                break;
            case Status.Connected_TimeOut:
                WriteString("������ ������ϴ�.");
                errorStatus = Status.None;
                break;
            case Status.CloseRoom:
                WriteString("���� �÷��� ������ �� �� �����ϴ�.");
                errorStatus = Status.None;
                break;
            default:
                break;
        }
    }
    private void WriteString(string str)
    {
        if (GameManager.instance.playerChase == null)
            return;
        GameManager.instance.playerChase.StatusUI.btn.gameObject.SetActive(true);
        GameManager.instance.playerChase.StatusUI.tMP_Text.gameObject.SetActive(true);
        StringDelay delay;
        if (GameManager.instance.playerChase.StatusUI.tMP_Text.gameObject.TryGetComponent<StringDelay>(out delay))
        {
            delay.str = str;
            delay.startTyping = true;
        }

        return;
    }
}
