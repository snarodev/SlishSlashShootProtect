using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class PlayAudio : MonoBehaviour
{
    FlowController flowController;

    private void Start()
    {
        flowController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlowController>();
    }

    public void PlayAudioEffect()
    {
        if (flowController.flow)
            return;

        GetComponent<AudioSource>().Play();
    }
}
