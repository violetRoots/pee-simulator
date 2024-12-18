using Common.Localisation;
using UnityEngine;
using UnityEngine.UI;

public class UiShopSuppliersCell : MonoBehaviour
{
    public SupplierRuntimeInfo RuntimeInfo { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro price;
    [SerializeField] private Button openButton;

    [SerializeField] private string pricePattern;

    private PlayerStats _playerStats;
    private SuppliersManager _suppliersManager;

    private void Awake()
    {
        _playerStats = SavesManager.Instance.PlayerStats.Value;
        _suppliersManager = GameManager.Instance.SuppliersManager;
    }

    private void OnEnable()
    {
        openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        openButton.onClick.RemoveAllListeners();
    }

    public void SetContext(SupplierRuntimeInfo info)
    {
        RuntimeInfo = info;

        icon.sprite = RuntimeInfo.VisualInfo.unknownIcon;
        price.SetKey(pricePattern, RuntimeInfo.configData.suppliersShopPrice);
    }

    private void OnOpenButtonClicked()
    {
        if (_playerStats.money < RuntimeInfo.configData.suppliersShopPrice) return;

        _playerStats.ChangeMoney(-RuntimeInfo.configData.suppliersShopPrice);

        _suppliersManager.SetAvailableByShop(RuntimeInfo);

        AudioManager.StaticPlaySound(SfxType.ItemBought, 0.075f);
    }
}
