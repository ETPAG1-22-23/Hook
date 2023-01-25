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


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log(playerPosition.x + " " + playerPosition.y);

        hit = Physics2D.Raycast(playerPosition, mousePosition, Mathf.Infinity, Hookable);

        if (hit.collider != null)
        {
            endHook = hit.point;
        }

        if (Input.GetKey(KeyCode.F))
        {
            playerR.gravityScale = 0;
            playerR.transform.Translate(mousePosition * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            playerR.gravityScale = 1;
        }
        
    }
}
