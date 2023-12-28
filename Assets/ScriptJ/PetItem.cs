using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetItem : Trash
{
    public GameObject Pet;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.tag == "Knife")
        {
            Instantiate(Pet, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Knife")
        {
            Instantiate(Pet, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
