using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    public Transform player;
    //private Rigidbody2D rb;
    //private Vector2 movement;
    public bool facingRight = true;
    public float health;
    public float speed;
    public int damage;
    
    [Header("Weapon")]
    public Transform attackPos;
    public LayerMask playerMask;
    public float radius;

    public float recharge;
    public float startRecharge;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();

        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        recharge += Time.deltaTime;

        //direction.Normalize();
        //movement = direction;
        //if (facingRight == false && direction.x > 0)
        //{
        //    Flip();
        //}
        //else if (facingRight == true && direction.x < 0)
        //{
        //    Flip();
        //}
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (recharge > startRecharge)
        {
            if (other.CompareTag("Player"))
            {
                anim.SetBool("isAttack", true);
                recharge = 0;
            }
        }
        else
        {

        }
    }
    //private void FixedUpdate()
    //{
    //    MoveCharacter(movement);
    //}


    public void OnAttack()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPos.position, radius, playerMask);
        anim.SetBool("isAttack", false);
        for (int i = 0; i < playerCollider.Length; i++)
        {
            playerCollider[i].GetComponent<Player>().TakeDamage(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    //void MoveCharacter(Vector2 direction)
    //{
    //    rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    //}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
}
