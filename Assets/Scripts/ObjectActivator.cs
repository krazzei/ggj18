using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public bool IsActivated { get { return _isActivated; } }

    [SerializeField]
    private float _delayedStartLength;
    private float _activationTime;

    protected bool _isActivated = false;


    public void ActivateObject()
    {
        _isActivated = true;
        _activationTime = Time.time;
        Invoke("SetGameObjectActive", _delayedStartLength);
    }

    protected virtual void SetGameObjectActive()
    {
        if(_isActivated)
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void DeactivateObject()
    {
        _isActivated = false;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
