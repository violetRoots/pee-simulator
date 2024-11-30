using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatabaseManager : SingletonFromResourcesBase<DatabaseManager>
{
    [SerializeField] private SpriteDatabase spriteDatabase;

    public Sprite GetSprite(string guid)
    {
        var res = spriteDatabase.objects.Where(kvp => kvp.Key == guid).FirstOrDefault();

        if (res == null) return null;

        return res.Value;
    }
}
