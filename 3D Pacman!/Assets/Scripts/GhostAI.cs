using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float ghost_speed = 2.0f;

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

            transform.Translate(dir * ghost_speed * Time.deltaTime);
        }
    }

    public void Reset()
    {
        transform.position = origin_position;
    }
}
