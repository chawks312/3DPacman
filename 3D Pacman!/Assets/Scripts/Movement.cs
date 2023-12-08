using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;  // movement speed
    public float sensitivity = 2.0f;  // camera lookaround sensitivity
    public float jump_power = 15.0f;  // power of jump
    public GameObject loseTextObject;  // text displayed when a player wins
    public GameObject againButton;        // button to play again
    public int jump_counter;  // number of initial jumps (10 for testing)

    public TextMeshProUGUI scoreText;

    private Rigidbody rb;  // player rigid body
    private bool canControl;
    private int score;
    private GameObject[] ghosts;

    // Start is called before the first frame update
    void Start()
    {
        print("start");
        rb = GetComponent<Rigidbody>();
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // I assume this line is causing cringe stuff but also if removed the object is not stable (camera spins cuz object is spinning)
            // if can find way to stabilize camera without this line might be good
        // maybe we just don't need rigidbody cuz we don't care too much abt physics except collision
        // idk
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        if (canControl) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0f, vertical);
            transform.Translate(movement * speed * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X");
            Vector3 rotation = new Vector3(0f, (mouseX * sensitivity), 0f);
            transform.Rotate(rotation);

            if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0)
            {
                Jump();
            }
        }

        if (rb.position.y <= -5) {
            Reset();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        print("woah collision!" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Ghost"))
        {
            print("Collision with Ghost");
            canControl = false;
            loseTextObject.SetActive(true);
            againButton.SetActive(true);
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point")) {
            print("collider with point");
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
    }

    void Jump() {
        rb.velocity = new Vector3(0, jump_power, 0);
        jump_counter--;
    }

    void SetScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

    public void Reset()
    {
        print("Reset");
        canControl = true;
        jump_counter = 10;
        score = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.transform.position = new Vector3(0f, 2f, 0f);
        rb.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        loseTextObject.SetActive(false);
        againButton.SetActive(false);
        Cursor.visible = false;
        // have to also make ghosts return to some position as well
        foreach (GameObject ghost in ghosts) {
            GhostAI ghostAi = ghost.GetComponent<GhostAI>();
            if (ghostAi != null) {
                ghostAi.Reset();
            }
        }
        SetScoreText();
    }
}
