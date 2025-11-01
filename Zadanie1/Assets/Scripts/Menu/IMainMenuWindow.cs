using UnityEngine;
using UnityEngine.Events;

public abstract class IMainMenuWindow: MonoBehaviour
{
    public UnityAction OnBackCallback;

    public abstract void Open ();
    public abstract void Close ();

}
