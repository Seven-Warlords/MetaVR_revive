using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EscapeObject : MonoBehaviour
{
    public enum EscapeStatus
    {
        IN,
        OUT
    }
    public EscapeStatus status;
    private void FixedUpdate()
    {
        if (GameManager.instance.playerVR)
        {
            if (GameManager.instance.playerVR.transform.position.y < transform.position.y)
            {
                GameManager.instance.playerVR.transform.position = GameManager.instance.lobby.transform.position;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (status == EscapeStatus.IN)
        {
            if (other.gameObject.CompareTag("Trash"))
            {
                PhotonView pv;
                if(other.gameObject.TryGetComponent<PhotonView>(out pv))
                {
                    if (pv.IsMine)
                    {
                        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        other.gameObject.transform.position = GameManager.instance.player.trashspawnpoint.position;
                    }
                }
            }
        }
    }
}
