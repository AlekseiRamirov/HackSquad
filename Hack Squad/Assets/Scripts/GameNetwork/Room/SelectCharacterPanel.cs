using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectCharacterPanel : Photon.PunBehaviour
{

    public int count = 0;
    CharacterImage characterImage;

    int timeLeft = 5;

    [SerializeField] TMP_Text timerText;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ValidateQuantityOfCharactersSelected();
    }

    public void IncreaseQuantityCharacters()
    {
        count++;
    }
    public void DecreaseQuantityCharacters()
    {
        count--;
    }

    
    public void ValidateQuantityOfCharactersSelected()
    {
        Debug.Log("Contador " + count);
        if (count == 3)
        {
            StartTimer();
        }
        else if (count != 3)
        {
            RestartTimer();
        }
    }

    public void StartTimer()
    {
        timerText.gameObject.SetActive(true);
        timeLeft -= (int)Time.deltaTime;
        timerText.text = "StarMatch In: " + timeLeft;
        LevelTransition();
    }

    public void RestartTimer()
    {
        timerText.gameObject.SetActive(false);
        timeLeft = 5;
    }

    public void LevelTransition()
    {
        if (timeLeft <= 0)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(count);
        }
        else
        {
            count = (int)stream.ReceiveNext();
        }
    }
}
