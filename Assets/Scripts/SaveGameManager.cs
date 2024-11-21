using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : SingletonMonoBehaviourBase<SaveGameManager>
{
    private void Awake()
    {
        var saveDirectotyPath = Application.persistentDataPath;

        if (!Directory.Exists(saveDirectotyPath))
            Directory.CreateDirectory(saveDirectotyPath);

        var config = new FBPPConfig()
        {
            SaveFileName = "saves.sav",
            AutoSaveData = true,
            EncryptionSecret = "pee",
            SaveFilePath = saveDirectotyPath,
            ScrambleSaveData = false
        };

        FBPP.Start(config);
    }

    public int DaysCount 
    {
        get
        {
            if (FBPP.HasKey(nameof(DaysCount)))
                _daysCount = FBPP.GetInt(nameof(DaysCount));
            return _daysCount;
        }
        set
        {
            FBPP.SetInt(nameof(DaysCount), value);
            _daysCount = value;
        }
    }
    private int _daysCount = 0;

    public int Money
    {
        get
        {
            if (FBPP.HasKey(nameof(Money)))
                _moneyCount = FBPP.GetInt(nameof(Money));
            return _moneyCount;
        }
        set
        {
            FBPP.SetInt(nameof(Money), value);
            _moneyCount = value;
        }
    }
    private int _moneyCount = 0;

    public void ClearSaves()
    {
        FBPP.DeleteAll();
    }
}
