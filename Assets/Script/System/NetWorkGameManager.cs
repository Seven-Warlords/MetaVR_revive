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
    void Update()
    {
        
    }

    void Playersort()
    {

    }
}
