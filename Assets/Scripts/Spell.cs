using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] public int damage = 5;
    [SerializeField] public GameObject spellSprite;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animator myAnimator;
    [SerializeField] public Sprite spellIcon;
    [SerializeField] AudioSource myAudioSource;
    [SerializeField] AudioClip sfx;
    [SerializeField] public bool isSeeking;
    [SerializeField] public bool isPiercing;
    [SerializeField] public bool canFlicker;

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
