using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private bool ready=false;
    public bool Ready { get { return ready; }set { ready = value; } }
    public GameObject[] pings;
    public int currentplayerNum;
    public int playercount;
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
	}
	public void playercountfun() {
        playercount = PhotonNetwork.PlayerList.Length;
	}
    public string getleftplayer() {

        oldplayers = PhotonNetwork.PlayerList;
        
        return oldplayers[0].NickName;
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
