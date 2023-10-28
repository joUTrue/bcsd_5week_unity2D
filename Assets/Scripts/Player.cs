using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletObjA, bulletObjB;
    public float maxShotDelay = 0.2f;
    public float curShotDelay;
    public int power = 1;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft)) h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom)) v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("InputH", (int)h);
        }
    }
    void Fire()
    {
        if ((!Input.GetButton("Fire1")) || (curShotDelay < maxShotDelay)) return;

        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigidbody2d = bullet.GetComponent<Rigidbody2D>();
                rigidbody2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left*0.1f, transform.rotation);
                Rigidbody2D rigidbody2dR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbody2dL = bulletL.GetComponent<Rigidbody2D>();
                rigidbody2dR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbody2dL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.2f, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                Rigidbody2D rigidbody2dRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbody2dLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbody2dCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidbody2dRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbody2dLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbody2dCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    break;
                case "right":
                    isTouchRight = true;
                    break;
                case "left":
                    isTouchLeft = true;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchTop = false;
                    break;
                case "bottom":
                    isTouchBottom = false;
                    break;
                case "right":
                    isTouchRight = false;
                    break;
                case "left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
