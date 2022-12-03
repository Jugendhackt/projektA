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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    private void Awake()
    {

    }
    private void FixedUpdate()
    {
        if (jump) {
            rb.AddForce(new Vector2(0, jumpForce));
            jumps++;
            jump = false;
        }

        rb.velocity = new Vector2(Speed, rb.velocity.y);

        if(transform.position.y < -20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        }
    }
}
