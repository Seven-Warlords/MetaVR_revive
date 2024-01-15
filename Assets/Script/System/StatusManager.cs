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
        CloseRoom,
        left

    }
    public Status errorStatus = Status.None;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerChase == null)
            return;
        switch (errorStatus)
        {
            case Status.Connect_TimeOut:
                StartCoroutine(WriteString("Photon ���ῡ �����ϼ̽��ϴ�.\n ������ ��õ��մϴ�."));
                errorStatus = Status.None;
                break;
            case Status.Connected_TimeOut:
                StartCoroutine(WriteString("������ ������ϴ�."));
                errorStatus = Status.None;
                break;
            case Status.CloseRoom:
                StartCoroutine(WriteString("���� �÷��� ������ �� �� �����ϴ�."));
                errorStatus = Status.None;
                break;
            default:
                break;
        }
    }
    IEnumerator WriteString(string str)
    {
        if (GameManager.instance.playerChase == null)
            yield return null;
        GameManager.instance.playerChase.StatusUI.btn.gameObject.SetActive(true);
        GameManager.instance.playerChase.StatusUI.tMP_Text.gameObject.SetActive(true);
        StringDelay delay;
        if (GameManager.instance.playerChase.StatusUI.tMP_Text.gameObject.TryGetComponent<StringDelay>(out delay))
        {
            delay.str = str;
            delay.startTyping = true;
        }
        yield return new WaitForSeconds(3);
        GameManager.instance.playerChase.StatusUI.btn.gameObject.SetActive(false);
        GameManager.instance.playerChase.StatusUI.tMP_Text.gameObject.SetActive(false);
    }
}
