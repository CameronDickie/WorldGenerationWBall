using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    public Rigidbody rb;
    public float G = 8 *6.674f;

    public static List<Attractor> Attractors;

    private void FixedUpdate()
    {

        foreach (Attractor attractor in Attractors)
        {
            if(attractor != this) Attract(attractor);
        }
        
    }
    private void OnEnable()
    {
        if(Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }
    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 dir = rb.position - rbToAttract.position;
        float distance = dir.magnitude;

        float forceMagnitude = (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2) *G;
        Vector3 force = dir.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
    public void RemoveThisAttractor()
    {
        if(Attractors.Contains(this))
        {
            Attractors.Remove(this);
        }
    }
}
