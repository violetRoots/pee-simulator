using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Serializable]
    public class PeeOriginInfo
    {
        public PeeGenerator generator;
        public float registeredTime;
    }

    [SerializeField] private int neededPeeOriginsCount = 3;
    [SerializeField] private float peeCooldown = 1.0f;
    [SerializeField] private float addMoneyCooldown = 1.0f;
    [SerializeField] private float moneyMultiplier = 1.0f;

    [Space]
    [SerializeField] private AnimationClip idleAnimationClip;
    [SerializeField] private AnimationClip celebrationAnimationClip;
    [SerializeField] private Animation humanAnimation;

    [SerializeField] private NeedPeePanel needPeePanel;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material peeMaterial;

    [SerializeField] private Renderer[] renderers;

    private GameManager _gameManager;

    private List<PeeOriginInfo> _peeOriginInfos = new List<PeeOriginInfo>();

    private float _lastAddMoneyTime;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        UpdateOriginInfos();
        UpdateNeedPeePanel();
        UpdateMoney();
        UpdateAnimation();

        if(_peeOriginInfos.Count == 0)
            SetMaterials(defaultMaterial);
        else
            SetMaterials(peeMaterial);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out PeeBox peeBox)) return;

        if (_peeOriginInfos.Any(info => info.generator == peeBox.Generator)) return;

        _peeOriginInfos.Add( new PeeOriginInfo 
        {  
            generator = peeBox.Generator, 
            registeredTime = Time.time 
        });
    }

    private void UpdateOriginInfos()
    {
        _peeOriginInfos = _peeOriginInfos.Where(info => Time.time - info.registeredTime < peeCooldown).ToList();
    }

    private void UpdateNeedPeePanel()
    {
        needPeePanel.SetCount(neededPeeOriginsCount - _peeOriginInfos.Count);
    }

    private void UpdateMoney()
    {
        if (_peeOriginInfos.Count < neededPeeOriginsCount) return;

        if (Time.time - _lastAddMoneyTime < addMoneyCooldown) return;

        _gameManager.Data.SetMoney((int)(_gameManager.Data.Money + (1 * moneyMultiplier)));

        _lastAddMoneyTime = Time.time;
    }

    private void SetMaterials(Material material)
    {
        foreach(var renderer in renderers)
        {
            renderer.material = material;
        }
    }

    private void UpdateAnimation()
    {
        //humanAnimation.Stop();
        

        //if (_peeOriginInfos.Count < neededPeeOriginsCount)
        //{
        //    humanAnimation.clip = idleAnimationClip;
        //}
        //else
        //{
        //    humanAnimation.clip = celebrationAnimationClip;
        //}

        //humanAnimation.Play();
    }
}
