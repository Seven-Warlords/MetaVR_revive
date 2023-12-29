using UnityEngine;
using Photon.Pun;

public class SyncVariables : MonoBehaviourPun, IPunObservable {
    // 동기화하려는 변수
    public int syncedValue = 0;

	public void OnCollisionEnter(Collision collision) {
        syncedValue++;

    }
	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // 내 클라이언트(송신 클라이언트)
            stream.SendNext(syncedValue);
        } else {
            // 다른 클라이언트(수신 클라이언트)
            syncedValue = (int)stream.ReceiveNext();
        }
    }
}