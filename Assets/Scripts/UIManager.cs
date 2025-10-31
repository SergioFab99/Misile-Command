using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text levelText;

    private void OnEnable()
    {
        EventBus.I.OnAmmoChanged += UpdateAmmo;
        EventBus.I.OnMessage += ShowMsg;
        GameManager.I.OnLevelUp += UpdateLevel;
        GameManager.I.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        EventBus.I.OnAmmoChanged -= UpdateAmmo;
        EventBus.I.OnMessage -= ShowMsg;
        GameManager.I.OnLevelUp -= UpdateLevel;
        GameManager.I.OnGameOver -= GameOver;
    }

    private void UpdateAmmo(int a) => ammoText.text = $"Ammo: {a}";
    private void ShowMsg(string m) => messageText.text = m;
    private void UpdateLevel(int l) => levelText.text = $"Level {l}";
    private void GameOver() => messageText.text = "GAME OVER";
}
