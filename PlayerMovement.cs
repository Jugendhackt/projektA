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
    [SerializeField]
    private float Speed;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AskQuestion askQuestion;

    private float timeStopped;

    bool question;
    bool grounded;
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
    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(FloorCheck.position, 0.1f, Ground) != null)
        {
            jumps = 0;
            animator.SetBool("grounded", true);
            grounded = true;
        } else if(grounded)
        {
            animator.SetBool("grounded", false);
            grounded = false;
        }
        if (shouldJump) {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("grounded", false);
            grounded = false;
            jumps++;
            shouldJump = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && !shouldJump) {
            if(jumps >= 2 && Physics2D.OverlapCircle(FloorCheck.position, 0.1f, Ground) != null)
            {
                jumps = 0;
                animator.SetBool("grounded", true);
                grounded = true;
            }
            if(jumps < 2)
            {
                shouldJump = true;
            }
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
