using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour 
{

    [SerializeField]
    protected GameObject _roomObject;
    protected List<ObjectActivator> _roomObjectsToActivate = new List<ObjectActivator>();



    protected void Start()
    {
        _roomObjectsToActivate = _roomObject.GetComponentsInChildren<ObjectActivator>().ToList();
	}
	
    protected void Update() 
    {
		
	}

    public void StartRoom()
    {
        foreach(var roomObj in _roomObjectsToActivate)
        {
            roomObj.ActivateObject();
        }
    }

    public void PauseRoom()
    {
        foreach(var roomObj in _roomObjectsToActivate)
        {
            roomObj.DeactivateObject();
        }
    }

    private void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 10, 150, 50), "Start Room"))
        //{
        //    StartRoom();
        //}
    }
}
