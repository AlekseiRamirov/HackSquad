using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomPrefab : MonoBehaviour
{

    [SerializeField]
    private TMP_Text roomNameText;
    private TMP_Text RoomNameText { get { return roomNameText; } }

    public string RoomName { get; private set; }

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

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }

}
