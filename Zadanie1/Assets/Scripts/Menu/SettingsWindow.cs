using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : IMainMenuWindow
{
    private const float FadeTime = 0.2f;

    [SerializeField] Button backButton;
    [SerializeField] CanvasGroup mainCanvasGroup;

    private bool opened = false;

    private void Start ()
    {
        if (!CheckDependencies ())
        {
            this.enabled = false;
            return;
        }
    }

    private void OnEnable ()
    {
        backButton.Select ();
        backButton.onClick.AddListener (OnBackCallback);
    }

    private void OnDisable ()
    {
        backButton.onClick.RemoveAllListeners ();
    }

    public override void Open ()
    {
        if (opened) return;

        opened = true;
        gameObject.SetActive (true);
        mainCanvasGroup.DOFade (1, FadeTime);
    }

    public override void Close ()
    {
        if (!opened) return;

        mainCanvasGroup.DOFade (0, FadeTime).onComplete += () =>
        {
            opened = false;
            gameObject.SetActive (false);
        };
    }

    private bool CheckDependencies ()
    {
        bool result = true;

        if (backButton == null)
        {
            Debug.LogError ("backButton in SettingsWindow is NULL!");
            result = false;
        }
        if (mainCanvasGroup == null)
        {
            Debug.LogError ("mainCanvasGroup in SettingsWindow is NULL!");
            result = false;
        }
        return result;
    }
}
