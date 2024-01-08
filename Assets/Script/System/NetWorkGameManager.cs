using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{
    public GameObject[] pings;
    public int currentplayerNum;
    public int playercount;
    public int previousPlayerCount;
    public int[] players;
    public Photon.Realtime.Player[] oldplayers;
    public Photon.Realtime.Player[] newplayers;
    // Start is called before the first frame update

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(playercount) ;
            stream.SendNext(currentplayerNum);
        }
        else
        {
            this.playercount = (int)stream.ReceiveNext();
            this.currentplayerNum = (int)stream.ReceiveNext();
        }
    }
    void Start()
    {
        oldplayers = new Photon.Realtime.Player[4];
        newplayers = new Photon.Realtime.Player[4];
    }
	public void Update() {
        playercountfun();
        // ���� �÷��̾� �迭�� �� �Ҵ�
        oldplayers = new Photon.Realtime.Player[PhotonNetwork.PlayerList.Length];
        Array.Copy(PhotonNetwork.PlayerList, oldplayers, PhotonNetwork.PlayerList.Length);

        if (previousPlayerCount > playercount) {
            // �÷��̾� ���� �������� �� ������ �ڵ� �ۼ�
            Debug.Log("�÷��̾� ���� �����߽��ϴ�!");
            string leftPlayerName = GetLeftPlayerName();
            Debug.Log("���� �÷��̾�: " + leftPlayerName);
        }
        // �÷��̾� ���� �������� �� ó��
        else if (previousPlayerCount < playercount) {
            // �÷��̾� ���� �������� �� ������ �ڵ� �ۼ�
            Debug.Log("�÷��̾� ���� �����߽��ϴ�!");
        }

        // ����� �÷��̾� ���� ���
        previousPlayerCount = playercount;
    }

    
	public void playercountfun() {
        playercount = PhotonNetwork.PlayerList.Length;
	}
  
    public string GetLeftPlayerName() {
        // ���� �÷��̾� �迭�� ���� �÷��̾� �迭�� ���Ͽ� ���� �÷��̾� ã��
        foreach (Photon.Realtime.Player oldPlayer in oldplayers) {
            bool playerStillInRoom = false;
            foreach (Photon.Realtime.Player currentPlayer in PhotonNetwork.PlayerList) {
                if (oldPlayer == currentPlayer) {
                    playerStillInRoom = true;
                    break;
                }
            }
            if (!playerStillInRoom) {
                // ���� �÷��̾��� �̸� ��ȯ
                return oldPlayer.NickName;
            }
        }

        // ���� �÷��̾ ������ �� ���ڿ� ��ȯ
        return "";
    }
    // Update is called once per frame
    public void PlayerJoin()
    {
        PhotonView.Get(this).RPC("NPlayerJoin", RpcTarget.All, null);
    }

    public void PlayerOut()
    {
        PhotonView.Get(this).RPC("NPlayerOut", RpcTarget.All, null);
    }

    [PunRPC]
    
    public void NPlayerJoin()
    {
        currentplayerNum++;
        playercount++;
    }

    [PunRPC]
    public void NPlayerOut()
    {
        currentplayerNum--;
        playercount--;
    }

    [PunRPC]
    public void Ping(int pingi, Vector3 position, Quaternion rotation) {
        switch (pingi) {
            case 0:
                Instantiate(pings[0], position, rotation);
                break;
            case 1:
                Instantiate(pings[1], position, rotation);
                break;
            case 2:
                Instantiate(pings[2], position, rotation);
                break;
        }
    }
}
