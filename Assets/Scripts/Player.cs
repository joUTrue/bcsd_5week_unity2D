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

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft)) h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom)) v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0)*speed*Time.deltaTime;

        transform.position = curPos + nextPos;

        if(Input.GetButtonDown("Horizontal")|| Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("InputH", (int)h);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Border")
        {
            switch(collision.gameObject.name) 
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
