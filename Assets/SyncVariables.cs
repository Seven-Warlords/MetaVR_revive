using UnityEngine;
using Photon.Pun;

public class SyncVariables : MonoBehaviourPun, IPunObservable {
    // ����ȭ�Ϸ��� ����
    public int syncedValue = 0;

	public void OnCollisionEnter(Collision collision) {
        syncedValue++;

    }
	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // �� Ŭ���̾�Ʈ(�۽� Ŭ���̾�Ʈ)
            stream.SendNext(syncedValue);
        } else {
            // �ٸ� Ŭ���̾�Ʈ(���� Ŭ���̾�Ʈ)
            syncedValue = (int)stream.ReceiveNext();
        }
    }
}