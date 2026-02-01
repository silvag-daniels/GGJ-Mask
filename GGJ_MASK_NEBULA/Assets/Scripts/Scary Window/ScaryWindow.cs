using System.Collections.Generic;
using UnityEngine;

public class ScaryWindow : MonoBehaviour
{
    public GameObject PlayerReference;
    public GameObject ScaryWindowMechanicManager;
    public AudioClip killAudio;
    public float secondsToAttack = 20f;
    public float secondsBetweenAudio = 5f;
    private Animator _animator;
    private float audioTimer = 0f;
    private float attackTimer = 0f;
    private bool startMechanic = false;
    private bool isAttacking = false;
    private AudioSource audioSource;
    private AudioClip[] audioClips = {}; // Audios de ventana abierta
    private List<Vector3> possiblePos = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SetStartMechanic(){
        startMechanic = true;
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        foreach (Transform child in transform)
        {
            possiblePos.Add(child.position);
        }
        audioClips = Resources.LoadAll<AudioClip>("Audio/Window/Open");
    }

    // Update is called once per frame
    void Update()
    {
        if (!startMechanic) return;

        if(isAttacking)
        {
            // Timer del audio
            audioTimer += Time.deltaTime;
            if (audioTimer >= secondsBetweenAudio)
            {
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
                Debug.Log("Audio");
                audioTimer = 0f;

                if (!isAttacking)
                {
                    isAttacking = true;
                    attackTimer = 0f;
                    _animator.SetTrigger("change");
                }
            }

            if (isAttacking)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= secondsToAttack)
                {
                    
                    isAttacking = false;
                    attackTimer = 0f;
                    audioTimer = 0f;
                    audioSource.PlayOneShot(killAudio);
                    PlayerReference.SendMessage("Die");
                    //FindFirstObjectByType<ScaryWindowEndGame>().PlayScreamer();

                }
            }
            
        }
    }

    void Attack()
    {
        isAttacking = true;
        _animator.SetTrigger("change");
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BasePlayer" && isAttacking)
        {
            isAttacking = false;
            attackTimer = 0f;
            audioTimer = 0f;
            _animator.SetTrigger("change");
            moveWindow();
            Debug.Log("Fainted");
            ScaryWindowMechanicManager.SendMessage("ScaryWindowFainted");
        }
    }

    private void moveWindow()
    {
        int randomIndex = Random.Range(0, possiblePos.Count);
        transform.position = Vector3.MoveTowards(transform.position, possiblePos[randomIndex], 0.5f);
    }
}
