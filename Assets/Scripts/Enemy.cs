using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "S";

    public float speed = 1;
    public int health = 10;
    public Sprite[] sprites;

    public GameObject bulletObjA, bulletObjB;
    public float maxShotDelay = 0.2f;
    public float curShotDelay;

    public GameObject player;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay) return;
        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigidbody2d = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigidbody2d.AddForce(dirVec, ForceMode2D.Impulse);
        }
        else if(enemyName == "L") 
        {
            GameObject bulletR = Instantiate(bulletObjA, transform.position+ Vector3.right*0.3f, transform.rotation);
            Rigidbody2D rigidbody2dR = bulletR.GetComponent<Rigidbody2D>();
            GameObject bulletL = Instantiate(bulletObjA, transform.position - Vector3.right * 0.3f, transform.rotation);
            Rigidbody2D rigidbody2dL = bulletL.GetComponent<Rigidbody2D>();
            
            Vector3 dirVec = (player.transform.position - transform.position).normalized;
            rigidbody2dR.AddForce(dirVec, ForceMode2D.Impulse);
            rigidbody2dL.AddForce(dirVec, ForceMode2D.Impulse);
        }
                
        
        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }
}
