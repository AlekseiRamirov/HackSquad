using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NetManager : Photon.PunBehaviour
{

    private string versionGame = "v0.1";

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

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.instance.PlayerName;
        if (!PhotonNetwork.insideLobby)
            PhotonNetwork.JoinLobby(TypedLobby.Default);

    }


    /// <summary>
    /// Al momento de conectarse al lobby del servidor, se le asigna al jugador un nombre identificador.
    /// </summary>
    public override void OnJoinedLobby()
    {
        if (!PhotonNetwork.inRoom)
            MainCanvasManager.instance.LobbyCanvas.transform.SetAsLastSibling();
        textInfo.SetText("Conectado al lobby, Player: " + PhotonNetwork.player.NickName);
    }


}
