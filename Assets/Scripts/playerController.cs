using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //move stuff
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    float moveInput;

    //jump stuff
    bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //animator controller
    private Animator anim;

    //attack variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int attackDamage = 2;

    //health varaibles

    public int maxHealth = 55;
    int currentHealth;
    public Slider healthSlider;
    public ParticleSystem bloodEffect;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;

            anim.SetTrigger("jump");
            
        }

        else if(isGrounded != true)
        {
            anim.SetBool("isJumping", false);
        }

        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }

        else
        {
            anim.SetBool("isRunning", true);
        }

        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

       
    }


    void Attack()
    {
        //play attack animation
        anim.SetTrigger("attack");

        //detect enemies in range of attack
       Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage Enemies

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        bloodEffect.Play();

        if (currentHealth <= 0)
        {
            
            
            SceneManager.LoadScene(0);
        }
    }


     void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
