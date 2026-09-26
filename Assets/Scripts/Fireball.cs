using UnityEngine;

public class Fireball : Spell
{
    [SerializeField] float speed;
    
    
    private bool exploding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!exploding)
        {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colliding");
        if(other.gameObject.GetComponent<EnemyController>() != null)
        {
            Debug.Log("Its an enemy");
            other.gameObject.GetComponent<EnemyController>().Hurt(damage);         
        }
        exploding = true;
        myAnimator.SetBool("isExploding", true);
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }

}
