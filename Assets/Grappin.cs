using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappin : MonoBehaviour
{
    public float GrappleSpeed = 10f;
    public float MaxDistance = 10f;
    public LayerMask Hookable;
    Rigidbody2D rb;
    public Camera cam;

    public LineRenderer lr;

    [SerializeField] bool IsGrappling;
    private Vector2 GrapplePoint;
    private Vector2 GrappleDirection;

    private Vector2 mousePosition;
    Vector2 playerPosition;
    RaycastHit2D hit;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsGrappling)
        {
            playerPosition = transform.position;
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            GrappleDirection = (GrapplePoint - playerPosition).normalized;

            hit = Physics2D.Raycast(playerPosition, GrappleDirection , MaxDistance, Hookable);
            if (hit.collider != null)
            {
                GrapplePoint = hit.point;
                IsGrappling = true;

            }

        }

        if (Input.GetMouseButtonUp(0) &&  IsGrappling == true)
        {
            IsGrappling = false;
        }


        if (IsGrappling == true)
        {
            float DistanceToPoint = Vector2.Distance(playerPosition, GrapplePoint);

            if (DistanceToPoint< 0.01f)
            {
                rb.velocity= Vector2.zero;
            }
            else
            {
                //rb.MovePosition(rb.position + GrappleDirection * GrappleSpeed * Time.deltaTime);

                rb.MovePosition(Vector2.MoveTowards(playerPosition, GrapplePoint,GrappleSpeed * Time.deltaTime));
                
            
            //playerPosition = Vector2.Lerp(playerPosition, GrapplePoint, Time.deltaTime * GrappleSpeed);
             }
        }
    }
}
