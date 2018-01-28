using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LevelMusic : MonoBehaviour 
{
	public AudioClip intro;
	public AudioClip loop;

	private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
		_source.clip = intro;
        _source.Play();

		yield return new WaitForSeconds(intro.length - Time.deltaTime * 2);
		_source.loop = true;
		_source.clip = loop;
		_source.Play();
    }
}
