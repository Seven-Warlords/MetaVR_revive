using UnityEngine;
using Photon.Pun;

public class AnotherScript : MonoBehaviourPun {
    public int parararara= 42; 
    void Update() {
        // 예를 들어, 스페이스바를 누르면 RPC 호출
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "MyRPCFunction"이라는 RPC를 호출하고, 42를 매개변수로 전달
            photonView.RPC("MyRPCFunction", RpcTarget.All, parararara);
        }
    }
}