using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject head;
    public GameObject handL;
    public GameObject handR;
  
    public int myNumber;
    public Transform trashspawnpoint;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        GameManager.instance.player.gameObject.transform.position = GameManager.instance.spawnpoints[a - 1].position;
        GameManager.instance.player.gameObject.transform.rotation = GameManager.instance.spawnpoints[a - 1].rotation;
        GameManager.instance.player.JoinGame();


        GameObject PlayerVR = GameManager.instance.playerVR;
        PlayerVR.transform.position = GameManager.instance.spawnpoints[a - 1].position;
        head = PlayerVR.GetComponent<PlayerChase>().head;
        handL= PlayerVR.GetComponent<PlayerChase>().hand1;
        handR = PlayerVR.GetComponent<PlayerChase>().hand2;

        GameObject ppp=PhotonNetwork.Instantiate("PPP", transform.position, transform.rotation, 0);
    }
	public void Update() {
      
    }
	// Update is called once per frame
	public void JoinGame()
    {
        myNumber = GameManager.instance.netWorkGameManager.currentplayerNum;
    }
}
