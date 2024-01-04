using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject head;
    public GameObject handL;
    public GameObject handR;
    public Material[] Mat;
    public MeshRenderer[] Renders;
    public int myNumber;
    public Transform trashspawnpoint;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        GameManager.instance.player.gameObject.transform.position = GameManager.instance.spawnpoints[a - 1].position;
        GameManager.instance.player.gameObject.transform.rotation = GameManager.instance.spawnpoints[a - 1].rotation;
        GameManager.instance.player.JoinGame();


        GameObject PlayerVR=Instantiate(GameManager.instance.playerVR,transform.position,transform.rotation);
        head = PlayerVR.GetComponent<PlayerChase>().head;
        handL= PlayerVR.GetComponent<PlayerChase>().hand1;
        handR = PlayerVR.GetComponent<PlayerChase>().hand2;

        GameObject ppp=PhotonNetwork.Instantiate("PPP", transform.position, transform.rotation, 0);
        Material[] temp = { Mat[myNumber-1] };
        try {
            ppp.GetComponentsInChildren<MeshRenderer>()[0].materials = temp;
            ppp.GetComponentsInChildren<MeshRenderer>()[1].materials = temp;
            ppp.GetComponentsInChildren<MeshRenderer>()[2].materials = temp;
		} catch {

		}

    }
    // Update is called once per frame
    public void JoinGame()
    {
        myNumber = GameManager.instance.netWorkGameManager.currentplayerNum;
    }
}
