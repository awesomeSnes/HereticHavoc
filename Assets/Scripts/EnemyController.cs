using UnityEngine;
using System;
using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemySprite;
    [SerializeField] float moveSpeed;
    [SerializeField] int health = 10;
    [SerializeField] int damage;
    [SerializeField] float damageTime;
    [SerializeField] float attackTime;
    [SerializeField] Color damageColor;

    private bool takingDamage = false;
    private bool attacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!(takingDamage || attacking)){
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
        transform.LookAt(player.transform.position, Vector3.up);
    }

    public void Hurt(int damage)
    {
        if(takingDamage){return;}
        health -= damage;
        StartCoroutine(TakeDamage());
    }

    IEnumerator TakeDamage()
    {
        enemySprite.GetComponent<SpriteRenderer>().color = damageColor;
        takingDamage = true;
        yield return new WaitForSeconds(damageTime);
        takingDamage = false;
        enemySprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        if(health <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null && !attacking)
        {
            Debug.Log("Its a player");
            attacking = true;
            other.gameObject.GetComponent<PlayerController>().Hurt(damage);
            StartCoroutine(DealDamage());
        }
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }
    
}
