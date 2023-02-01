using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappingue : MonoBehaviour
{

    private Vector3 mousePosition;
    [SerializeField] private GameObject playerG;
    [SerializeField] private Rigidbody2D playerR;
    [SerializeField] private Transform player;
    Vector2 playerPosition;
    [SerializeField] LayerMask Hookable;

    Vector2 endHook;

    RaycastHit2D hit;

    float distance_Grap;

    bool can_grap;


    // Start is called before the first frame update
    void Start()
    {
        can_grap = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void Moving()
    {
        can_grap = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        hit = Physics2D.Raycast(playerPosition, mousePosition, 40f, Hookable);

        Debug.DrawRay(playerPosition, mousePosition, Color.green);

        if (Input.GetKey(KeyCode.F) && hit.collider != null && can_grap)
        {
            Moving();
            Debug.Log("Beh");
        }
            //Ray ray = new Ray(transform.position, Vector2.right);

            //Debug.Log(playerPosition.x + " " + playerPosition.y);

            /*
            hit = Physics2D.Raycast(playerPosition, mousePosition, 40f, Hookable);

            distance_Grap = Vector2.Distance(playerPosition, endHook);
            Vector2 VecGrab = new Vector2(endHook.x, endHook.y);

            if (Input.GetKey(KeyCode.F) && hit.collider != null)
            {
                Debug.DrawRay(playerPosition, mousePosition, Color.red);
                endHook = hit.point;
                playerR.gravityScale = 0;
                playerR.velocity = Vector2.zero;
                playerR.AddForce(VecGrab, ForceMode2D.Impulse);
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                playerR.gravityScale = 1;
            }
            */

        }
}
