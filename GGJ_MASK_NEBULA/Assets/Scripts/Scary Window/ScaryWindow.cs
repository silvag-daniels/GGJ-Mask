using System.Collections.Generic;
using UnityEngine;

public class ScaryWindow : MonoBehaviour
{
    public Sprite onAttackSprite;
    public Sprite IdleSprite;
    public GameObject ScaryWindowMechanicManager;
    public float secondsToAttack = 20f;
    public float secondsBetweenAudio = 5f;
    private float audioTimer = 0f;
    private float attackTimer = 0f;
    private bool startMechanic = false;
    private bool isAttacking = false;
    private AudioSource audioSource;
    private List<Vector3> possiblePos = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SetStartMechanic(){
        startMechanic = true;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (Transform child in transform)
        {
            possiblePos.Add(child.position);
        }
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
                audioSource.Play();
                Debug.Log("Audio");
                audioTimer = 0f;

                if (!isAttacking)
                {
                    isAttacking = true;
                    attackTimer = 0f;
                    this.GetComponent<SpriteRenderer>().sprite = onAttackSprite;
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
                    //FindFirstObjectByType<ScaryWindowEndGame>().PlayScreamer();

                }
            }
            
        }
    }

    void Attack()
    {
        isAttacking = true;
        this.GetComponent<SpriteRenderer>().sprite = onAttackSprite;
        audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BasePlayer" && isAttacking)
        {
            isAttacking = false;
            attackTimer = 0f;
            audioTimer = 0f;
            this.GetComponent<SpriteRenderer>().sprite = IdleSprite;
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
