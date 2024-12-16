using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CircleItemConfig ItemConfig => circleItemConfig;

    [SerializeField] private bool activateOnWeaponEnabled = true;
    [SerializeField] private CircleItemConfig circleItemConfig;

    [Space]
    [SerializeField] private BasePeePart[] peeParts;
    [SerializeField] private GameObject content;

    private float _enableTime;

    private void Awake()
    {
        content.SetActive(!activateOnWeaponEnabled);
    }

    public void Activate()
    {
        foreach (var part in peeParts)
            part.Activate();
    }

    public void Deactivate()
    {
        foreach (var part in peeParts)
            part.Deactivate();
    }

    public void Enable()
    {
        StopAllCoroutines();

        _enableTime = Time.time;
        content.SetActive(activateOnWeaponEnabled);

        StartCoroutine(DisableProcess());
    }

    private IEnumerator DisableProcess()
    {
        while (Time.time - _enableTime < circleItemConfig.duration)
        {
            yield return new WaitForSeconds(0.1f);
        }

        content.SetActive(!activateOnWeaponEnabled);
    }
}
