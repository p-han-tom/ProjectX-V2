using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Update is called once per frame
    Transform leader;
    Vector3 leaderVelocity;
    Vector3 mousePos;
    Vector3 leaderPos;
    float xMaxDist = 1f;
    float yMaxDist = 1f;
    float cameraTime = 0.1f;
    float maxCameraSpeed = 100f;
    void Start()
    {
        leader = GameObject.Find("Player").transform;
        transform.position = new Vector3(leader.position.x, leader.position.y, transform.position.z);
    }
    public void TeleportToLeader() { transform.position = new Vector3(leader.position.x, leader.position.y, transform.position.z); }
    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        leaderPos = leader.position;
        leaderVelocity = leader.GetComponent<Rigidbody2D>().velocity;
        float x = (leaderPos.x + mousePos.x) / 2;
        if (x > leaderPos.x + xMaxDist) x = leaderPos.x + xMaxDist;
        else if (x < leaderPos.x - xMaxDist) x = leaderPos.x - xMaxDist;
        float y = (leaderPos.y + mousePos.y) / 2;
        if (y > leaderPos.y + yMaxDist) y = leaderPos.y + yMaxDist;
        else if (y < leaderPos.y - yMaxDist) y = leaderPos.y - yMaxDist;
        Vector3 center = new Vector3(x, y, transform.position.z);
        if (leader != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, center, ref leaderVelocity, cameraTime, maxCameraSpeed);
        }
    }

    public void setTarget(Transform target)
    {
        leader = target;
    }
    public IEnumerator cameraShake(float duration, float magnitude)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
