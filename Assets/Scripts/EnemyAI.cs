using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    
    public GameObject target;
    public float moveSpeed = 3.0f;
    public float detectionDistance = 10.0f;
    public float stopDistance = 5.0f;

    private Vector3 dir = Vector3.right;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        lookTowards();
        axisMovement();
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
        float distance = Vector3.Distance(thisObj, playerObj);
        float speed = moveSpeed * Time.deltaTime;
        if (distance < detectionDistance && distance > stopDistance) {
            gameObject.transform.position = Vector3.MoveTowards(thisObj, playerObj, speed);
            strafe();
        } else {
            strafe();
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
