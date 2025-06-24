using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float speed = 5f;
    public Vector2 movement;

    public Transform Aim;
    bool isWalking = false;

    Vector2 lastMoveDirection = Vector2.zero;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement * speed;

        if (isWalking)
        {
            Vector3 vector3 = new Vector3(movement.x, movement.y, 0);
            vector3 *= -1;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }



    void ProcessInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if ((moveHorizontal == 0 && moveVertical == 0) && (movement != Vector2.zero))
        {
            isWalking = false;
            lastMoveDirection = movement;

            Vector3 vector3 = new Vector3(lastMoveDirection.x, lastMoveDirection.y, 0);
            vector3 *= -1;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);



        }
        else if (moveHorizontal != 0 || moveVertical != 0)
        {
            isWalking = true;
        }



        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Normalize();







    }
    
    void HandleInteraction()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        Vector2 direction = lastMoveDirection;
        
        // Default facing down if no movement yet
        if (direction == Vector2.zero)
        {
            direction = Vector2.down;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f);

        Debug.DrawRay(transform.position, direction * 1.5f, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            Customer customer = hit.collider.GetComponent<Customer>();
            if (customer != null)
            {
                customer.TakeOrder();
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }
}










}
