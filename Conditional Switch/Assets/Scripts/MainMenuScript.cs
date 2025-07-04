/*
Main Menu script
Copyright (C) 2025 Ethan Bayer

This file is part of Conditional Switch.

Conditional Switch is free software: you can redistribute it and/or
modify it under the terms of the GNU General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Conditional Switch is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Toggle fullscreenToggle;
    public Toggle presModeToggle;
    public Slider volumeSlider;
    public TextMeshProUGUI highscoreText;

    public GameObject quitButton;
    public GameObject optionsButton;
    public GameObject creditsButtonWebGL;
    public GameObject creditsBackButtonWebGL;
    public GameObject creditsBackButtonMain;

    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;

    private int currentResolutionIndex = 0;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer) // if in webGL
        {
            bool presentationMode = false;
            presModeToggle.isOn = presentationMode;
            highscoreText.text = "Highscore: 0";

            quitButton.SetActive(false);
            optionsButton.SetActive(false);
            creditsButtonWebGL.SetActive(true);

            creditsBackButtonWebGL.SetActive(true);
            creditsBackButtonMain.SetActive(false);
        } else
        {
            bool prefFullscreen = PlayerPrefs.GetInt("fullscreen", 0) == 1;
            Screen.SetResolution(PlayerPrefs.GetInt("resolutionWidth", Screen.currentResolution.width), PlayerPrefs.GetInt("resolutionHeight", Screen.currentResolution.height), prefFullscreen);

            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {

                string option = resolutions[i].width + " x " + resolutions[i].height + "@" + resolutions[i].refreshRateRatio.value.ToString("0.00") + "hz";
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume", 0));
            volumeSlider.value = PlayerPrefs.GetFloat("volume", 0);

            bool presentationMode = PlayerPrefs.GetInt("presentationMode", 0) == 1;

            if (presentationMode == true)
            {
                presentationMode = false;
                PlayerPrefs.SetInt("presentationMode", 0);
                PlayerPrefs.Save();
            }

            presModeToggle.isOn = presentationMode;
            fullscreenToggle.isOn = prefFullscreen;

            highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);

        PlayerPrefs.SetInt("resolutionWidth", resolution.width);
        PlayerPrefs.SetInt("resolutionHeight", resolution.height);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.Save();
    }

    public void SetPresentationMode(bool isPresentationMode)
    {
        PlayerPrefs.SetInt("presentationMode", isPresentationMode ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
