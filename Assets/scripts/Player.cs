using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float input;
    public int health;

    public Animator anim;
    private Rigidbody2D rb;
    public bool turning = true;

    [Header("Оружие")]
    public Transform attackPos;
    public LayerMask enemy;
    public float radius;
    public int damage;

    private float recharge;
    public float startRecharge;

    //private float timeBtwAttack;
    //public float startBtwAttack;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if (recharge <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("isAttacking", true);
            }
            recharge = startRecharge;
        }
        else
        {
            recharge -= Time.deltaTime;
        }

        if (turning == false & input > 0)
            Flip();
        else if (turning == true & input < 0)
            Flip();
        if (input == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
            anim.SetBool("isRunning", true);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius, enemy);
        anim.SetBool("isAttacking", false);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPos == null)
            return;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Flip()
    {
        turning = !turning;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
