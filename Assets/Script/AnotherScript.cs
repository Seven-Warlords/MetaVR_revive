using UnityEngine;
using Photon.Pun;

public class AnotherScript : MonoBehaviourPun {
    public int parararara= 42; 
    void Update() {
        // ���� ���, �����̽��ٸ� ������ RPC ȣ��
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "MyRPCFunction"�̶�� RPC�� ȣ���ϰ�, 42�� �Ű������� ����
            photonView.RPC("MyRPCFunction", RpcTarget.All, parararara);
        }
    }
}