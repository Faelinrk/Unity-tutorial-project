using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;

    [Space]
    [SerializeField] private Button _newGameButton = null;
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _quitButton = null;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _quitButton.onClick.AddListener(QuitGame);
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OpenSettings()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
