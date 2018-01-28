using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : ObjectActivator 
{
    [SerializeField]
    private Transform _destinationPosition;
    [SerializeField]
    private float _timeTilDestination;
    [SerializeField]
    private iTween.EaseType _easeType;

    private Vector3 _initialPosition;
    private Vector3 _finalDestination;

    private void Awake()
    {
        _initialPosition = transform.position;
        _finalDestination = _destinationPosition.transform.position;
    }

    protected override void SetGameObjectActive()
    {
        gameObject.SetActive(true);
        iTween.MoveTo(gameObject, iTween.Hash("time", _timeTilDestination, "easeType", _easeType, "position", _finalDestination));
    }

    public override void DeactivateObject()
    {
        iTween.MoveTo(gameObject, iTween.Hash("time", _timeTilDestination, "easeType", _easeType, "position", _initialPosition, "oncomplete", "FinishedSliding"));
    }

    protected void FinishedSliding()
    {
        gameObject.SetActive(false);
    }

}
