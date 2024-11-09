using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;

    private AudioSource sfxPlayer;

    public AudioClip uma;
    public AudioClip tabetai;
    public AudioClip nande;
    public AudioClip hya;
    public AudioClip help;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
    }

    public void playSfx(string index)
    {
        switch (index)
        {
            case "uma":
                sfxPlayer.clip = uma;
                sfxPlayer.Play();
                break;
            case "tabetai":
                sfxPlayer.clip = tabetai;
                sfxPlayer.Play();
                break;
            case "help":
                sfxPlayer.clip = help;
                sfxPlayer.Play();
                break;
            case "nande":
                sfxPlayer.clip = nande;
                sfxPlayer.Play();
                break;
        }
        
    }

}
