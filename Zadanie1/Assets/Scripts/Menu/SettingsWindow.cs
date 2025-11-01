using DG.Tweening;
using UnityEngine;

public class SettingsWindow : IMainMenuWindow
{
    private const float FadeTime = 0.2f;


    [SerializeField] CanvasGroup mainCanvasGroup;

    private bool opened = false;


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
}
