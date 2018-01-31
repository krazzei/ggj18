using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : ObjectActivator 
{
    [SerializeField]
    private RoomController _currentRoomController;
    [SerializeField]
    private RoomController _nextRoomController;
    [SerializeField]
    private Transform _playerDestination;
    [SerializeField]
    private iTween.EaseType _easeType;

    private Vector3 _finalPlayerPosition;



	// Use this for initialization
	private void Start () 
    {
        _finalPlayerPosition = _playerDestination.position;
	}

    protected override void SetGameObjectActive()
    {
        base.SetGameObjectActive();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.NameToLayer("Player") == collision.gameObject.layer)
        {
            iTween.MoveTo(collision.gameObject, iTween.Hash("time", 3, "position", _finalPlayerPosition, "easeType", _easeType, "oncomplete", "NotifyNextRoom"));
        }
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var layer = LayerMask.NameToLayer("Player");
        if (layer == other.gameObject.layer)
        {
            
            iTween.MoveTo(other.gameObject, iTween.Hash("time", 0.3f, "position", _finalPlayerPosition, "easeType", _easeType, "onComplete", "NotifyNextRoom"));
        }
    }

    private void NotifyNextRoom()
    {
        _nextRoomController.StartRoom();
    }

}
