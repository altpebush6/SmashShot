using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class BallManager : MonoBehaviour
{

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private TextMeshProUGUI ballCount;
    [SerializeField] private RawImage ballImage;
    [SerializeField] private GameObject outofball;

    [SerializeField] private float cooldownTime = 1f;

    [SerializeField] private int originalBallRight;
    [SerializeField] private float offset;

    private GameManager GM;
    private PlayerSpawner playerSpawner;
    private Transform playerTF;
    private int ballRight;
    private bool instantiatable = true;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();

        PlayerSpawner.OnPlayerCreated += HandlePlayerCreated;

        ballRight = originalBallRight;
        ballImage.color = Color.green;
        ballCount.text = ballRight.ToString();
    }

    void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreated -= HandlePlayerCreated;
    }

    void Update()
    {
        if(GM.isGameActive())
        {
            if(Input.GetButton("InstantiateBall") && instantiatable && ballRight > 0 && playerTF != null)
            {
                ballRight--;

                UpdateBallImageColor();

                StartCoroutine(InstantiateCooldown());

                Vector3 position = new Vector3(playerTF.transform.position.x, playerTF.transform.position.y + offset, playerTF.transform.position.z);

                PhotonNetwork.Instantiate(ballPrefab.name, position, Quaternion.identity); 

                ballCount.text = ballRight.ToString();

                if(ballRight == 0)
                {
                    outofball.SetActive(true);
                }
            }
        }
    }

    private void HandlePlayerCreated(GameObject player)
    {
        playerTF = player.transform;
    }

    void UpdateBallImageColor()
    {
        float ratio = (float)ballRight / (float)originalBallRight;

        ballImage.color = Color.Lerp(Color.red, Color.green, ratio);
    }

    IEnumerator InstantiateCooldown()
    {
        instantiatable = false;
        yield return new WaitForSeconds(cooldownTime);
        instantiatable = true;
    }
}
