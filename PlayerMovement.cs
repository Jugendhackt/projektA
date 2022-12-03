using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
    private int jumps;
    [SerializeField]
    private Transform FloorCheck;
    [SerializeField]
    private LayerMask Ground;
    bool jump;
    [SerializeField]
    private float Speed;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AskQuestion askQuestion;

    private float timeStopped;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        if (rb.velocity.x < Speed * 0.1f)
        {
            timeStopped += Time.deltaTime;
            if(timeStopped >= 1)
            {
                AskQuestion();
            }
        }
        else
        {
            timeStopped = 0;
        }
    }
    private void Awake()
    {

    }
    private void FixedUpdate()
    {
        if (jump) {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("grounded", false);
            jumps++;
            jump = false;
        }

        if (rb.velocity.x < Speed * 0.1f)
            Debug.Log("Stopped");
        rb.velocity = new Vector2(Speed, rb.velocity.y);

        if(transform.position.y < -30)
        {
            AskQuestion();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2 && !jump) {
            Debug.Log("Jump");
            jump = true;

        } else if (Physics2D.OverlapCircle(FloorCheck.position, 0.1f,Ground) != null && !jump)
        {
            jumps = 0;
            animator.SetBool("grounded", true);
        }
    }

    private void AskQuestion()
    {
        Time.timeScale = 0;
        askQuestion.AskNewQuestion();
    }
}
