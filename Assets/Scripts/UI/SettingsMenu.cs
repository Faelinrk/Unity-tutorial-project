using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;

    [SerializeField] private Button _closeSettingsButton = null;
    [SerializeField] private Toggle _muteToggle = null;
    [SerializeField] private Slider _volumeSlider = null;
    private bool _mute = false;


    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _closeSettingsButton.onClick.AddListener(CloseSettings);
        _muteToggle.onValueChanged.AddListener(MuteGame);
    }
    private void CloseSettings()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    private void MuteGame(bool value)
    {
        GameSettings.instance.Mute = value;
    }

    private void ChangeVolume(float value)
    {
        GameSettings.instance.Volume = value;
    }
}
