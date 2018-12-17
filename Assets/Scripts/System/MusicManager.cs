using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musics;

    int cptMusic = 0;

    AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        musicSource.clip = musics[cptMusic];
        musicSource.Play();
        cptMusic++;
    }

    // Update is called once per frame
    void Update()
    {
        if(!musicSource.isPlaying)
        {
            musicSource.clip = musics[cptMusic];
            musicSource.Play();

            if (cptMusic >= musics.Length - 1)
                cptMusic = 0;
            else
                cptMusic++;
        }
    }
}
