using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject Mark;
    private HealthManager HM;
    private GameManager GM;
    private PhotonView photonView;
    
    [SerializeField] private float minHeight;
    [SerializeField] private GameObject playerNameText;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if(!photonView.IsMine)
        {
            SetOtherPlayer();
        }
    }

    void Update()
    {
        if(transform.position.y <= minHeight)
        {
            HM.Die();
            gameObject.SetActive(false);
        }
    }

    void SetOtherPlayer()
    {
        string playerName = photonView.Owner.NickName;
        gameObject.name = playerName;

        playerNameText.SetActive(true);
        playerNameText.GetComponent<TMP_Text>().text = playerName;
    }

    public void PassPortal(string sceneName)
    {
        DestroyPlayer();
        NextScene(sceneName);
    }

    public void NextScene(string sceneName)
    {

        if (photonView.IsMine)
        {
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
            {
                {"PortalPassed", sceneName}
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }

    public void DestroyPlayer()
    {
        if (photonView.IsMine)
        {
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
            {
                {"PlayerGameObject", gameObject.name}
            };
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);        
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameObjectName;
        if (propertiesThatChanged.TryGetValue("PlayerGameObject", out gameObjectName))
        {
            string playerName = (string) gameObjectName;
            GameObject player = GameObject.Find(playerName);
            if(player != null)
            {
                if(!player.GetComponent<PhotonView>().IsMine)
                {
                    player.SetActive(false);
                }
            }
        }

        object sceneNameObj;
        if (propertiesThatChanged.TryGetValue("PortalPassed", out sceneNameObj))
        {
            string sceneName = (string) sceneNameObj;
            if(sceneName != null)
            {
                if(sceneName.Equals("End"))
                {
                    GM.SetGameDeactive();
                    GameObject.Find("GameDoneGO").transform.Find("GameDone").gameObject.SetActive(true);
                }
                else
                {
                    GameObject.Find("SceneManager").GetComponent<SceneLoader>().LoadSceneByName(sceneName);
                }
               
            }
        }
    }
}
