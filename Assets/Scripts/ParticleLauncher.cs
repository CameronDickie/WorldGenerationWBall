using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem collisionParticle;
    public GameObject player;
    
    Attractor primary;
    List<ParticleCollisionEvent> collisionEvents;
    ParticleSystem.Particle[] particles;


    public float forceMag;
    

    // Use this for initialization
    void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
        primary = GetComponentInParent<Attractor>();
	}

    void CalcForce()
    {
        //find the direction vector between player and particle transforms
        particleLauncher.GetParticles(particles);
        Vector3 dir = new Vector3();
        for (int i = 0; i < particles.Length; i++)
        {
            //find dir vector between index and player
            dir = (player.transform.position - particles[i].position).normalized;
            float dist = Vector3.Distance(player.transform.position, particles[i].position);

            particles[i].velocity = dir * Mathf.Sqrt(player.GetComponent<Rigidbody>().mass * primary.G / dist);
            //set magnitude equal to calculated velocity based on attractor

        }

        particleLauncher.SetParticles(particles, particles.Length);
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, player, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent pCE)
    {
        /*
         * To set the particle to spawn at the POI (point of intersection) use the following statement
         * collisionParticle.transform.position = pCE.intersection;
         * To calculate rotation
         * collisionParticle.transform.rotation = Quaternion.LookRotation (particleCollisionEvent.normal)
         */
        collisionParticle.Emit(1);
    }

    // Update is called once per frame
    void Update () {
        particleLauncher.Emit(1); //emits 1 particle every frame **CHANGE 
        CalcForce();
	}
}
