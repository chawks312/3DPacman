using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Vector3 origin_position;
    // Start is called before the first frame update
    void Start()
    {
        origin_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
    }

    public void Reset()
    {
        transform.position = origin_position;
    }
}
