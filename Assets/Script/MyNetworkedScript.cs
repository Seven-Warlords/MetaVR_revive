using UnityEngine;
using Photon.Pun;

public class MyNetworkedScript : MonoBehaviourPun {
    // RPC로 호출될 함수
    public int para;
    [PunRPC]
    void MyRPCFunction(int parameter) {
        // 원격으로 호출되어야 하는 코드
        para = parameter;
        Debug.Log("RPC 호출됨. 전달된 매개변수: " + parameter);
    }
}