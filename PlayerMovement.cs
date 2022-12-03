using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
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
         
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
