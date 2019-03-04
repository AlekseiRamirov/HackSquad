using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script que contiene las acciones que debe poseer el prefab de nombre del player.
/// </summary>
public class PlayerUIPrefab : Photon.PunBehaviour {

    /// <summary>
    /// Variable que contiene se crea para asignar un photonplayer al prefab creado.
    /// </summary>
    public PhotonPlayer PhotonPlayer { get; private set; }

    [SerializeField]
    private TMP_Text playerName;
    private TMP_Text PlayerName { get { return playerName; } }

    /// <summary>
    /// Método por el cual se le asigna un photonplayer al prefab de nombre de un player.
    /// Además se le asigna el nombre que viene del servicio de photon al prefab de nombre.
    /// </summary>
    /// <param name="photonPlayer"></param>
    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;

    }
}
