using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que controla la información del jugador en el servicio de photon,
/// además mantiene toda esta información para no ser destruida entre carga de niveles.
/// </summary>
public class PlayerNetwork : Photon.PunBehaviour
{
    /// <summary>
    /// Instancia que se crea durante todo el juego y mantiene los datos del jugador
    /// </summary>
    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }

    public Player CharacterSelected {get; set;}

    int playersInGame = 0;
    private void Awake()
    {
        instance = this; // Instancia del objeto PlayerNetwork antes de que arranque el juego.
        PlayerName = "Player# " + Random.Range(1000, 9999); // Asignación de un nombre de jugador antes de que arranque el juego.

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Test")
        {
            if (PhotonNetwork.isMasterClient) {
                MasterLoadedGame();
            }else
            {
                NonMasterLoadedGame();
            }
                
        }
    }

    private void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene.");
        }
    }

}
