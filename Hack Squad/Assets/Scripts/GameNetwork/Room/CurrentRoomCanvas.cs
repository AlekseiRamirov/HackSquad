using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que contiene todas las acciones que deben realizar los objetos hijos que se encuentren en la jerarquía CurrentRoom.
/// </summary>
public class CurrentRoomCanvas : MonoBehaviour {

    [SerializeField]
    private GameObject panelListPlayers;

    [SerializeField]
    private GameObject panelSelectPlayer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ChangeToPanelSelectPlayer()
    {
        panelListPlayers.transform.SetAsFirstSibling();
    }
}
