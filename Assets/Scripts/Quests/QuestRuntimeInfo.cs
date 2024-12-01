using System;
using System.Xml.Serialization;

[Serializable]
public class QuestRuntimeInfo
{
    public QuestConfig.QuestConfigData configData;
    public float progressValue = 0;
    public bool isFinished;
    public bool isOpened;

    [XmlIgnore]
    public SupplierVisualInfo VisualInfo
    {
        get
        {
            if (_visualInfo == null)
                _visualInfo = DatabaseManager.Instance.GetSupplierVisual(configData.supplierVisualId);
            return _visualInfo;
        }
    }
    [XmlIgnore]
    private SupplierVisualInfo _visualInfo;
}
