using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaceCarMove : MonoBehaviour
{
    [SerializeField]
    private Transform targetPoint;

    private NavMeshAgent navMeshAgent;

    private Vector3 lastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();

        //if (navMeshAgent != null)
        //{
        //    Vector3 targetVector = targetPoint.transform.position;
        //    navMeshAgent.SetDestination(targetVector);
        //}

        //// StartCoroutine(AimFront(0.3f));
    }

    void Update()
    {
        var speed = 60.0f;
        var rotationSpeed = 100.0f;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        var translation = Input.GetAxis("Vertical") * speed;
        var rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);
        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }

    //private IEnumerator AimFront(float waitTime)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(waitTime);

    //        var speedVector = transform.position - lastPosition;

    //        lastPosition = transform.position;

    //        transform.rotation = Quaternion.LookRotation(speedVector);
    //    }
    //}

}
