using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PositionSync : MonoBehaviourPunCallbacks, IPunObservable
{

    Vector3 CurrentPos;
    Quaternion Rotate;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(this.gameObject.transform.position);

            stream.SendNext(this.gameObject.transform.rotation);
        }
        else
        {
            CurrentPos = (Vector3)stream.ReceiveNext();

            Rotate = (Quaternion)stream.ReceiveNext();
        }
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = CurrentPos;
        transform.rotation = Rotate;
    }
}
