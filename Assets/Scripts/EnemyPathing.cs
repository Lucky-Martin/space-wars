using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;

    int currentWaypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        //Start the the first waypoint
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig wave)
    {
        waveConfig = wave;
    }

    private void Move()
    {
        //Check if there are avail waypoints
        if (currentWaypointIndex <= waypoints.Count - 1)
        {
            //Get the target positions
            Vector2 targetPosition = waypoints[currentWaypointIndex].transform.position;
            //Calculate how much we will be moving this frame more = fast, less = slow
            float frameMovement = waveConfig.moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, frameMovement);

            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

            if (currentPosition == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
