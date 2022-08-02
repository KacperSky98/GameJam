using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    [SerializeField] private Transform leftPointObject;
    [SerializeField] private Transform rightPointObject;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    
    [SerializeField] private bool startLeft;
    public Vector3 target;

    [SerializeField] private float movementSpeed = 10f;


    void Start()
    {
        leftPoint = new Vector3(leftPointObject.position.x, leftPointObject.position.y, leftPointObject.position.z) ;
        rightPoint = new Vector3(rightPointObject.position.x, rightPointObject.position.y, rightPointObject.position.z);
        if (startLeft)
        {
            this.transform.position = leftPointObject.position;
        }
        else {
            this.transform.position = rightPointObject.position;
            this.transform.localScale= new Vector3(this.transform.localScale.x*-1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startLeft)
        {
            target = rightPoint;
        }
        else {
            target = leftPoint;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed*Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
            if (startLeft)
            {
                startLeft = false;
                target = leftPoint;
            }
            else
            {
                startLeft = true;
                target = rightPoint;
            }
        }
    }
}
