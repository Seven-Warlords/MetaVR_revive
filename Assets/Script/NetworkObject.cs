using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObject : MonoBehaviourPun, IPunObservable
{
    private void Awake()
    {
        // PhotonTransformView ������Ʈ�� ����ȭ �ɼ� ����
        PhotonTransformView transformView = GetComponent<PhotonTransformView>();
        transformView.m_SynchronizePosition = true; // ��ġ ����ȭ ����
        transformView.m_SynchronizePosition = true; // ȸ�� ����ȭ ����
        transformView.m_SynchronizePosition = true;    // ũ�� ����ȭ ����
    }

    // IPunObservable �������̽� ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �����͸� �۽��� ���� ����
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // �����͸� ������ ���� ����
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
