using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PT_Ready : MonoBehaviour
{
    [SerializeField]
    private Image[] readyPly;
    public Image[] ReadyPly { get { return readyPly; }}

    [SerializeField]
    private Button readyBtn;
    public Button ReadyBtn { get { return readyBtn; } }
    
    private void OnEnable()
    {
        for(int i = 0; i < readyPly.Length; i++)
        {
            readyPly[i].color = Color.white;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TMP_Text text=ReadyBtn.transform.GetChild(0).GetComponent<TMP_Text>();
            text.text = "Start";
        }
    }

}
