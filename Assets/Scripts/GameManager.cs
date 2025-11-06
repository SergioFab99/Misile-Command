using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }

    [Header("City Management")]
    [SerializeField] private List<City> cities = new List<City>();

    public event System.Action<int> OnLevelUp;
    public event System.Action OnGameOver;
    public event System.Action<City> OnCityDestroyed;

    public int Level { get; private set; } = 1;
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        I = this;
    }

    public List<City> GetActiveCities()
    {
        return cities.Where(c => c != null && c.IsAlive).ToList();
    }

    public void RegisterCity(City c)
    {
        if (!cities.Contains(c))
            cities.Add(c);
    }

    public void NotifyCityDestroyed(City c)
    {
        if (IsGameOver || I == null) return;
        OnCityDestroyed?.Invoke(c);
        if (GetActiveCities().Count == 0)
            EndGame();
    }

    public void NextLevel()
    {
        if (IsGameOver || I == null) return;
        Level++;
        OnLevelUp?.Invoke(Level);
    }

    public void EndGame()
    {
        if (IsGameOver || I == null) return;
        IsGameOver = true;
        OnGameOver?.Invoke();

        if (Application.isPlaying)
        {
            StartCoroutine(LoadGameOverScene());
        }
    }

    private System.Collections.IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f);

        if (I != null)
        {
            SceneManager.LoadScene("Derrota");
        }
    }
}
