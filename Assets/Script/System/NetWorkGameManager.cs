using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private bool ready = false;
    public bool Ready { get { return ready; } set { ready = value; } }
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
        if (stream.IsWriting)
        {
            stream.SendNext(playercount);
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
    public void Update()
    {
        playercountfun();
        // 이전 플레이어 배열에 값 할당
        oldplayers = new Photon.Realtime.Player[PhotonNetwork.PlayerList.Length];
        Array.Copy(PhotonNetwork.PlayerList, oldplayers, PhotonNetwork.PlayerList.Length);

        if (previousPlayerCount > playercount)
        {
            // 플레이어 수가 감소했을 때 실행할 코드 작성
            Debug.Log("플레이어 수가 감소했습니다!");
            string leftPlayerName = GetLeftPlayerName();
            Debug.Log("떠난 플레이어: " + leftPlayerName);
        }
        // 플레이어 수가 증가했을 때 처리
        else if (previousPlayerCount < playercount)
        {
            // 플레이어 수가 증가했을 때 실행할 코드 작성
            Debug.Log("플레이어 수가 증가했습니다!");
        }

        // 변경된 플레이어 수를 기록
        previousPlayerCount = playercount;
    }


    public void playercountfun()
    {
        playercount = PhotonNetwork.PlayerList.Length;
    }

    public string GetLeftPlayerName()
    {
        // 이전 플레이어 배열과 현재 플레이어 배열을 비교하여 떠난 플레이어 찾기
        foreach (Photon.Realtime.Player oldPlayer in oldplayers)
        {
            bool playerStillInRoom = false;
            foreach (Photon.Realtime.Player currentPlayer in PhotonNetwork.PlayerList)
            {
                if (oldPlayer == currentPlayer)
                {
                    playerStillInRoom = true;
                    break;
                }
            }
            if (!playerStillInRoom)
            {
                // 떠난 플레이어의 이름 반환
                return oldPlayer.NickName;
            }
        }

        // 떠난 플레이어가 없으면 빈 문자열 반환
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

    public void GameStart()
    {
        PhotonView.Get(this).RPC("StartPlayer", RpcTarget.All, null);
    }
    public void GameReady(int playerNum)
    {
        if (ready)
        {
            PhotonView.Get(this).RPC("ReadyPlayer", RpcTarget.All, playerNum);
        }
        else
        {
            PhotonView.Get(this).RPC("CancelPlayer", RpcTarget.All, playerNum);
        }
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
    public void StartPlayer()
    {
        //개인의 준비를 확인
        if (!ready)
        {
            //마스터가 나가는경우 확인 필요
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            //준비되어있으면 시작
            QuizManager.Instance.state = QuizManager.State.ready;
            GameManager.instance.lobby.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
    [PunRPC]
    public void ReadyPlayer(int num)
    {
        GameManager.instance.lobby.UI.transform.
            GetChild(1).GetComponent<PT_Ready>().
                ReadyPly[num].color = Color.green;
    }
    [PunRPC]
    public void CancelPlayer(int num)
    {
        GameManager.instance.lobby.UI.transform.
            GetChild(1).GetComponent<PT_Ready>().
                ReadyPly[num].color = Color.red;
    }
    [PunRPC]
    public void Ping(int pingi, Vector3 position, Quaternion rotation)
    {
        switch (pingi)
        {
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
