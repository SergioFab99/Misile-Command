using UnityEngine;
using System;

public class EventBus : MonoBehaviour
{
    public static EventBus I { get; private set; }

    public event Action<Vector3> OnPlayerClick;
    public event Action<int> OnAmmoChanged;
    public event Action<string> OnMessage;

    private void Awake()
    {
        I = this;
    }

    public void PlayerClicked(Vector3 pos) => OnPlayerClick?.Invoke(pos);
    public void AmmoChanged(int a) => OnAmmoChanged?.Invoke(a);
    public void Message(string msg) => OnMessage?.Invoke(msg);
}
