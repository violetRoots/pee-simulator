using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanContentController : MonoBehaviour
{
    public HumanType HumanType => _content.HumanType;
    public Collider FightCollider => _content.FightCollider;

    public Action<HumanContent> ContentSpawned;
    [SerializeField] private HumanContent[] humans;

    private HumanContent _content;

    private void Start()
    {
        var randomHuman = UnityEngine.Random.Range(0, humans.Length);
        _content = Instantiate(humans[randomHuman], transform.position, transform.rotation, transform);

        ContentSpawned?.Invoke(_content);

        _content.FightCollider.gameObject.SetActive(false);
    }
}
