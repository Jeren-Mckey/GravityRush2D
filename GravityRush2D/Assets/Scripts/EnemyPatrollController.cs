using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrollController : MonoBehaviour
{
    private Vector3 returnTarget;
    public Transform target; 
    private bool reachedEndOfPath;
    private float distance;
    private Vector3 direction;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        distance = VectorMath.SqrDistanceXZ(transform.position, target.position);
        direction = (target.position - transform.position);
        returnTarget = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = VectorMath.SqrDistanceXZ(target.position, transform.position);
        if (distance >= 0 && distance < .01)
        {
            Vector3 temp = target.position;
            target.position = returnTarget;
            returnTarget = temp;
            direction = (target.position - transform.position);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
}
