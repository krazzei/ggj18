﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour 
{
    protected List<ObjectActivator> _roomObjectsToActivate = new List<ObjectActivator>();

    protected void Awake()
    {
        var comps = gameObject.GetComponentsInChildren<ObjectActivator>();
        _roomObjectsToActivate = comps.ToList();
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

    //public void EndRoom()
    //{
    //    foreach(var roomObj in _roomObjectsToActivate)
    //    {
    //        
    //    }
    //}

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

        //if(GUI.Button(new Rect(170, 10, 150, 50), "Leave Room"))
        //{
        //    PauseRoom();
        //}
    }
}
