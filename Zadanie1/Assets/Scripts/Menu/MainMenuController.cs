using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private const float FadeTime = 0.2f;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private SettingsWindow settingsWindow;

    private Stack<IMainMenuWindow> windowsStack = new Stack<IMainMenuWindow>();

    private void Start ()
    {
        if (!CheckDependencies ())
        {
            this.enabled = false;
            return;
        }

        playButton.onClick.AddListener (OnPlayClicked);
        settingsButton.onClick.AddListener (OnSettingsClicked); 
        exitButton.onClick.AddListener (OnExitClicked);

        settingsWindow.OnBackCallback += OnBackClicked;
        playButton.Select ();
    }

    private void OnDestroy ()
    {
        settingsWindow.OnBackCallback -= OnBackClicked;
    }

    private void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnBackClicked ();
        }
    }

    private void OnPlayClicked ()
    {
        SceneManager.LoadScene ("Gameplay");
    }

    private void OnSettingsClicked ()
    {
        OpenWindow (settingsWindow);
    }

    private void OnExitClicked ()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
    private void OpenWindow(IMainMenuWindow window)
    {
        if(windowsStack.Count == 0)
        {
            mainMenuCanvasGroup.DOFade (0, FadeTime).onComplete += () =>
            {
                mainMenuCanvasGroup.gameObject.SetActive (false);
            };
        }

        windowsStack.Push (window);
        window.Open ();
    }

    private void OnBackClicked ()
    {
        if (windowsStack.Count == 0) return;

        windowsStack.Peek ().Close ();
        windowsStack.Pop ();

        if (windowsStack.Count == 0)
        {
            mainMenuCanvasGroup.gameObject.SetActive(true);
            mainMenuCanvasGroup.DOFade (1, FadeTime);
            playButton.Select ();
        }
    }

    private bool CheckDependencies ()
    {
        bool result = true;

        if(playButton == null)
        {
            Debug.LogError ("PlayButton in MainMenuController is NULL!");
            result = false;
        }
        if(settingsButton == null)
        {
            Debug.LogError ("SettingsButton in MainMenuController is NULL!");
            result = false;
        }
        if (exitButton == null)
        {
            Debug.LogError ("ExitButton in MainMenuController is NULL!");
            result = false;
        }
        if (mainMenuCanvasGroup == null)
        {
            Debug.LogError ("MainMenuCanvasGroup in MainMenuController is NULL!");
            result = false;
        }
        if (settingsWindow == null)
        {
            Debug.LogError ("SettingsWindow in MainMenuController is NULL!");
            result = false;
        }
        return result;
    }
}
