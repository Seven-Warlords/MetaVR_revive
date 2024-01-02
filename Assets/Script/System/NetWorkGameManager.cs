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
        }
        else if(stream.IsReading)
        {
            playercount = (int)stream.ReceiveNext();
        }
    }
    void Start()
    {
        
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