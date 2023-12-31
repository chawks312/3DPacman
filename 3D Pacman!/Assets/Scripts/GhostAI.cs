using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float ghost_speed = 2.0f;
    private float extra_speed = 0.0f;

    private Transform player;
    private Vector3 origin_position;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        origin_position = transform.position;

        if (player == null) {
            Debug.LogError("'Player' tag not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // also want to make it so that ghosts move slower when travelling through walls
        // should just be a simple collision detection (they can move through walls rn so like while touching wall, speed -1 or smth)
        if (player != null) {
            Vector3 dir = (player.position - transform.position).normalized; // find direction to player

            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.3f);

            //transform.Translate(dir * (ghost_speed + extra_speed) * Time.deltaTime);
            transform.Translate(Vector3.forward * (ghost_speed + extra_speed) * Time.deltaTime);
        }
    }

    // this stuff doesnt work
    private void OnTriggerEnter(Collider other)
    {
        // print("collision ghost + unknown detected");
        if (other.gameObject.CompareTag("Wall")) {
            // print("ghost enter wall");
            ghost_speed = 0.5f;
            extra_speed /= 2.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall")) {
            ghost_speed = 2.0f;
            extra_speed *= 2.0f;
        }
    }

    public void Reset()
    {
        transform.position = origin_position;
        ghost_speed = 2.0f;
        extra_speed = 0.0f;
    }

    public void IncreaseSpeed(int score) {
        extra_speed = (score * score) / 400.0f;
        print("extra speed: " + extra_speed.ToString());
    }
}
