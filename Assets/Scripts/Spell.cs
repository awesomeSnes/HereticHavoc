using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected int damage = 5;
    [SerializeField] protected GameObject spellSprite;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animator myAnimator;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        spellSprite.transform.LookAt(player.transform.position, Vector3.up);
    }
}
