using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jump_power = 5.0f;
    public GameObject loseTextObject;  // text displayed when a player wins
    public GameObject againButton;        // button to play again
    public int jump_counter = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        print("start");
       // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        Reset();
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

    private void OnCollisionEnter(Collision other)
    {
        print("woah collision!" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Ghost"))
        {
            print("Collision with Ghost");
            loseTextObject.SetActive(true);
            againButton.SetActive(true);
        }
    }

    void Jump() {
        rb.velocity = new Vector3(0, jump_power, 0);
        jump_counter--;
    }

    public void Reset()
    {
        print("Reset");
        // rb.transform.position = new Vector3(0f, 2f, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        loseTextObject.SetActive(false);
        againButton.SetActive(false);
    }
}
