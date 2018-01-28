﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	private List<SliderInfo> _info;
	private Vector2 _uiOffset;
	private Canvas _canvas;

    private void Awake()
    {
		instance = this;
		_info = new List<SliderInfo>();
		_canvas = GetComponent<Canvas>();
    }

    public void MakeCooldownBar(Transform trans, float duration)
	{
		Debug.Log("Making cooldown bar");
		var cdBar = Instantiate(cooldownBar);
		//cdBar.gameObject.transform.SetParent(trans, false);
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
