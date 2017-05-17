using UnityEngine;
using System.Collections;
using Pathfinding;

public class AI_Pather : MonoBehaviour {

    public Transform target;
    Seeker seeker;
    Path path;
    int currentWaypoint;
    public float speed = 20f;

    CharacterController characterController;

    float maxWaypointDistance = 2f;

	void Start ()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        characterController = GetComponent<CharacterController>();
	}


    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        else
            Debug.Log(p.error);
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint>=path.vectorPath.Count)
        {
            return;
        }

        Vector3 dir = (path.vectorPath[currentWaypoint]- this.transform.position).normalized*speed;
        characterController.SimpleMove(dir);

        if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint])< maxWaypointDistance)
            currentWaypoint++;
    }
}
