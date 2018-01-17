using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [HideInInspector]
    private float masterVolume = 0.5f;

    private GameObject[] sounds;

    // Use this for initialization
    void Start()
    {
        //TODO: Get music audio
        sounds = GameObject.FindGameObjectsWithTag("Sound");

        //Make sure sounds don't get destroyed since there's only one track right now.
        foreach(GameObject soundObject in sounds)
        {
            DontDestroyOnLoad(soundObject);
        }
    }

    void Update()
    {
        //TODO: Set audio source volume to master volume setting
    }
    
    //Called to change the volume of the game
    public void UpdateVolume(float newVolume)
    {
        masterVolume = newVolume;

        foreach(GameObject soundObject in sounds)
        {
            soundObject.GetComponent<AudioSource>().volume = masterVolume;
        }
    }
}
