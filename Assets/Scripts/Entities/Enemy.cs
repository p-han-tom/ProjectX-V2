using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    bool collidedWithObstacle;

    Grid grid;

    Transform target;
    Vector3 currentTargetPosition;
    Vector3 currentWaypoint;
    Vector3[] path;
    int targetIndex;

    protected override void Start()
    {
        base.Start();
        target = GameObject.Find("Player").transform;
        movementSpeed = new Stat(3f);
        grid = GameObject.Find("Pathfinding").GetComponent<Grid>();

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
        if (path.Length == 0) yield break;
        targetIndex = 0;
        currentWaypoint = path[0];

        if (path.Length >= 2) {
            Vector2 tempPos = new Vector2((float)Math.Round(transform.position.x*2)/2,(float) Math.Round(transform.position.y*2)/2);
            bool outsideRange = (tempPos.x < path[0].x && tempPos.x < path[1].x) || (tempPos.x > path[0].x && tempPos.x > path[1].x) || (tempPos.y < path[0].y && tempPos.y < path[1].y) || (tempPos.y > path[0].y && tempPos.y > path[1].y);

            float distance = Vector2.Distance(tempPos,path[0]) + Vector2.Distance(tempPos,path[1]);
            float total = Vector2.Distance(path[0], path[1]);

            if (total - 0.35f <= distance && total + 0.35f >= distance) {
                targetIndex++;
                currentWaypoint = path[1];
            }
        }

        while (true) {
            
            if (Mathf.Abs(Vector2.Distance(transform.position, currentWaypoint)) < 0.25f) {
                targetIndex ++;
                if(targetIndex >= path.Length){
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
            }

            currentWaypoint = path[targetIndex];



            Vector2 movement = (currentWaypoint-transform.position).normalized;
            if (!underAttack) rb.velocity = movement * movementSpeed.value();

            yield return null;
        }
    }

    void ResetPath() {
        targetIndex = 0;
        currentWaypoint = path[0];
    }

   	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one/4);

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
        if (Vector2.Distance(currentTargetPosition, target.position) > 0.5f) {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            currentTargetPosition = target.position;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer ==  LayerMask.NameToLayer("Obstacle")) {
            collidedWithObstacle = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            collidedWithObstacle = false;
        }
    }

}

