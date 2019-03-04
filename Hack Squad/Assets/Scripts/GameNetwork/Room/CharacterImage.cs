using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterImage : Photon.PunBehaviour {

	#region Variables
    public bool isSelected = false;
    
    private Image image;

    [SerializeField] private TMP_Text nameOfPlayer;
    public TMP_Text NameOfPlayer { set { } get { return nameOfPlayer; } }

    [SerializeField] private Sprite spritePlayer;

    [SerializeField] private Sprite crossImage;

    private PhotonPlayer playerSelect;

    /// <summary>
    /// Tipo de personaje que puede elegir un jugador, cada imagen contiene su propio prefab de Personaje.
    /// </summary>
    [SerializeField] Player playerPrefab;

    /// <summary>
    /// Instancia de jugador que se crea al iniciar el juego, se utiliza para poder adjuntar un personaje a un jugador.
    /// </summary>
    PlayerNetwork playerNetwork;

    SelectCharacterPanel selectCharacterScript;

    #endregion

    #region Métodos que ejecuta la clase

    private void Start() 
    {
        playerNetwork = GameObject.Find("DDOL").GetComponentInChildren<PlayerNetwork>();
        selectCharacterScript = GetComponentInParent<SelectCharacterPanel>();
        image = GetComponent<Image>();
    }


    /// <summary>
    /// Método RPC que se utiliza para que un jugador seleccione a su personaje.
    /// Y envíe la información a los demás jugadores en el juego.
    /// </summary>
    /// <param name="player">Jugador actual</param>
    [PunRPC]
    private void SetCharacter(PhotonPlayer player)
    {
        playerSelect = player;
        isSelected = true;
        NameOfPlayer.text = player.NickName;
        image.sprite = crossImage;
    }

    /// <summary>
    /// Método RPC que se utiliza para deseleccionar a un personaje del juego.
    /// Y envíe la información a los demás jugadores en el juego.
    /// </summary>
    /// <param name="player">Jugador actual</param>
    [PunRPC]
    private void UnsetCharacter()
    {
        playerSelect = null;
        isSelected = false;
        NameOfPlayer.text = "Name player";
        image.sprite = spritePlayer;
    }

    /// <summary>
    /// Método que asigna un personaje al jugador, para posteriormente instanciarlo en la escena.
    /// </summary>
    public void AssignCharacterToPlayer()
    {
        playerNetwork.CharacterSelected = playerPrefab;
    }

    /// <summary>
    /// Método que hace el llamado a los RPC y selecciona personaje para el player.
    /// Primero, evalúa si el personaje no ha sido seleccionado previamente, para asignarlo a un jugador y mostrarlo en las pantallas de los demás jugadores.
    /// Segundo, evalúa si ya está seleccionado un personaje y es el mismo jugador que lo seleccionó, para poder deseleccionarlo y elegir otro.
    /// </summary>
    public void Character_OnClick()
    {
        if (!isSelected && playerNetwork.CharacterSelected == null)
        {
            selectCharacterScript.IncreaseQuantityCharacters();
            AssignCharacterToPlayer();
            photonView.RPC("SetCharacter", PhotonTargets.AllBuffered, PhotonNetwork.player);
        }
        else if (isSelected && PhotonNetwork.player == playerSelect)
        {
            selectCharacterScript.DecreaseQuantityCharacters();
            playerNetwork.CharacterSelected = null;
            photonView.RPC("UnsetCharacter", PhotonTargets.AllBuffered);
        }
    }
    #endregion
}
