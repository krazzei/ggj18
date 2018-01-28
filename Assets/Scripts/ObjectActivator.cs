using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public bool IsActivated { get { return _isActivated; } }

    [SerializeField]
    private float _delayedStartLength;

    private bool _isActivated = false;
    private bool _hasSetGameObjectActive = false;
    private float _activationTime;

    public void ActivateObject()
    {
        _isActivated = true;
        _activationTime = Time.time;
        Invoke("SetGameObjectActive", _delayedStartLength);
    }

    private void SetGameObjectActive()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateObject()
    {
        _isActivated = false;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        
        gameObject.SetActive(false);
    }
}
