using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NetworkUI : Photon.PunBehaviour
{
    [SerializeField]
    private TMP_InputField roomNameInput; // Input de nombre de sala

    [SerializeField]
    private TMP_Dropdown maxPlayersDropDown; // Dropdown que contiene los valores de la cantidad máx de jugadores por sala

    [SerializeField]
    private GameObject listRoomsDropDown; // Dropdown donde se aloja la lista de salas creadas por los jugadores

    [SerializeField]
    private GameObject prefabOfRoom; // Prefab de boton de acceso a una sala creada

    [SerializeField]
    private TMP_Text titleOfRoom; // Texto que refleja el título o nombre de la sala creada

    [SerializeField]
    private GameObject listOfplayers; // Gameobject donde se aloja la lista de jugadores presentes en una sala

    [SerializeField]
    private GameObject prefabNameOfPlayer; // Prefab del nombre que posee cada jugador

    private int quantityOfPlayers;

    private GameObject panelListRooms;
    private GameObject panelRoom;

    /// <summary>
    /// Método por el cual crea una sala de juego.
    /// Esta se crea con las opciones que le brinda el jugador; "Nombre de la sala" y "Cantidad de jugadores".
    /// </summary>
    public void CreateRoomGame()
    {
        var options = new RoomOptions();
        switch (maxPlayersDropDown.value)
        {
            case 0:
                options.MaxPlayers = 3;
                quantityOfPlayers = options.MaxPlayers;
                break;
            case 1:
                options.MaxPlayers = 4;
                quantityOfPlayers = options.MaxPlayers;
                break;
            case 2:
                options.MaxPlayers = 5;
                quantityOfPlayers = options.MaxPlayers;
                break;
        }
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, options,TypedLobby.Default);
        print("Room creada con éxito!");
    }

    /// <summary>
    /// Método por el cual se cargan las salas de juego previamente creadas.
    /// Estas salas se cargan en una lista, en donde cada jugador puede elegir a cual conectarse para jugar.
    /// </summary>
    public void LoadRooms()
    {
        StartCoroutine(UpdateRooms());
    }

    /// <summary>
    /// Método por el cual se actualizan las salas creadas para que los jugadores puedan acceder a ellas.
    /// </summary>
    /// <returns>Una espera de 3 segundos entre actualización</returns>
    public IEnumerator UpdateRooms()
    {
        TextMeshProUGUI componentNameRoom;
        int quantityOfRooms = 0;
        do
        {
            if (quantityOfRooms != PhotonNetwork.GetRoomList().Length)
            {
                foreach (var item in PhotonNetwork.GetRoomList())
                {
                    print("Nombre de sala: " + item.Name);
                    prefabOfRoom = Instantiate(prefabOfRoom, listRoomsDropDown.transform.position, Quaternion.identity, listRoomsDropDown.transform) as GameObject;
                    prefabOfRoom.GetComponent<RectTransform>().localScale = Vector3.one;
                    prefabOfRoom.GetComponent<RectTransform>().position = new Vector3(0, 0, 10f);
                    componentNameRoom = prefabOfRoom.GetComponentInChildren<TextMeshProUGUI>();
                    componentNameRoom.SetText(item.Name);
                }
            }
            else
            {
                print("No hay nuevas salas");
            }
            print("Actualización de salas");
            quantityOfRooms = PhotonNetwork.GetRoomList().Length;
            yield return new WaitForSeconds(3f);
        } while (true);
    }

    /// <summary>
    /// Método por el cual se accede a la sala de juego que el jugador haya seleccionado.
    /// </summary>
    public void JoinRoom()
    {
        #region //Sección (región) creada debido a que el prefab de "Room" no admite eventos Onclick que no estén presentes en un Script  
        panelListRooms = GameObject.Find("PanelListRooms");
        panelListRooms.SetActive(false);
        panelRoom = GameObject.Find("Canvas").transform.Find("PanelRoom").gameObject;
        panelRoom.SetActive(true);
        #endregion
        if (panelRoom.activeSelf)
        {
            TextMeshProUGUI roomName = this.GetComponentInChildren<TextMeshProUGUI>(); // Se toma el componente de texto del prefab "Room".
            PhotonNetwork.JoinRoom(roomName.text); // Se accede a la sala con el nombre que contiene el componente de texto del prefab "Room".
        }
    }

    /// <summary>
    /// Método que sobreescribe la función por defecto de Photon.
    /// Está función se ejecuta al momento que un jugador ingresa a la sala de juego.
    /// </summary>
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            titleOfRoom.SetText("Room: " + PhotonNetwork.room.Name);
            StartCoroutine(UpdatePlayersInRoom());
        }
        else
        {
            PhotonNetwork.ReJoinRoom(PhotonNetwork.room.Name);
        }

    }

    public override void OnLeftRoom()
    {
        StartCoroutine(UpdatePlayersInRoom());
    }

    /// <summary>
    /// Método por el cual se realiza un actualización de la lista de los jugadores presentes en la sala
    /// cada 3 segundos
    /// </summary>
    /// <returns>Una espera de 3 segundos entre actualización de lista de jugadores</returns>
    [PunRPC]
    public IEnumerator UpdatePlayersInRoom()
    {
        TextMeshProUGUI componentNamePlayer;
        int playersInRoom = 0;
        do
        {
            if (playersInRoom != PhotonNetwork.playerList.Length)
            {
                foreach (var item in PhotonNetwork.playerList)
                {
                    prefabNameOfPlayer = Instantiate(prefabNameOfPlayer, listOfplayers.transform.position, Quaternion.identity, listOfplayers.transform) as GameObject;
                    prefabNameOfPlayer.GetComponent<RectTransform>().localScale = Vector3.one;
                    prefabNameOfPlayer.GetComponent<RectTransform>().position = new Vector3(0, 0, 10f);
                    componentNamePlayer = prefabNameOfPlayer.GetComponent<TextMeshProUGUI>();
                    componentNamePlayer.SetText(item.NickName);
                }
            }
            playersInRoom = PhotonNetwork.playerList.Length;
            print("Actualización de players");
            yield return new WaitForSeconds(3f);
        } while (playersInRoom != quantityOfPlayers);
        LoadNewScene();
    }

    /// <summary>
    /// Método por el cual se carga la escena Test al momento que se cumpla la cantidad de jugadores máx establecidos inicialmente.
    /// </summary>
    public void LoadNewScene()
    {
        PhotonNetwork.LoadLevel("Test");
        OnFinishedLoadScene();
    }

    
    public void OnFinishedLoadScene()
    {
        if (SceneManagerHelper.ActiveSceneName == "Test") {
            foreach (var item in PhotonNetwork.playerList)
            {
                print(item.NickName);
            }
        }       
    }

    

}


