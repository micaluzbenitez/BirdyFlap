using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject wing;
    [SerializeField] private float wingSpeed;

    public bool alive = true;

    private Rigidbody2D rb;
    private float minAngle = 0;
    private float maxAngle = 20;
    private float angle = 0;
    private bool direction = false; //false baja - true sube
    private Vector3 pos;

    public static Action onPlayerCollision;
    public static Action onGrabCoin;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            alive = false;
            onPlayerCollision?.Invoke();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            Debug.Log("Colision con un obstaculo.");
        }
        if(collision.gameObject.layer == 9)
        {
            onGrabCoin?.Invoke();
            Debug.Log("Colision con una moneda");
        }
    }

    void Update()
    {
        if (!alive) return;
        
        CheckInputs();
        MoveWing();
    }

    void Jump()
    {
        Vector2 force = new Vector2(0, jumpForce);
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Jump();
    }

    void MoveWing()
    {
        if (direction)
        {
            angle += wingSpeed * Time.deltaTime;
            if (angle > maxAngle)
            {
                direction = !direction;
            }
        }
        else
        {
            angle -= wingSpeed * Time.deltaTime;
            if (angle < minAngle)
            {
                direction = !direction;
            }
        }

        wing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void ResetPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        alive = true;
        transform.position = pos;

        Debug.Log("Reseteo de personaje.");
    }
}
