using UnityEngine;
using Photon.Pun;

public class AnotherScript : MonoBehaviourPun {
    public int parararara= 42;
    private PhotonView  pv;
    
    int mynumber;
    void Update() {
        // ���� ���, �����̽��ٸ� ������ RPC ȣ��
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "MyRPCFunction"�̶�� RPC�� ȣ���ϰ�, 42�� �Ű������� ����
            //(�Լ���, Ÿ�� , �Ķ����) // �Լ������� �̵��ؼ� ��� 
            photonView.RPC("MyRPCFunction", RpcTarget.All, parararara);
        }
    }
    //�����.RPC ���� ����� �۵� 
    [PunRPC]
    void mynumbertemp(int parameter , string tempst) {
        // �������� ȣ��Ǿ�� �ϴ� �ڵ�
        if (tempst == pv.name) {
            mynumber = parameter;
        }
    }
}