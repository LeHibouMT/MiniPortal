using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    // Audio
    public AudioSource openingPortalSound;
    public AudioSource takingPortalSound;
    public AudioSource errorSound;

    // Use this for initialization
    void Start()
    {
        openingPortalSound = GetComponents<AudioSource>()[0];
        takingPortalSound = GetComponents<AudioSource>()[1];
        errorSound = GetComponents<AudioSource>()[2];
    }

    public static SFX sfxInstance;

    private void awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
