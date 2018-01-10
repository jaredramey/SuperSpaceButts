using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [HideInInspector]
    private float masterVolume = 0.5f;

    // Use this for initialization
    void Start()
    {
        //TODO: Get music audio
    }

    void Update()
    {
        //TODO: Set audio source volume to master volume setting
    }
    
    //Called to change the volume of the game
    public void UpdateVolume(float newVolume)
    {
        masterVolume = newVolume;
        Debug.Log("Master volume now set to " + masterVolume);
    }
}
