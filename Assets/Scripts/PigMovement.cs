using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PigMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 75f;

    private float vInput;
    private float hInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * turnSpeed;
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        //transform.Rotate(Vector3.up, hInput * turnSpeed * Time.deltaTime);

        //Vector3 move = transform.forward * vInput * moveSpeed * Time.deltaTime;
        //rb.MovePosition(rb.position + move);
        //transform.Translate(move);
    }

    void FixedUpdate()
    {
        /*
        transform.Rotate(Vector3.up, hInput * turnSpeed * Time.fixedDeltaTime);

        Vector3 move = transform.forward * vInput * moveSpeed * Time.deltaTime;
        //rb.MovePosition(rb.position + move);
        transform.Translate(move, Space.World);
        */
    }
}