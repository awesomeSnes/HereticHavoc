using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected int damage = 5;
    [SerializeField] protected GameObject spellSprite;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animator myAnimator;
    [SerializeField] public Sprite spellIcon;
    [SerializeField] AudioSource myAudioSource;
    [SerializeField] AudioClip sfx;
    [SerializeField] protected bool isSeeking;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        
        spellSprite.transform.LookAt(player.transform.position, Vector3.up);
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }

    public void PlaySoundEffect()
    {
        myAudioSource.Play();
    }
}
