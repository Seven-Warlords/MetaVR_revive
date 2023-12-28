using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashTag
    {
        Paper,
        Can,

    }
    public Player catchPlayer;
    public TrashTag trashTag;
    Rigidbody rigd;
    Transform trans;

    public bool shootTrigger = false;
    public float speed = 6f;
    public float high = 5f;
    public float distance;
    public Vector3 direction;
    private bool upForce = true;
    private float timeX = 0;

    // Start is called before the first frame update
    public void Start()
    {
        rigd = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
    }
    public virtual void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, direction * 10);
        if (shootTrigger)
        {
            timeX += Time.fixedDeltaTime;
            trans.Translate(speed * Time.fixedDeltaTime * new Vector3(direction.x,0,direction.z).normalized);
            if (upForce)
            {
                rigd.useGravity = false;
                trans.Translate((4*high*speed/distance - 8*high * Mathf.Pow(speed, 2) * timeX / Mathf.Pow(distance, 2)) * Vector3.up * Time.fixedDeltaTime);
            }
        }
        else
        {
            timeX = 0;
            rigd.useGravity = true;
            upForce = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            upForce = false;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (shootTrigger)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                rigd.useGravity = true;
                shootTrigger = false;
            }
        }
    }
}
