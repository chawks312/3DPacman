using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jump_power = 5.0f;

    public int jump_counter = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, mouseX * sensitivity, 0f);
        transform.Rotate(rotation);

        if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0)
        {
            Jump();
        }
    }

    void Jump() {
        rb.velocity = new Vector3(0, jump_power, 0);
        jump_counter--;
    }
}
