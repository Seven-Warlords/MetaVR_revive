using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetItem : MonoBehaviour
{
    public GameObject Pet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.tag == "Knife")
        {
            Instantiate(Pet, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       gameObject.GetComponent<BoxCollider>().isTrigger = true;
        if (other.tag == "Knife")
        {
            Instantiate(Pet, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
