using UnityEngine;
using Photon.Pun;

public class MyNetworkedScript : MonoBehaviourPun {
    // RPC로 호출될 함수
    public int para;
    int[] array = { 0, 0, 0, 0 };
    int mynumber;
    string tempst;
    public int playercount = 0;
    void temp() {
        // 초기 배열


        // 배열 순회
        for (int i = 0; i < array.Length; i++) {
            // 첫 번째 0을 찾았을 때 1 할당하고 반복문 종료
            if (array[i] == 0) {
                array[i] = 1;
                mynumber = i;
                photonView.RPC("mynumbertemp", RpcTarget.All, mynumber, tempst);
                break;
            }
        }

        // 결과 출력 (옵션)
        Debug.LogFormat("배열: {0}, {1}, {2}, {3}", array[0], array[1], array[2], array[3]);
    }
    //포톤뷰.RPC 에서 실행시 작동 
    [PunRPC]
    //함수명 코드 작동, 파라미터 받아서 내부 함수 실행
    //함수안에 아무거나 넣어도 됨 
    void MyRPCFunction(int parameter) {
        // 원격으로 호출되어야 하는 코드
        para = parameter;
        Debug.Log("RPC 호출됨. 전달된 매개변수: " + parameter);
        playercount++;
        temp();
    }

}