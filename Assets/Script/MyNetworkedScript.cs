using UnityEngine;
using Photon.Pun;

public class MyNetworkedScript : MonoBehaviourPun {
    // RPC�� ȣ��� �Լ�
    public int para;
    [PunRPC]
    void MyRPCFunction(int parameter) {
        // �������� ȣ��Ǿ�� �ϴ� �ڵ�
        para = parameter;
        Debug.Log("RPC ȣ���. ���޵� �Ű�����: " + parameter);
    }
}