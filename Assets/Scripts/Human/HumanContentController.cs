using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanContentController : MonoBehaviour
{
    public Action<GameObject> ContentSpawned;
    [SerializeField] private GameObject[] humans;

    private GameObject _content;

    private void Start()
    {
        var randomHuman = UnityEngine.Random.Range(0, humans.Length);
        _content = Instantiate(humans[randomHuman], transform.position, transform.rotation, transform);

        ContentSpawned?.Invoke(_content);
    }
}
