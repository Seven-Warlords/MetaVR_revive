using UnityEngine;
using Photon.Pun;

public class MyNetworkedScript : MonoBehaviourPun {
    // RPC�� ȣ��� �Լ�
    public int para;
    int[] array = { 0, 0, 0, 0 };
    int mynumber;
    string tempst;
    public int playercount = 0;
    void temp() {
        // �ʱ� �迭


        // �迭 ��ȸ
        for (int i = 0; i < array.Length; i++) {
            // ù ��° 0�� ã���� �� 1 �Ҵ��ϰ� �ݺ��� ����
            if (array[i] == 0) {
                array[i] = 1;
                mynumber = i;
                photonView.RPC("mynumbertemp", RpcTarget.All, mynumber, tempst);
                break;
            }
        }

        // ��� ��� (�ɼ�)
        Debug.LogFormat("�迭: {0}, {1}, {2}, {3}", array[0], array[1], array[2], array[3]);
    }
    //�����.RPC ���� ����� �۵� 
    [PunRPC]
    //�Լ��� �ڵ� �۵�, �Ķ���� �޾Ƽ� ���� �Լ� ����
    //�Լ��ȿ� �ƹ��ų� �־ �� 
    void MyRPCFunction(int parameter) {
        // �������� ȣ��Ǿ�� �ϴ� �ڵ�
        para = parameter;
        Debug.Log("RPC ȣ���. ���޵� �Ű�����: " + parameter);
        playercount++;
        temp();
    }

}