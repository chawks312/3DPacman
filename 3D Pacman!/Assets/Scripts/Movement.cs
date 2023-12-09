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
    public TextMeshProUGUI jumpText;

    private Rigidbody rb;  // player rigid body
    private bool canControl;
    private int score;
    private GameObject[] ghosts;
    private GameObject[] points;

    // Start is called before the first frame update
    void Start()
    {
        print("start");
        rb = GetComponent<Rigidbody>();
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        points = GameObject.FindGameObjectsWithTag("Point");
        // freeze the camera rotation at the start of the game
        rb.freezeRotation = true;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canControl) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0f, vertical);
            transform.Translate(movement * speed * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X");
            Vector3 rotation = new Vector3(0f, (mouseX * sensitivity), 0f);
            transform.Rotate(rotation);

            if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0 && isGrounded())
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
            // print("Collision with Ghost");
            canControl = false;
            loseTextObject.SetActive(true);
            againButton.SetActive(true);
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            //print("Collision with Ghost");
            canControl = false;
            loseTextObject.SetActive(true);
            againButton.SetActive(true);
            Cursor.visible = true;
        }

        if (other.gameObject.CompareTag("Point")) {
            //print("Collision with point");
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            //print("Collision with jump");
            other.gameObject.SetActive(false);
            jump_counter += 2;
            SetJumpText();
        }
    }

    void Jump() {
        rb.velocity = new Vector3(0, jump_power, 0);
        jump_counter--;
        SetJumpText();
    }

    void SetScoreText() {
        scoreText.text = "Score: " + score.ToString();
        foreach (GameObject ghost in ghosts)
        {
            GhostAI ghostAi = ghost.GetComponent<GhostAI>();
            if (ghostAi != null)
            {
                ghostAi.IncreaseSpeed(score);
            }
        }
    }

    void SetJumpText()
    {
        jumpText.text = "Jumps: " + jump_counter.ToString();
    }

    private bool isGrounded() {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f) &&
               hit.collider.CompareTag("Ground");
    }

    public void Reset()
    {
        // print("Reset");
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
        foreach (GameObject point in points) {
            Point p = point.GetComponent<Point>();
            if (p != null) {
                p.Reset();
                p.gameObject.SetActive(true);
            }
        }
        SetJumpText();
        SetScoreText();
    }
}
