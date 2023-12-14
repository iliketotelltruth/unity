using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class move : MonoBehaviour
{   private Rigidbody2D  Rd;
    [Header("jumpSet")]
    [SerializeField] float jumpSpeed=70f;
    public Transform jumpPoint;
    public Transform wallpoint;
    private bool onFloor=true;
    public bool facingright = true;
    [Header("runset")]
    [SerializeField] float moveSpeed = 8f;
    [Header("wallhit")]
    private bool hitwall = false;
    private Vector2 contactNormal = Vector2.zero;
    private float angleWithFloor = 0f;
    private bool hittingwall = false;
    void Start()
    {
        Rd = GetComponent<Rigidbody2D>();
    }
 
    void PointCheck()
    {
        onFloor = Physics2D.OverlapCircle(jumpPoint.position, 0.15f, LayerMask.GetMask("floor & wall"));
        //撞牆
        hitwall = Physics2D.OverlapCircle(wallpoint.position, 0.15f, LayerMask.GetMask("floor & wall"));
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        PointCheck();
        WallHit();
        Debug.Log(hittingwall);
    }
    private void FixedUpdate()
    {
        howMove();
        jump();
    }
    void Flip()
    {
        facingright = !facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void howMove()
    {
        if (!Input.GetButton("Jump") && onFloor)
        {
            Rd.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), Rd.velocity.y);
        }
        if (Input.GetAxis("Horizontal") != 0f)
        {
            if((facingright && Input.GetAxis("Horizontal")<0f)||(!facingright && Input.GetAxis("Horizontal") > 0f))
            {
                Flip();
            }
        }
    }
    float Times=0f;
    float minTimes = 1f;
    public float maxTimes = 10f;
    void jump()
    {
        if (Input.GetButton("Jump") && onFloor)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if(Times < maxTimes)
                {
                Times += Time.deltaTime * 10f;
                }
            else
            {
                Times = maxTimes;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color (83,158,209,255);
            if (Times > 0f)
            {
                Times += minTimes;
                if (facingright && Input.GetButton("Right"))
                {
                    Rd.AddForce(Vector2.up * jumpSpeed * Times);
                    Rd.AddForce(Vector2.right * jumpSpeed * Times / 2);
                }
                else if (!facingright && Input.GetButton("Left"))
                {
                    Rd.AddForce(Vector2.up * jumpSpeed * Times);
                    Rd.AddForce(Vector2.left * jumpSpeed*Times/2);
                }
                else
                {
                    Rd.AddForce(Vector2.up * jumpSpeed * Times);
                }
                Times = 0f;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("floor & wall"))
        {
            contactNormal = collision.GetContact(0).normal;
            angleWithFloor = (Mathf.Atan(contactNormal.y/contactNormal.x))*180f/Mathf.PI;
        }
    }
    void WallHit ()
    {
        if (!hittingwall)
        {
            if (hitwall && Mathf.Abs(angleWithFloor) < 35f && !onFloor)
            {
                hittingwall = true;
            }
        }
        else
        {
            if (!hitwall && onFloor)
            {
                hittingwall = false;
            }
        }
    }
}
