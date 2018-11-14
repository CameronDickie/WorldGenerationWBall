using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour {

    public List<GameObject> points;// a gameobject list of all points 
	void Start () {
        points = Screen(this.gameObject.GetComponentsInChildren<GameObject>(), "Point");   
	}
    /*
    private void Update()
    {
        checkParticles();
    }

    private void findParticles()
    {

    }
    private void checkParticles()
    {
        
    }
    */

    List<GameObject> Screen(GameObject[] list, String tag)
    {
        List<GameObject> newList = new List<GameObject>();
        for (int i = 0; i < list.Length; i++)
        {
            if(list[i].CompareTag(tag))
            {
                newList.Add(list[i]);
            }
        }
        return newList;
    }

    
}
