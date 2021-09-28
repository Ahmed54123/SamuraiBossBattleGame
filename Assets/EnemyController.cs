using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public LayerMask playerLayerMask;
    public float attackRange = 1f;
    public Transform attackPoint;
    public int attackDamage = 5;

    public GameObject victoryMessage;

    void Start()
    {

        currentHealth = maxHealth;
        victoryMessage.SetActive(false);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        //die animation

        //disable the enemy
        GetComponent<CapsuleCollider2D>().enabled = false;
        
        Time.timeScale = 0;
        victoryMessage.SetActive(true);

        Destroy(gameObject);
    }

    public void EnemyAttack()
    {
       
        //detect enemies in range of attack

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayerMask);

        //damage Enemies

        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<playerController>().DamagePlayer(attackDamage);
            
        }
        
    }
}
