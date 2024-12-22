using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Controls;
    [SerializeField] private GameObject Lobby;
    [SerializeField] private GameObject CreateAndJoin;
    [SerializeField] private GameObject CreateRoom;
    [SerializeField] private GameObject JoinRoom;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;

    [SerializeField] private TMP_InputField username;

    private bool paused = false;
    private Dictionary<GameObject, GameObject> prevPages = new Dictionary<GameObject, GameObject>();
    private GameObject currentPage;
    private GameObject serverManager;

    void Start()
    {
        serverManager = GameObject.Find("ServerManager");

        currentPage = MainMenu;

        prevPages.Add(Controls, MainMenu);
        prevPages.Add(Lobby, MainMenu);
        prevPages.Add(CreateAndJoin, Lobby);
        prevPages.Add(CreateRoom, CreateAndJoin);
        prevPages.Add(JoinRoom, CreateAndJoin);
    }

    public void ToggleMusic()
    {
        if(!paused)
        {
            backgroundMusic.Pause();
            soundImage.sprite = muted;
            paused = true;
        }
        else
        {
            backgroundMusic.Play(0);
            soundImage.sprite = unmuted;
            paused = false;
        }
    }

    public void GoBack()
    {
        if(currentPage == Lobby)
        {
            serverManager.GetComponent<ConnectToServer>().DisconnectFromServer();
        }

        currentPage.SetActive(false);
        prevPages[currentPage].SetActive(true);

        currentPage = prevPages[currentPage];
    }
    public void SetUsernameAndContinue() 
    {
        if(username.text != "")
        {
            currentPage.SetActive(false);
            CreateAndJoin.SetActive(true);

            currentPage = CreateAndJoin;
            serverManager.GetComponent<Username>().SetUserName(username.text);
        }
        else
        {
            Debug.Log("Username can not be empty!");
        }
    }

    public void OpenControls()      { currentPage.SetActive(false); Controls.SetActive(true); currentPage = Controls;                  }
    public void OpenLobby()         { currentPage.SetActive(false); Lobby.SetActive(true); currentPage = Lobby;                        }
    public void OpenCreateRoom()    { currentPage.SetActive(false); CreateRoom.SetActive(true); currentPage = CreateRoom;         }
    public void OpenJoinRoom()      { currentPage.SetActive(false); JoinRoom.SetActive(true); currentPage = JoinRoom;             }
}
