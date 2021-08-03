using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    
    public GameObject target;
    public float moveSpeed = 3.0f;
    public float detectionDistance = 10.0f;
    public float stopDistance = 5.0f;
    public float lookSpeed = 5.0f;

    private Vector3 dir = Vector3.right;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement();
    }

    // Smoothly look towards a target object all the time
    void lookTowards() {
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position).normalized;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }

    // Movement based on detection
    // If player is within the detection distance and stop distance, move towards and look at player
    // If player is within the stop distance, only look at player
    // If moved away, move back to the user defined idle location
    void axisMovement() {
        Vector3 thisObj = gameObject.transform.position;
        Vector3 playerObj = target.transform.position;
        float distance = Vector3.Distance(thisObj, playerObj);
        float speed = moveSpeed * Time.deltaTime;
            
        if (distance < detectionDistance && distance > stopDistance) {
            gameObject.transform.position = Vector3.MoveTowards(thisObj, playerObj, speed);
            lookTowards();
        } else if (distance < stopDistance) {
            lookTowards();
        } else {
            
        }
    }

    void strafe() {
        gameObject.transform.Translate(dir*moveSpeed*Time.deltaTime);
        if (gameObject.transform.position.x >= -4) {
            dir = Vector3.left;
        } else if (gameObject.transform.position.x <= 4){
            dir = Vector3.right;
        }
    }
}
