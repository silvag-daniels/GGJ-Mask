using System.Collections.Generic;
using UnityEngine;

public class ScaryWindowMechanicManager : MonoBehaviour
{
    public int windowsToWin = 5;
    public float secondsBetweenAttacks = 1.5f;
    public GameObject Door;
    private ScaryWindow[] ScaryWindows = {};
    private bool miniGameStarted = false;
    private bool activeDelay = false;
    private int activeWindowIndex = -1;
    void Start()
    {
        ScaryWindows = FindObjectsByType<ScaryWindow>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if(activeDelay)
        {
            if(secondsBetweenAttacks <= 0)
            {
                activeDelay = false;
                activeWindowIndex = -1;
                secondsBetweenAttacks = 1.5f;
            }
            else
            {
                secondsBetweenAttacks -= Time.deltaTime;
                return;
            }
        }
        if(!miniGameStarted) return;

        if(activeWindowIndex == -1)
        {
            activeWindowIndex = Random.Range(0, ScaryWindows.Length);
            ScaryWindows[activeWindowIndex].SendMessage("Attack");
        }
    }

    void StartMiniGame()
    {
        for(int i = 0; i < ScaryWindows.Length; i++)
        {
            ScaryWindows[i].SendMessage("SetStartMechanic");
        }
        miniGameStarted = true;
    }

    void ScaryWindowFainted()
    {
        activeWindowIndex = -1;
        windowsToWin--;

        if(windowsToWin == 0)
        {
            miniGameStarted = false;
            AudioSource audioSource = Door.GetComponent<AudioSource>();
            Door.SetActive(false);
        }
        else
        {
            activeDelay = true;
        }
    }
}
