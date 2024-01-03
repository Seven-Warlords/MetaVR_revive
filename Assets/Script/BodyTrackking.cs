using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class BodyTrackking : MonoBehaviour
{

    public GameObject mybody;
    private Transform myTR;
    private PhotonView myPV;
    public MeshRenderer myMR;
    public int humantype = 1;
    // Start is called before the first frame update
    void Start()
    {

        myTR = GetComponent<Transform>();
        myPV = GetComponent<PhotonView>();

        if(myPV.IsMine) {
            switch (humantype){
                case 1:
                    mybody = GameManager.instance.playerChase.head;
                    break;
                case 2:
                    mybody = GameManager.instance.playerChase.hand1;
                    break;
                case 3:
                    mybody = GameManager.instance.playerChase.hand2;
                    break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myPV.IsMine && mybody!=null) {
            //myMR.enabled = false;
            myTR.transform.position = mybody.transform.position;
            myTR.transform.rotation = mybody.transform.rotation;
        }
        
    }
}
