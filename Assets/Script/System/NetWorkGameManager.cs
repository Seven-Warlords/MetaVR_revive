using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetWorkGameManager : MonoBehaviourPun, IPunObservable
{

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
        gameObject.GetComponent<PhotonView>().RPC("NPlayerJoin", RpcTarget.All, null);
    }

    public void PlayerOut()
    {
        gameObject.GetComponent<PhotonView>().RPC("NPlayerOut", RpcTarget.All, null);
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
}
