using UnityEngine;
using Photon.Pun;

public class AnotherScript : MonoBehaviourPun {
    public int parararara= 42;
    private PhotonView  pv;
    
    int mynumber;
    void Update() {
        // 예를 들어, 스페이스바를 누르면 RPC 호출
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "MyRPCFunction"이라는 RPC를 호출하고, 42를 매개변수로 전달
            //(함수명, 타겟 , 파라미터) // 함수명으로 이동해서 계속 
            photonView.RPC("MyRPCFunction", RpcTarget.All, parararara);
        }
    }
    //포톤뷰.RPC 에서 실행시 작동 
    [PunRPC]
    void mynumbertemp(int parameter , string tempst) {
        // 원격으로 호출되어야 하는 코드
        if (tempst == pv.name) {
            mynumber = parameter;
        }
    }
}