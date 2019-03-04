using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Script que permite la conexión inicial al servicio de Photon, este se lanza cada que un jugador inicia un juego nuevo.
/// </summary>
public class NetManager : Photon.PunBehaviour
{
    /// <summary>
    /// Versión del juego.
    /// </summary>
    private string versionGame = "v0.1";

    /// <summary>
    /// Texto que aparece en la escena de juego, para señalar el nombre del player cuando este conectado al lobby de photon.
    /// </summary>
    [SerializeField]
    private TMP_Text textInfo;

    /// <summary>
    /// Verificación de la conexión al momento de iniciar el juego, si no está conectado, debe conectar al servicio Photon
    /// </summary>
    void Start()
    {
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings(versionGame);
    }

    /// <summary>
    /// Al momento de conectarse al servidor este método carga el tipo de sincronización de las escenas presentes en el juego,
    /// asigna el nombre al jugador de acuerdo a la instancia creada, y se conecta al lobby por defecto de photon.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.instance.PlayerName;
        if (!PhotonNetwork.insideLobby)
            PhotonNetwork.JoinLobby(TypedLobby.Default);

    }


    /// <summary>
    /// Al momento de conectarse al lobby del servidor, se muestra un mensaje en pantalla de conexión con el nombre de jugador,
    /// y se verifica si se está en una Room, si no lo está, se muestra primero en la jerarquía el objeto Looby.S
    /// </summary>
    public override void OnJoinedLobby()
    {
        textInfo.SetText("Conectado al lobby: " + PhotonNetwork.player.NickName);
        //if (!PhotonNetwork.inRoom)
        //    MainCanvasManager.instance.LobbyCanvas.transform.SetAsLastSibling();       
    }


}
