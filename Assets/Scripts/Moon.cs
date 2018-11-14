using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {


    public GameObject primary;
    Rigidbody rbPrimary;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        primary = this.gameObject.GetComponentInParent<GameObject>();
        rbPrimary = primary.GetComponent<Rigidbody>();
        // base this.transform.localScale proportional to the mass of the primary
        rb.mass = 1 / rbPrimary.mass;
        this.transform.localScale = new Vector3(1 / primary.transform.localScale.x, 1 / primary.transform.localScale.y, 1 / primary.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
