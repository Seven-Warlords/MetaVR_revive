using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfClose : MonoBehaviour
{
    public float closecool = 3f;
 

    // Update is called once per frame
    void Update()
    {
        closecool += Time.deltaTime;
        if(closecool > 3f) {
            gameObject.SetActive(false);
		} else {
            gameObject.SetActive(true);
        }
    }
}
