using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{
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

    [PunRPC]
    
    public void NPlayerJoin()
    {
        currentplayerNum++;
        playercount++;
    }

    public void NPlayerOut()
    {
        currentplayerNum--;
        playercount--;
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
