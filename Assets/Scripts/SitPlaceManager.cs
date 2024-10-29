using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SitPlaceManager
{
    public class SitPlaceGameplayInfo
    {
        public bool IsFree => human == null;

        public SitPlace place;
        public HumanProvider human;
    }

    private readonly List<SitPlaceGameplayInfo> _placesInfo = new List<SitPlaceGameplayInfo>();

    public void Init()
    {
        var places = GameObject.FindObjectsByType<SitPlace>(FindObjectsSortMode.InstanceID);

        foreach(var place in places)
        {
            _placesInfo.Add(new SitPlaceGameplayInfo()
            {
                place = place,
                human = null
            });
        }
    }

    public bool TryGetAvaialableSitPlace(out SitPlaceGameplayInfo sitPlace)
    {
        sitPlace = GetRandomAvailableSitPlace();
        return sitPlace != null;
    }

    public SitPlaceGameplayInfo GetRandomAvailableSitPlace()
    {
        var availablePlaces = _placesInfo.Where(p => p.IsFree).ToArray();

        if(availablePlaces.Length == 0) return null;

        var index = UnityEngine.Random.Range(0, availablePlaces.Length);
        return availablePlaces[index];
    }

    public void TakeSitPlace(SitPlaceGameplayInfo sitPlaceInfo, HumanProvider human)
    {
        sitPlaceInfo.human = human;
    }

    public SitPlaceGameplayInfo FreeSitPlace(HumanProvider human)
    {
        var nedeedPlaceInfo =  _placesInfo.Where(info => info.human == human).FirstOrDefault();

        if (nedeedPlaceInfo != null)
            nedeedPlaceInfo.human = human;

        return nedeedPlaceInfo;
    }
}
