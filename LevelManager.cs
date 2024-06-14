using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadDelay = 2f;

    [SerializeField] private GameObject panelLogin;
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        StartCoroutine(InitDataDropDown());
    }

    private IEnumerator InitDataDropDown()
    {
        yield return new WaitForSeconds(1f);

        if (dropdown == null)
        {
            Debug.LogError("Dropdown is null");
            yield break;
        }

        dropdown.ClearOptions();
        var options = new List<TMP_Dropdown.OptionData>();

        if (ASM_MN.Instance != null && ASM_MN.Instance.listRegion != null)
        {
            foreach (var region in ASM_MN.Instance.listRegion)
            {
                options.Add(new TMP_Dropdown.OptionData(region.Name));
            }
        }
        else
        {
            Debug.LogError("ASM_MN instance or listRegion is null");
        }

        dropdown.AddOptions(options);
    }

    public void Login()
    {
        if (panelLogin == null)
        {
            Debug.LogError("panelLogin is null");
            return;
        }

        panelLogin.SetActive(true);

        if (userName != null)
        {
            userName.text = "";
        }
        else
        {
            Debug.LogError("userName is null");
        }
    }

    public void CloseLogin()
    {
        if (panelLogin != null)
        {
            panelLogin.SetActive(false);
        }
        else
        {
            Debug.LogError("panelLogin is null");
        }
    }

    public void LoadGame()
    {
        if (userName == null || string.IsNullOrWhiteSpace(userName.text))
        {
            Debug.LogWarning("Username is empty or userName is null");
            return;
        }

        if (ScoreKeeper.Instance != null)
        {
            ScoreKeeper.Instance.ResetScore(userName.text, dropdown.value);
        }
        else
        {
            Debug.LogError("ScoreKeeper instance is null");
        }

        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
