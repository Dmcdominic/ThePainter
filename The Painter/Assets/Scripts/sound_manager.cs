using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
    public AudioSource footstep1;
    public AudioSource footstep2;
    public AudioSource painting_sound;
    public AudioSource turpentine_sound;
    public AudioSource galleria;
    public AudioSource putting_out_fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFootstep1()
    {
        footstep1.Play();
    }

    public void playFootstep2()
    {
        footstep2.Play();
    }

    public void playPaintingSound()
    {
        painting_sound.Play();
    }

    public void playTurpentineSound()
    {
        turpentine_sound.Play();
    }

    public void playGalleria()
    {
        galleria.Play();
    }

    public void playPuttingOutFire()
    {
        putting_out_fire.Play();
    }

}
