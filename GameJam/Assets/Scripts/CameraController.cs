using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,player.position.y-3,this.transform.position.z);       
    }

}
