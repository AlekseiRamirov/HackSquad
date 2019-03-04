using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script que se le asigna al objeto PlayerLayoutGroup en la jerarquía, y contiene las acciones que debe realizar mientras se estén conectando a una room los jugadores.
/// </summary>
public class PlayerLayoutGroup : Photon.PunBehaviour {

    /// <summary>
    /// Objeto que trae al prefab del nombre del jugador.
    /// </summary>
    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject PlayerUIPrefab { get { return playerUIPrefab; } }

    /// <summary>
    /// Nombre de la sala actual que contiene la lista de players.
    /// </summary>
    [SerializeField]
    private TMP_Text titleRoom;

    /// <summary>
    /// Lista de prefabs de nombres.
    /// </summary>
    private List<PlayerUIPrefab> playerUIPrefabs = new List<PlayerUIPrefab>();
    private List<PlayerUIPrefab> PlayerUIPrefabs{ get { return playerUIPrefabs; } }

    [SerializeField] CreateRoom createRoom;

    /// <summary>
    /// Método que ejecuta el servicio de photon al momento de que un jugador ingrese a una Room previamente creada. 
    /// Primero, se destruye cualquier objeto repetido para que la lista sea limpia.
    /// Segundo, se muestra de primero en camara al objeto CurrentRoomCanvas.
    /// Tercero, se muestra el nombre de la sala.
    /// Cuarto, se hace un recuento de cuantos jugadores están presentes en la sala con el método Playerlist, y se agregan a un vector PhotonPlayer.
    /// Quinto, se recorre dicho vector ejecutando el método creado "PlayerJoinedRoom", pasándole por parámetro cada posición del vector.
    /// Por último, se evalúa cuantos players se encuentran en la room, si se cumple la cantidad máxima, se procede a elegir personaje.
    /// </summary>
    public override void OnJoinedRoom()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        MainCanvasManager.instance.CurrentRoomCanvas.transform.SetAsLastSibling();

        titleRoom.text = "List Players of: " + PhotonNetwork.room.Name;

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        for (int i = 0; i < photonPlayers.Length; i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }

        if (PhotonNetwork.playerList.Length == createRoom.maxPlayersCount)
        {
            MainCanvasManager.instance.CurrentRoomCanvas.ChangeToPanelSelectPlayer();
        }

    }

    /// <summary>
    /// Método que se ejecuta cuando el creador de la sala abandona o pierde la conexión.
    /// Este método ejecuta otro método del servicio photon "LeaveRoom".
    /// </summary>
    /// <param name="newMasterClient"></param>
    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Método que se ejecuta cada vez que un jugador nuevo se ha conectado a una room, mientras estemos en ella.
    /// Este método ejecuta otro método creado previamente, "PlayerJoinedRoom".
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        PlayerJoinedRoom(newPlayer);
        if (PhotonNetwork.playerList.Length == createRoom.maxPlayersCount)
        {
            MainCanvasManager.instance.CurrentRoomCanvas.ChangeToPanelSelectPlayer();
        }
    }

    /// <summary>
    /// Método que se ejecuta cada vez que un jugador se ha desconectado de una room.
    /// Este método ejecuta otro método creado previamente, "PlayerLeftRoom"
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        PlayerLeftRoom(otherPlayer);
    }

    /// <summary>
    /// Método que se ejecuta cuando cada jugador ingresa a una room.
    /// Primero, verifica que el parametro que trae no este vacío.
    /// Segundo, ejecuta el método PlayerLeftRoom, para saber si debe remover un prefab de nombre repetido.
    /// Tercero, se instancia un prefab de nombre como hijo del presente objeto "PlayerLayoutGroup".
    /// Cuarto, se llama al componente script PlayerUIPrefab y se ejecuta un método creado en esa clase llamado "ApplyPhotonPlayer" asignadole el parámetro traido por el photonPlayer.
    /// Quinto, se agrega a la lista de prefabs, el prefab de nombre instanciado previamente.
    /// </summary>
    /// <param name="photonPlayer">Contiene toda la información de un player alojada en el servicio de photon.</param>
    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);

        GameObject playerUIObj = Instantiate(PlayerUIPrefab);
        playerUIObj.transform.SetParent(transform, false);

        PlayerUIPrefab playerUIPrefab = playerUIObj.GetComponent<PlayerUIPrefab>();
        playerUIPrefab.ApplyPhotonPlayer(photonPlayer);

        PlayerUIPrefabs.Add(playerUIPrefab);
    }

    /// <summary>
    /// Método que se ejecuta cuando un player dejó una sala.
    /// Primero, se hace un recorrido de la lista de prefabs de nombres de los player, buscando la variable x sea igual al que se está pasando por parámetro.
    /// De esta manera, si se encuentra que es igual, el sistema arroja un -1.
    /// Si no es igual a -1, entonces significa que el player no se encuentra en sala y por ende debe destruirse su prefab de nombre y removerse de la lista de prefabs, para que no aparezca en pantalla.
    /// </summary>
    /// <param name="photonPlayer"></param>
    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = PlayerUIPrefabs.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index != -1)
        {
            Destroy(PlayerUIPrefabs[index].gameObject);
            PlayerUIPrefabs.RemoveAt(index);
        }
    }

    /// <summary>
    /// Método que se ejecuta cuando un player clickea el botón Leave Room.
    /// Primero, recorre la lista de prefabs de nombres y lipia toda la lista para evitar repeticiones de nombres si hay un reingreso a la sala.
    /// Segundo, se ejecuta el método alojado en el LobbyCanvas, para que aparezca de primero las opciones de conexión.
    /// Tercero, se ejecuta el método LeaveRoom de photon. El cual permite que el jugador deje una sala y retorne al lobby.
    /// </summary>
    public void OnClick_LeaveRoom()
    {
        for (int index = 0; index < PlayerUIPrefabs.Count; index++)
        {
            Destroy(PlayerUIPrefabs[index].gameObject);
            PlayerUIPrefabs.RemoveAt(index);
        }
        MainCanvasManager.instance.LobbyCanvas.ChangeToOptionsNet();
        PhotonNetwork.LeaveRoom();
    }
}
