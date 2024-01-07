using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{
    private bool ready=false;
    public bool Ready { get { return ready; }set { ready = value; } }
    public int currentplayerNum;
    public int playercount;
    public int[] players;
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
        
    }
	public void Update() {
        playercountfun();
	}
	public void playercountfun() {
        playercount = PhotonNetwork.PlayerList.Length;
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
        GameManager.instance.lobby.UI.transform.
            GetChild(1).GetComponent<PT_Ready>().
                ReadyPly[currentplayerNum-1].color = Color.red;
    }

    public void NPlayerOut()
    {
        currentplayerNum--;
        playercount--;
    }
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
        }
    }
    public void ReadyPlayer(int num)
    {
        GameManager.instance.lobby.UI.transform.
            GetChild(1).GetComponent<PT_Ready>().
                ReadyPly[num - 1].color = Color.green;
    }
    public void CancelPlayer(int num)
    {
        GameManager.instance.lobby.UI.transform.
            GetChild(1).GetComponent<PT_Ready>().
                ReadyPly[num - 1].color = Color.red;
    }
}
