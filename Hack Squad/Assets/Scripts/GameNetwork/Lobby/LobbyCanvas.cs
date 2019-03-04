using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que controla las acciones que poseen los paneles hijos presentes en la jerarquía "Lobby".
/// </summary>
public class LobbyCanvas : MonoBehaviour {

    /// <summary>
    /// Layout que contiene la lista de Rooms, previamente creadas por los jugadores.
    /// </summary>
    [SerializeField]
    private RoomLayoutGroup roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup { get { return roomLayoutGroup; } }

    /// <summary>
    /// Panel que contiene al layout de la lista de Rooms.
    /// </summary>
    [SerializeField]
    private GameObject panelListRoom;
    private GameObject PanelListRoom { get { return panelListRoom; } }

    /// <summary>
    /// Panel que contiene lo necesario para la creación de una Room de juego.
    /// </summary>
    [SerializeField]
    private GameObject panelCreateRoom;
    private GameObject PanelCreateRoom { get { return panelCreateRoom; } }

    /// <summary>
    /// Panel que contiene las opciones de creación de una room o cargar una lista de rooms creadas previamente.
    /// </summary>
    [SerializeField]
    private GameObject panelOptionsNet;
    private GameObject PanelOptionsNet { get { return panelOptionsNet; } }

    /// <summary>
    /// Método que le asigna el objeto Looby a cada prefab button de Room, para que tenga la posibilidad de acceder a una room determinada con el nombre.
    /// </summary>
    /// <param name="roomName"></param>
    public void OnClick_JoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {

        }
        else
        {
            print("Join room failed.");
        }
    }

    /// <summary>
    /// Método que jerarquiza de primer lugar al panel de las opciones de conexión, para poder mostrar el panel de Creación de Room.
    /// </summary>
    public void ChangeToCreateRoom()
    {
        PanelOptionsNet.transform.SetAsFirstSibling();
    }

    /// <summary>
    /// Método que jerarquiza de primer lugar al panel de las opciones de conexión, luego al de creación de room, para poder mostrar el Panel de la lista de Rooms.
    /// </summary>
    public void ChangeToListRooms()
    {
        PanelOptionsNet.transform.SetAsFirstSibling();
        PanelCreateRoom.transform.SetAsFirstSibling();
    }

    /// <summary>
    /// Método que jerarquiza de último lugar al Panel de opciones de conexión para poder mostrarlo.
    /// </summary>
    public void ChangeToOptionsNet()
    {
        PanelOptionsNet.transform.SetAsLastSibling();
    }
}
