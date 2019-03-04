using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script que controla las acciones que posee el prefab de la Room, y además, los atributos que este posee.
/// </summary>
public class RoomPrefab : MonoBehaviour
{
    /// <summary>
    /// Texto que aparece en el prefab de la Room cuando se crea, este texo se lo asigna un jugador que crea la Room.
    /// </summary>
    [SerializeField]
    private TMP_Text roomNameText;
    private TMP_Text RoomNameText { get { return roomNameText; } }

    /// <summary>
    /// Método que trae el nombre de la room cuando se listan las rooms en la pantalla de carga de Rooms.
    /// </summary>
    public string RoomName { get; private set; }

    /// <summary>
    /// Variable booleana que actualiza el estado de la Room, para asegurarse de que se muestre o no en la lista de Rooms.
    /// Primero, se crea un objeto al cual se le asigna la instancia del objeto Canvas principal "MainCanvasManager".
    /// Segundo, se crea un objeto script "LobbyCanvas" al cual se le asigna el objeto creado anteriormente pero trayendo su componente script "LobbyCanvas".
    /// Tercero, se crea un objeto "button" y se le asigna el componente "button".
    /// Cuarto, se utiliza el método AddListener para añadir una funcionalidad al prefab button de la Room, para cuando se de click encima de este, acceda a una Room con el nombre asignado al momento de su creación.
    /// </summary>
    public bool Updated { get; set; }

    void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.instance.LobbyCanvas.gameObject; 
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClick_JoinRoom(RoomNameText.text)); 

    }

    /// <summary>
    /// Método que se lanza cuando un prefab button de Room es destruido,
    /// este se asegura que se destruya y además se le remueven todos las acciones agregadas con AddListener.
    /// </summary>
    void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Método que setea un nombre de sala al presente prefab button de Room.
    /// </summary>
    /// <param name="text"> Este parametro es de tipo entero, y guarda el nombre de la Room, para despues asignarlo</param>
    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }

}
