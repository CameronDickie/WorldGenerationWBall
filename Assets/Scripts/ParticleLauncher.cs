using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem collisionParticle;
    public GameObject player;
    List<ParticleCollisionEvent> collisionEvents;
    
    // Use this for initialization
    void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
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
       particleLauncher.Emit(1); //emits 1 particle every frame
        
	}
}
