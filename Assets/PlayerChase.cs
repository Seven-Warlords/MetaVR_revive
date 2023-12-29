using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    // Start is called before the first frame update

    static public PlayerChase Incetance;
    public GameObject head;
    public GameObject hand1;
    public GameObject hand2;

    void Start()
    {
        if(Incetance == null) {
            Incetance = this;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
