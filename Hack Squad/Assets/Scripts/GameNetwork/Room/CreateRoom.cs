using UnityEngine;
using TMPro;

/// <summary>
/// Script que contiene las acciones y los atributos que le corresponden a la creación de una nueva Room.
/// </summary>
public class CreateRoom : Photon.PunBehaviour
{

    /// <summary>
    /// Nombre de la sala a crear.
    /// </summary>
    [SerializeField]
    private TMP_Text roomName;
    private TMP_Text RoomName { get { return roomName; } }

    /// <summary>
    /// Cantidad máxima de jugadores permitidos en la room.
    /// </summary>
    public int maxPlayersCount;

    /// <summary>
    /// Método que se ejecuta cuando se clickea el botón Create Room.
    /// Primero, envia el mensaje de creación de la sala, con el nombre ingresado en el input text, las opciones que debe tener la sala, y el tipo de lobby en el que debe ser creada.
    /// Si el mensaje no se pudo enviar se lanza un mensaje de alerta.
    /// </summary>
    public void OnClick_CreateRoom()
    {
        if (PhotonNetwork.CreateRoom(RoomName.text, OptionsRoom(), TypedLobby.Default))
            print("Create room succesfully sent, with " + OptionsRoom().MaxPlayers + " players");
        else
            print("Create room failed to sent");
    }

    /// <summary>
    /// Método que asigna las opciones de una sala.
    /// Primero, asigna opciones para que la sala sea visible, esté abierta, y la cantidad de jugadores sea 5.
    /// </summary>
    /// <returns>Retorna las opciones de una sala</returns>
    public RoomOptions OptionsRoom()
    {
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayersCount };
        return options;
    }

    /// <summary>
    /// Método que se lanza si la hubo algún error al momento de la creación de una sala.
    /// </summary>
    /// <param name="codeAndMessage"></param>
    public override void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create room failed: " + codeAndMessage[1]);
    }

}
