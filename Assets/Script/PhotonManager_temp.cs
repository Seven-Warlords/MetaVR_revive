using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
using TMPro;
using OVR;
using Photon.Pun.Demo.PunBasics;

public class PhotonManager_temp : MonoBehaviourPunCallbacks {

    public GameObject SpawnLocation;
    private Transform startpoint;
    private int playerNumber;
    StatusManager SM;
    
    private void Awake() {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

        // startpoint = SpawnLocation.GetComponent<Transform>();
#if On
        Debug.Log(PhotonNetwork.SendRate);
#endif
    }
    private void Start()
    {
        SM = GameManager.instance.statusManager;   
    }

    public override void OnConnectedToMaster() {
#if On
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
#endif
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
#if On
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
#endif

        //버튼으로 시작할거임
        //PhotonNetwork.JoinRandomRoom();
    }
   
    public override void OnJoinRandomFailed(short returnCode, string message) {
#if On
        Debug.Log($"JoinRoom Faild {returnCode}:{message}");
#endif
      // 만약 룸이 없다면 새로운 룸 생성
        if (PhotonNetwork.CountOfRooms == 0) {
      
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("My Room", ro);
        }
        else
        {
            SM.errorStatus = StatusManager.Status.CloseRoom;
        }

    }
    public override void OnDisconnected(DisconnectCause cause) {
        SM.errorStatus = StatusManager.Status.Connected_TimeOut;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        SM.errorStatus = StatusManager.Status.Connected_TimeOut;


    }

	public override void OnCreatedRoom() {
#if On
    Debug.Log("Created Room");
    Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
#endif
    }

    public override void OnJoinedRoom() {
#if On
    Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
    Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
#endif
        GameManager.instance.lobby.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameManager.instance.lobby.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        PhotonNetwork.NickName = "Player : " + a.ToString();
        GameManager.instance.netWorkGameManager.PlayerJoin();
       
        foreach (var player in PhotonNetwork.CurrentRoom.Players) {
            Debug.Log($"{player.Value.NickName}");
        }
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.instance.netWorkGameManager.Ready = true;
        }
        StartCoroutine(StartPlayer());

    }

    IEnumerator StartPlayer()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.player.enabled = true;
    }
    //one Room
    public void ReadyedPlayer()
    {
        Debug.Log("IN Room");
        PhotonNetwork.JoinRandomRoom();
    }
    //Muti Room
    public void ReadyedPlayer(string room)
    {
        PhotonNetwork.JoinRoom(room);
    }
    public void InGameReady()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //게임 시작-특이사항 준비안한 플레이어는 방에서 내보낸다.
            GameManager.instance.netWorkGameManager.GameStart();
        }
        else
        {
            bool ready = GameManager.instance.netWorkGameManager.Ready;
            if (!ready) { 
                //개인의 준비를 확인하는 변수를 true
                GameManager.instance.netWorkGameManager.Ready=true;
                Button ReadyBtn = GameManager.instance.lobby.UI.transform.
                    GetChild(1).GetComponent<PT_Ready>().ReadyBtn;
                TMP_Text text = ReadyBtn.transform.GetChild(0).GetComponent<TMP_Text>();
                text.text = "Cancel";
                GameManager.instance.netWorkGameManager.GameReady(GameManager.instance.player.myNumber);
            }
            else
            {
                //개인의 준비를 확인하는 변수를 true
                GameManager.instance.netWorkGameManager.Ready = false;
                Button ReadyBtn = GameManager.instance.lobby.UI.transform.
                    GetChild(1).GetComponent<PT_Ready>().ReadyBtn;
                TMP_Text text = ReadyBtn.transform.GetChild(0).GetComponent<TMP_Text>();
                text.text = "Ready";
                GameManager.instance.netWorkGameManager.GameReady(GameManager.instance.player.myNumber);
            }
        }
    }
}

