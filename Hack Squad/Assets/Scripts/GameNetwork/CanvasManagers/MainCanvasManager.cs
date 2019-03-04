using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que permite controlar los objetos presentes en la jerarquía del Canvas principal de la UI del juego.
/// </summary>
public class MainCanvasManager : MonoBehaviour
{
    /// <summary>
    /// Objeto estatico que se crea para que no se destruya durante todo el juego, y se mantengan los datos de la room y los jugadores
    /// </summary>
    public static MainCanvasManager instance;

    /// <summary>
    /// Objeto que representa al Lobby presente en la jerarquía del canvas principal
    /// </summary>
    [SerializeField]
    private LobbyCanvas lobbyCanvas;
    public LobbyCanvas LobbyCanvas { get { return lobbyCanvas; } }

    /// <summary>
    /// Objeto que representa a la actual Room previamente creada, donde acceden los jugadores y se listan, este tambien se encuentra presente en la jerarquía del canvas principal
    /// </summary>
    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }

    private void Awake()
    {
        instance = this; // Instancia del objeto actual, antes de que inicie el juego.
    }
}
