using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class testtemp : MonoBehaviourPunCallbacks {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("�����̽��ٴ����� ");
            GetComponent<PhotonView>().RPC("testrun", RpcTarget.All, null);
            PhotonNetwork.Disconnect();



        }
    }
    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("������ ������ ������ϴ�.");
    
    }

    [PunRPC]

    void testrun() {
        Debug.Log("������ Ƣ���� ���!");
    }

}