using UnityEngine;

public class Fireball : Spell
{
    [SerializeField] float speed;

    [SerializeField] GameObject target;
    
    private bool exploding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isSeeking)
        {
            target = FindObjectOfType<EnemyController>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!exploding)
        {
            if(isSeeking)
            {
                if(target.transform.position == null){isSeeking = false; return;}
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else{
                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            }
        } else if(canFlicker){
            GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colliding");
        if(other.gameObject.GetComponent<EnemyController>() != null)
        {
            Debug.Log("Its an enemy");
            other.gameObject.GetComponent<EnemyController>().Hurt(damage);  
            if(isPiercing)
            {
                return;
            }       
        }
        exploding = true;
        myAnimator.SetBool("isExploding", true);
    }

    
}
