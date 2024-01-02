using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObject : MonoBehaviourPun, IPunObservable
{
    private void Awake()
    {
        // PhotonTransformView 컴포넌트의 동기화 옵션 설정
        PhotonTransformView transformView = GetComponent<PhotonTransformView>();
        transformView.m_SynchronizePosition = true; // 위치 동기화 여부
        transformView.m_SynchronizePosition = true; // 회전 동기화 여부
        transformView.m_SynchronizePosition = true;    // 크기 동기화 여부
    }

    // IPunObservable 인터페이스 구현
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 송신할 때의 동작
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // 데이터를 수신할 때의 동작
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            transform.localScale = (Vector3)stream.ReceiveNext();
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
}
