using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning("Scene name is empty. Assign a valid scene name in the inspector.");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
