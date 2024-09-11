using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrkSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            source.Play();
        }
    }
}
