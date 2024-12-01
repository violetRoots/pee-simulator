using System.Xml.Serialization;

public class SupplierRuntimeInfo
{
    public SupplierConfig.SupplierConfigData configData;
    public bool isAvailable;

    [XmlIgnore]
    public SupplierVisualInfo VisualInfo
    {
        get
        {
            if (_visualInfo == null)
                _visualInfo = DatabaseManager.Instance.GetSupplierVisual(configData.visualId);
            return _visualInfo;
        }
    }
    [XmlIgnore]
    private SupplierVisualInfo _visualInfo;
}