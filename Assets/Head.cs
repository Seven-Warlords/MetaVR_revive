using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class Head : MonoBehaviour
{

    public GameObject head;
    private Transform myTR;
    private PhotonView myPV;
    public int humantype = 1;
    // Start is called before the first frame update
    void Start()
    {
        myTR = GetComponent<Transform>();
        myPV = GetComponent<PhotonView>();

        if(myPV.IsMine) {
            switch (humantype){
                case 1:
                    head = GameManager.instance.player.head;
                    break;
                case 2:
                    head = GameManager.instance.player.handL;
                    break;
                case 3:
                    head = GameManager.instance.player.handR;
                    break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myPV.IsMine) {
            myTR.transform.position = head.transform.position;
            myTR.transform.rotation = head.transform.rotation;
        }
        
    }
}
