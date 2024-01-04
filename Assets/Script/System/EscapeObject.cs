using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeObject : MonoBehaviour
{
    public enum EscapeStatus
    {
        IN,
        OUT
    }
    public EscapeStatus status;
    private Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                        other.gameObject.transform.position = GameManager.instance.player.trashspawnpoint.position;
                    }
                }
            }else if (other.gameObject.CompareTag("Player"))
            {
                PhotonView pv;
                if (other.gameObject.TryGetComponent<PhotonView>(out pv))
                {
                    if (pv.IsMine)
                    {
                        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
                        other.gameObject.transform.position = GameManager.instance.spawnpoints[a - 1].position;
                    }
                }
            }
        }
    }
}
