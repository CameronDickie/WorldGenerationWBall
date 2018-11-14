using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jumps;
    public float jumpTimer;
    public List<Collider> Colliders;
    public ParticleSystem jumpEffect;

    public Vector3 pos;
    public Vector3 vel;
    public Vector3 acc;

    Rigidbody rb;

    float mag;
    float jumpMag;
    float maxSpeed;

    bool jumpCool;

    

    void Start () {
        rb = GetComponent<Rigidbody>();
        jumps = 2;
        Colliders = new List<Collider>();
        mag = 25f;
        jumpMag = 450f;
        maxSpeed = 20f;
        jumpTimer = 0f;
        jumpCool = false;
	}
	//Updates every frame
    void Update()
    {
        UpdateStats();
    }
	// FixedUpdate is called once per frame after rendering **for physics**
	void FixedUpdate () {
        Move();
        Jump();
	}
    void Move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            //move left
            if (rb.velocity.x > -maxSpeed) rb.AddForce(Vector3.left.normalized * mag); 
            else rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        } else if(Input.GetKey(KeyCode.D))
        {
            //move right
            if (rb.velocity.x < maxSpeed) rb.AddForce(Vector3.right.normalized * mag);
            else rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            //move forward
            if(rb.velocity.z < maxSpeed)rb.AddForce(Vector3.forward.normalized * mag);
            else rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
        } else if(Input.GetKey(KeyCode.S))
        {
            //move backwards
            if(rb.velocity.z > -maxSpeed) rb.AddForce(Vector3.back.normalized * mag);
            else rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxSpeed);
        }

    }


    internal void RemoveCollider(Collider bubble)
    {
        
        
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            //as long as jumping is not on cooldown, commit jump
            if(!jumpCool)
            {
                if (jumps == 1)
                {
                    jumpMag = 900f;
                    jumpEffect.Play();
                    
                } else
                {
                    jumpMag = 450f;
                }
                rb.AddForce(Vector3.up * jumpMag);
                jumps--;
                jumpCool = true;

            }
            
        }
        
        if(jumpCool) jumpTimer += Time.deltaTime;
        if (jumpTimer > 1)
        {
            jumpCool = false;
            jumpTimer = 0;
        }
        
        if (checkGrounded())
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                jumps = 2;
            }

        }
        
    }
    bool checkGrounded()
    {
        //for all items in colliders, if any have the tag platform then return true
        for(int i = 0; i < Colliders.Count; i++)
        {
            if(Colliders[i].gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
    void UpdateStats()
    {
        pos = rb.position;
        vel = rb.velocity;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bubble")) return;
        if(!Colliders.Contains(other.collider)) Colliders.Add(other.collider);

    }
    private void OnCollisionExit(Collision other)
    {
        if (Colliders.Contains(other.collider))
        {
            Colliders.Remove(other.collider);
        }
        
    }
}
