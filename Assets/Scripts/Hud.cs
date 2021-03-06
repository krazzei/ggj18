﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class Hud : MonoBehaviour
{
	/// <summary>
	/// Dont assign this outside this class!
	/// </summary>
	public static Hud instance;

	struct SliderInfo
	{
		public Transform transform;
		public Slider slider;
		public float duration;
		public float startTime;
		public float currentTime;
	}

	public Slider cooldownBar;
	public Slider HealthBar;
	public Slider Dashbar;
	public Text dead;
	public Image deadBkg;

	private List<SliderInfo> _info;
	private Vector2 _uiOffset;

    private void Awake()
    {
		instance = this;
		_info = new List<SliderInfo>();
		dead.enabled = false;
		deadBkg.enabled = false;

		DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var playerHealth = GameMode.instance.playerObject.GetComponent<Health>();
		playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    public void MakeCooldownBar(Transform trans, float duration)
	{
		Debug.Log("Making cooldown bar");
		var cdBar = Instantiate(cooldownBar);
		cdBar.transform.SetParent(transform, false);

		var info = new SliderInfo
		{
			transform = trans,
			slider = cdBar,
			duration = duration,
			startTime = Time.time,
			currentTime = Time.time
		};

		_info.Add(info);
	}

	public void UpdateDashCooldown(float current, float duration)
	{
		Dashbar.value = current / duration;
	}

	public void Dead()
	{
		dead.enabled = true;
		deadBkg.enabled = true;
		StartCoroutine(DeadRoutine());
	}

	private IEnumerator DeadRoutine()
	{
		var time = 0.0f;
		var color = deadBkg.color;
		while (time < 5f)
		{
			color.a = Mathf.Lerp(0, 1, time / 5);
			deadBkg.color = color;
			time += Time.deltaTime;
			yield return null;
		}

		SceneManager.LoadScene("MainMenu");
		Destroy(gameObject);
	}

	private void UpdateHealthBar(float current, float max)
	{
		HealthBar.value = current / max;
	}

    private void Update()
    {
		for (var i = _info.Count - 1; i > -1; --i)
		{
			Debug.Log("Updating bar: " + i);
			var mod = _info[i];
			mod.currentTime += Time.deltaTime;
			if (mod.currentTime > mod.startTime + mod.duration)
			{
				//Destroy(mod.slider.gameObject);
				//_info.Remove(mod);
				//continue;
			}
			//var rect = mod.slider.GetComponent<RectTransform>();
			//var worldView = Camera.main.WorldToViewportPoint(mod.transform.position);
			//var screenSpace = new Vector2(
			//	((worldView.x * rect.sizeDelta.x) - (rect.sizeDelta.x * 0.5f)),
			//	((worldView.y * rect.sizeDelta.y) - (rect.sizeDelta.y * 0.5f)));

			//rect.anchoredPosition = screenSpace;
			mod.slider.transform.position = transform.position;

			mod.slider.value = mod.currentTime - mod.startTime / mod.duration;
			_info[i] = mod;
		}
    }
}
