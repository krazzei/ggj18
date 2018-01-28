using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour 
{
	public static GameMode instance;
	public GameObject playerObject;

    private void Awake()
    {
		instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
