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
    bool shouldJump;
    bool isJumping;
    [SerializeField]
    private float Speed;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AskQuestion askQuestion;

    private float timeStopped;

    bool question;
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
                timeStopped = 0;
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
        if (shouldJump) {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("grounded", false);
            jumps++;
            shouldJump = false;
            isJumping = true;
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
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2 && !shouldJump) {
            Debug.Log("Jump");
            shouldJump = true;

        } else if (isJumping && !shouldJump && Physics2D.OverlapCircle(FloorCheck.position, 0.1f, Ground) != null)
        {
            jumps = 0;
            animator.SetBool("grounded", true);
            isJumping = false;
        }
    }

    private void AskQuestion()
    {
        if (question)
            return;

        Freeze(true);
        askQuestion.AskNewQuestion();
        question = true;
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        Freeze(false);
        Debug.Log(transform.position + "; " + Mathf.FloorToInt(transform.position.x / 32f));
        transform.position = new Vector2(Mathf.FloorToInt(transform.position.x / 32f)*32, 18);
        question =false;
    }

    public void ReloadLevel()
    {
        Freeze(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        question = false;
    }

    public void Freeze(bool value)
    {
        if (value)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
