using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
	public GameObject mainMenu;
	public GameObject options;

	public Dropdown screenResolutions;
	public Toggle fullScreen;
	public Toggle controller;

	private List<Dropdown.OptionData> _resolutionOptions;
	private Dictionary<Dropdown.OptionData, Resolution> _resolutions;

    private void Awake()
    {
        ActivateMainMenu();
		_resolutionOptions = new List<Dropdown.OptionData>();
		_resolutions = new Dictionary<Dropdown.OptionData, Resolution>();
		foreach (var r in Screen.resolutions)
		{
			var opData = new Dropdown.OptionData();
			opData.text = string.Format("{0} x {1}", r.width, r.height);

			_resolutionOptions.Add(opData);
			_resolutions.Add(opData, r);
		}
		screenResolutions.AddOptions(_resolutionOptions);
		fullScreen.isOn = Screen.fullScreen;
		controller.isOn = PlayerPrefs.GetInt("Controller", 0) == 0 ? false : true;
    }

    public void LoadLevel()
	{
		SceneManager.LoadScene("Dungeon 1(NotBroke)");
	}

	public void ActivateOptions()
	{
		mainMenu.SetActive(false);
		options.SetActive(true);
	}

	public void ActivateMainMenu()
	{
		mainMenu.SetActive(true);
		options.SetActive(false);
	}

	public void SetFullScreen()
	{
		Debug.Log(fullScreen.isOn);
		Screen.fullScreen = fullScreen;
	}

	public void SetController()
	{
		Debug.Log(controller.isOn);
		PlayerPrefs.SetInt("Controller", controller.isOn ? 1 : 0);
	}

	public void SetResolution()
	{
		Debug.Log(screenResolutions.value);
		var res = _resolutions[_resolutionOptions[screenResolutions.value]];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}
}
