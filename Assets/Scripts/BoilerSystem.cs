using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoilerSystem : MonoBehaviour
{
    [SerializeField] private float peeTickValue = 0.001f;

    [SerializeField] private Boiler[] boilers;

    private Bottle[] _bottles;

    private float _generalSatisfaction;
    private float _generalCausticity;

    private void Update()
    {
        _bottles = GetNotEmptyBoilerBottles();

        CalcucalateGeneralValues();
    }

    public bool HasBottles()
    {
        return GetNotEmptyBoilerBottles().Length > 0;
    }

    private Bottle[] GetNotEmptyBoilerBottles()
    {
        return boilers.Where(boiler => boiler.AttachedBottle != null && !boiler.AttachedBottle.IsEmpty)
                      .Select(boiler => boiler.AttachedBottle)
                      .ToArray();
    }

    private void CalcucalateGeneralValues()
    {
        var sumSatisfaction = 0.0f;
        var sumCausticity = 0.0f;

        foreach (var bottle in _bottles)
        {
            sumSatisfaction += bottle.Supplier.satisfaction;
            sumCausticity += bottle.Supplier.causticity;
        }

        _generalSatisfaction = sumSatisfaction / _bottles.Length;
        _generalCausticity = sumCausticity / _bottles.Length;
    }

    public void TickPee()
    {
        if (_bottles.Length == 0) return;

        var everyBottleTickValue = peeTickValue / _bottles.Length;
        foreach (var bottle in _bottles)
        {
            bottle.PeeTick(everyBottleTickValue);
        }
    }
}
