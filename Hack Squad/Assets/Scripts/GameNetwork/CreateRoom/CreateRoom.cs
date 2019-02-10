using UnityEngine;
using TMPro;

public class CreateRoom : Photon.PunBehaviour
{

    [SerializeField]
    private TMP_Text roomName;
    private TMP_Text RoomName { get { return roomName; } }


    private int maxPlayersCount = 5;

    public void OnClick_CreateRoom()
    {
        if (PhotonNetwork.CreateRoom(RoomName.text, OptionsRoom(), TypedLobby.Default))
            print("Create room succesfully sent, with " + OptionsRoom().MaxPlayers + " players");
        else
            print("Create room failed to sent");
    }

    public RoomOptions OptionsRoom()
    {
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayersCount };
        return options;
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create room failed: " + codeAndMessage[1]);
    }
}
