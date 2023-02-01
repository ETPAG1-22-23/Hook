using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
   // Animator animController;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;

    float jumpForce = 12f;

    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;

    private Vector3 mousePosition;
    Vector2 playerPosition;
    [SerializeField] LayerMask Hookable;

    Vector2 endHook;

    RaycastHit2D hit;

    float distance_Grap;

    [SerializeField]  bool can_grap;
    [SerializeField]  bool touching;
    [SerializeField]  int count;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //animController = GetComponent<Animator>();
        //Debug.Log(Mathf.Lerp(current, target, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
        can_grap = true;
        count = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }

    private void Moving()
    {
        can_grap = false;
        while (touching == false || count < 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, endHook, 0.5f);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {

        playerPosition = transform.position;
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hit = Physics2D.Raycast(playerPosition, mousePosition, 40f, Hookable);

        Debug.DrawRay(playerPosition, mousePosition, Color.green);

        if (Input.GetKey(KeyCode.F) && hit.collider != null && can_grap)
        {
            Moving();
            Debug.Log("Beh");
        }

        horizontal_value = Input.GetAxis("Horizontal");

        if(horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;
        
        //animController.SetFloat("Speed", Mathf.Abs(horizontal_value));
   
        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
            //animController.SetBool("Jumping", true);
        }
    }
    void FixedUpdate()
    {
        if (is_jumping && can_jump)
        {           
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = false;
        }
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        can_jump = true;
        //animController.SetBool("Jumping", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //animController.SetBool("Jumping", false);        
    }
}