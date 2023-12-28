using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootObj : MonoBehaviour
{
    Rigidbody rigd;
    Transform trans;

    public bool shootTrigger = false;
    public float force = 10;
    public Vector3 direction= new Vector3(10, 10, 10);
    private bool upForce = true;

    public Transform targetTR;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();

        direction = direction.normalized;
        //rigd.AddForce(direction*force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (shootTrigger)
        {
            Vector3 bottomShoot = new Vector3(direction.x, 0, direction.z);
            Vector3 upShoot = new Vector3(0,direction.y,0);
            trans.Translate(8 * Time.fixedDeltaTime * bottomShoot);
            if (upForce)
            {
                rigd.AddForce(upShoot * force, ForceMode.Impulse);
                upForce = false;
            }
        }
        else
        {
            if (targetTR != null)
            {
                distance = Vector3.Distance(targetTR.position, trans.position);
                direction = (targetTR.position - trans.position).normalized;
                float timeDis = distance / 8 * Time.fixedDeltaTime;
                direction = direction.normalized;
            }
            upForce = true;
        }
    }
}
