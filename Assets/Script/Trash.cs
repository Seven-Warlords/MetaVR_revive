using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashTag
    {
        Paper,
        Can,
        Pet,
    }
    public Player catchPlayer;
    public TrashTag trashTag;
    Rigidbody rigd;
    Transform trans;
    Collider collider;

    public bool shootTrigger = false;
    public float speed = 6f;
    public float high = 5f;
    public float distance;
    public Vector3 direction;
    private bool upForce = true;
    private float timeX = 0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rigd = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }
    public virtual void FixedUpdate()
    {
        if (shootTrigger)
        {
            collider.enabled = false;
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

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            collider.enabled = true;
            upForce = false;
            rigd.useGravity = true;
            shootTrigger = false;
        }
        if (other.gameObject.CompareTag("Hand"))
        {
        }
    }
    public virtual void OnCollisionEnter(Collision collision)
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
