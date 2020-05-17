using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControls : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        //Get movement from player input
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.AddForce(movement * movementSpeed * Time.fixedDeltaTime);

        //Clamp movement within screen view
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -8.15f, 8.15f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.25f, 4.25f);
        transform.position = clampedPosition;
    }
}
