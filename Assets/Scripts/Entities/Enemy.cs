using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    Transform target;
    Vector3 currentTargetPosition;
    Vector3[] path;
    int targetIndex;

    protected override void Start()
    {
        base.Start();
        target = GameObject.Find("Player").transform;
        movementSpeed = new Stat(3);

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        currentTargetPosition = target.position;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        targetIndex = 0;
        Vector3 currentWaypoint = path[0];
        while (true) {
            if (Vector2.Distance(transform.position, currentWaypoint) < 0.5f) {
                targetIndex ++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            Vector2 movement = new Vector2(currentWaypoint.x - transform.position.x, currentWaypoint.y - transform.position.y).normalized * movementSpeed.value();
            rb.velocity = movement;
            yield return null;
        }
    }

    public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one / 5);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentTargetPosition != target.position) {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            currentTargetPosition = target.position;
        }
    }
}
