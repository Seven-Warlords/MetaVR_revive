using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObject : MonoBehaviourPun
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            GetComponent<PhotonView>().RequestOwnership();
        }
    }
    public void Destory()
    {
        GetComponent<PhotonView>().RPC("NetworkDestory", RpcTarget.All, null);
    }

    [PunRPC]

    void NetworkDestory()
    {
        gameObject.SetActive(false);
    }

    void PositionSync()
    {
       
    }
}
