using UnityEngine;
using TMPro;

public class AmmoUIManager : MonoBehaviour
{
    [SerializeField] private BasicTurret[] turrets;
    [SerializeField] private TextMeshProUGUI[] ammoTexts;

    private void Update()
    {
        if (turrets == null || ammoTexts == null) return;

        for (int i = 0; i < turrets.Length && i < ammoTexts.Length; i++)
        {
            if (turrets[i] != null && ammoTexts[i] != null)
                ammoTexts[i].text = "Balas: " + turrets[i].GetRemainingMissiles();
        }
    }
}
