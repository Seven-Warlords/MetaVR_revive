using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChange : MonoBehaviour
{
    public GameObject hand;
    public GameObject knife;
    public GameObject hosu;
    public int T  = 0;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }

   public void Change()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.One,ARAVRInput.Controller.RTouch) && T == 1) 
        {
            T = 0;
            hand.SetActive(true);
            knife.SetActive(false);
            hosu.SetActive(false);
        }
        else if (ARAVRInput.GetDown(ARAVRInput.Button.One,ARAVRInput.Controller.RTouch) && T == 0)
        {
            T = 2;
            hand.SetActive(false);
            knife.SetActive(true);
            hosu.SetActive(false);
            knife.transform.position= ARAVRInput.RHandPosition;
            knife.transform.rotation= ARAVRInput.RHand.rotation;
            knife.transform.parent= ARAVRInput.RHand;
        }
     else if(ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && T == 2)
        {
            T = 1;
            hand.SetActive(false);
            knife.SetActive(false);
            hosu.SetActive(true);
            hosu.transform.position = ARAVRInput.RHandPosition;
            hosu.transform.rotation = ARAVRInput.RHand.rotation;
            hosu.transform.parent= ARAVRInput.RHand;
        }
    }
}
