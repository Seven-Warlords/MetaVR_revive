using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pppcolorchange : MonoBehaviour {

    public Material[] Mat;
    public MeshRenderer[] Renders;
    public NetWorkGameManager plr;
	// Start is called before the first frame update
	private void Awake() {
        plr = GameObject.Find("NetworkGameManager").GetComponent<NetWorkGameManager>();
        Material[] temp = { Mat[plr.playercount - 1] };
        try {
            gameObject.GetComponentsInChildren<MeshRenderer>()[0].materials = temp;
            gameObject.GetComponentsInChildren<MeshRenderer>()[1].materials = temp;
            gameObject.GetComponentsInChildren<MeshRenderer>()[2].materials = temp;
        } catch {

        }
    }

	// Update is called once per frame
	void Update()
    {
       
    }
}
