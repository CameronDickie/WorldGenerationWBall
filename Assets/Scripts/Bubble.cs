using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
    public float particleRad;
    float minMass;
    float maxMass;
    float popMag;
    float popRad;
    float timer;

    Rigidbody rb;
    void Start()
    {
        MakeBubble();
    }
    public void MakeBubble() {
        timer = 0;
        rb = this.gameObject.GetComponent<Rigidbody>();
        popMag = 1000f;
        popRad = rb.mass;
        minMass = 7;
        maxMass = 15;
        rb.mass = Random.Range(minMass, maxMass);
        this.transform.localScale = new Vector3(Mathf.Sqrt(rb.mass), Mathf.Sqrt(rb.mass), Mathf.Sqrt(rb.mass));
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //add explosion force
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(popMag, this.transform.position, popRad);
            other.gameObject.GetComponent<Player>().RemoveCollider(this.gameObject.GetComponent<Collider>());
            Pop();
        }
    }

    void Update () {
        //update rb.position to be equal to sin(time)
        timer += Time.deltaTime;
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Sin(timer), rb.velocity.z);
	}
    void Pop()
    {
        this.gameObject.GetComponent<Attractor>().RemoveThisAttractor();
        Collider[] nearby = Physics.OverlapSphere(transform.position, particleRad);
        foreach (Collider nearbyObject in nearby)
        {
            if(nearbyObject.CompareTag("Player")) { 
                
            }
        }
        Destroy(this.gameObject);
        //find an efficient way to remove the attractor from the array of attractors   
    }

    private void OnParticleCollision(GameObject other)
    {
        
    }
}
