using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    [SerializeField] private Transform center;
    [SerializeField] private Transform cameraFollow;

    [SerializeField] private float initialOrthoSize = 18.0f;
    [SerializeField] private float targetOrthoSize = 15.0f;

    [SerializeField] private float transitionDuration;
    [SerializeField] private float waitTime;

    private Transform player;
    private bool isTransitioning;
    private float transitionTime = 0.0f;

    void Awake()
    {
        PlayerSpawner.OnPlayerCreated += HandlePlayerCreated;
    }

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        cinemachineCamera.Follow = cameraFollow;
        cinemachineCamera.m_Lens.OrthographicSize = initialOrthoSize;

        Invoke("StartCameraTransition", waitTime);
    }

    void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreated -= HandlePlayerCreated;
    }

    void Update()
    {
        if (player != null && isTransitioning)
        {
            transitionTime += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTime / transitionDuration);

            cameraFollow.position = Vector3.Lerp(center.position, player.position, t);

            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthoSize, targetOrthoSize, t * 2);

            if (t >= 1.0f)
            {
                isTransitioning = false;
                cinemachineCamera.Follow = player;
                
                GM.SetGameActive();
            }
        }
    }

    void StartCameraTransition()
    {
        isTransitioning = true;
        transitionTime = 0.0f;
    }

    private void HandlePlayerCreated(GameObject player)
    {
        if(player.GetComponent<PhotonView>().IsMine)
        {
            this.player = player.transform;
            cinemachineCamera.Follow = this.player;
        }
    }
}
