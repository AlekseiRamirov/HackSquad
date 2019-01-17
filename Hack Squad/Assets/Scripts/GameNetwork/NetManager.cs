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
        if (!PhotonNetwork.connected) PhotonNetwork.ConnectUsingSettings(versionGame);

    }

    /// <summary>
    /// Al momento de conectarse al lobby del servidor, se le asigna al jugador un nombre identificador.
    /// Este identificador es el nombre del dispositivo donde se está jugando.
    /// </summary>
    public override void OnJoinedLobby()
    {
        PhotonNetwork.player.NickName = SystemInfo.deviceName;
        textInfo.SetText("Conectado al server master, dispositivo: " + PhotonNetwork.player.NickName);
    }


}
