using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] gunSounds;
    public AudioClip meleeSound;

    public AudioSource playerAttack_AudioSource;

    public AudioSource zombieAttack_AudioSource;
    public AudioSource zombie_Rise_AudioSource;
    public AudioSource zombie_Die_AudioSource;
    public AudioSource zombie_Die_Voice_AudioSource;

    public AudioClip zombieRise_Clip, zombieDie_Clip;
    public AudioClip[] zombieAttack_Clip;
    public AudioClip[] zombieDieVoice_Clip;

    public AudioSource fence_Explosion_AudioSource;
    public AudioClip fence_Explosion_Clio;


    private void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GunSound(int index)
    {
        playerAttack_AudioSource.PlayOneShot(gunSounds[index], .8f);
    }

    public void MeleeAttackSound()
    {
        playerAttack_AudioSource.PlayOneShot(meleeSound, 1.0f);
    }

    public void ZombieRiseSound()
    {
        zombie_Rise_AudioSource.PlayOneShot(zombieRise_Clip, 1.0f);
    }

    public void ZombieDieSound()
    {
        zombie_Die_AudioSource.PlayOneShot(zombieDie_Clip, 1.0f);
     AudioSource.PlayClipAtPoint(zombieDieVoice_Clip[Random.Range(0, zombieDieVoice_Clip.Length)], Camera.main.transform.position);
    }

    public void ZombieAttackSound()
    {
        int index = Random.Range(0, zombieAttack_Clip.Length);
        zombieAttack_AudioSource.PlayOneShot(zombieAttack_Clip[index], 1.0f);
    }

    public void FenceExxplosioveSound()
    {
        fence_Explosion_AudioSource.PlayOneShot(fence_Explosion_Clio, 1.0f);
    }
}
