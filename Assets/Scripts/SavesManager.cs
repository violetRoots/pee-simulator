using UnityEngine;

public class SavesManager : SingletonMonoBehaviourBase<SavesManager>
{
    public XmlVariable<PlayerStats> PlayerStats {  get; private set; }

    private void Awake()
    {
        PlayerStats = new XmlVariable<PlayerStats>(Application.persistentDataPath, nameof(PlayerStats));
    }
}
