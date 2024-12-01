using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common.Localisation;
using System;

public class UiQuestCell : MonoBehaviour
{
    public QuestRuntimeInfo RuntimeInfo { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private Image progressFiller;

    [SerializeField] private CanvasGroup finsihedGroup;
    [SerializeField] private CanvasGroup lockGroup;
    [SerializeField] private CanvasGroup openedGroup;

    [SerializeField] private LockEffect lockEffect;

    [SerializeField] private Button openButton;

    [SerializeField] private string progressPattern;

    public void SetContext(QuestRuntimeInfo questnfo, Action onOpenButtonAction)
    {
        RuntimeInfo = questnfo;

        title.SetKey(RuntimeInfo.configData.title);
        description.SetKey(RuntimeInfo.configData.description, RuntimeInfo.configData.maxProgress);

        openButton.onClick.AddListener(() =>
        {
            OnOpenButtonClicked();

            onOpenButtonAction?.Invoke();
        });

        UpdateProgressValue();
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveAllListeners();
    }

    public void UpdateProgressValue()
    {
        progressFiller.fillAmount = RuntimeInfo.progressValue / RuntimeInfo.configData.maxProgress;
        progress.text = string.Format(progressPattern, (int) RuntimeInfo.progressValue, RuntimeInfo.configData.maxProgress);

        finsihedGroup.gameObject.SetActive(RuntimeInfo.isFinished);
        lockGroup.gameObject.SetActive(!RuntimeInfo.isOpened);
        lockEffect.SetIsActive(RuntimeInfo.isFinished);
        openedGroup.gameObject.SetActive(RuntimeInfo.isOpened);

        icon.sprite = RuntimeInfo.isOpened ? RuntimeInfo.VisualInfo.icon : RuntimeInfo.VisualInfo.unknownIcon;
    }

    private void OnOpenButtonClicked()
    {
        RuntimeInfo.isOpened = true;

        UpdateProgressValue();
    }
}
