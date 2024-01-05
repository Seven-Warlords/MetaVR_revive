using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;

public class PhotonManager_temp : MonoBehaviourPunCallbacks {

    public GameObject SpawnLocation;
    private Transform startpoint;
    private int playerNumber;
    public StatusManager SM;
    
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

        //��ư���� �����Ұ���
        //PhotonNetwork.JoinRandomRoom();
    }
   
    public override void OnJoinRandomFailed(short returnCode, string message) {
#if On
        Debug.Log($"JoinRoom Faild {returnCode}:{message}");
#endif
      // ���� ���� ���ٸ� ���ο� �� ����
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
        GameManager.instance.netWorkGameManager.PlayerJoin();
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        PhotonNetwork.NickName = "Player : " + a.ToString();
       
        foreach (var player in PhotonNetwork.CurrentRoom.Players) {
            Debug.Log($"{player.Value.NickName}");
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
        GameManager.instance.lobby.gameObject.SetActive(false);
        GameManager.instance.quizManager.state = QuizManager.State.ready;
        PhotonNetwork.JoinRandomRoom();
    }
    //Muti Room
    public void ReadyedPlayer(string room)
    {
        PhotonNetwork.JoinRoom(room) ;
    }
}

