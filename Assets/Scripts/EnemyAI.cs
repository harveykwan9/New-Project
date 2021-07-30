using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    
    public GameObject target;
    public float detectionRadius = 10.0f;

    // Start is called before the first frame update
    void Start(){
    }
    
    // Update is called once per frame
    void Update()
    {
        lookTowards();
    }
    
    // Look towards a target object all the time
    void lookTowards() {
        Vector3 thisObj = gameObject.transform.position;
        Vector3 playerObj = target.transform.position;
        Vector3 location = playerObj - thisObj;
        Vector3 temp = Vector3.RotateTowards(thisObj, location, 10, 100);
        gameObject.transform.rotation = Quaternion.LookRotation(temp);
    }
    
    // Movement based on detection
    void axisMovement() {
        Vector3 thisObj = gameObject.transform.position;
        Vector3 playerObj = target.transform.position;
    }
}
