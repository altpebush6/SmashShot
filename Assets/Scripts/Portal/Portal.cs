using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private float distance;

    [SerializeField] private GameObject gameDone;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject passParticle;

    [SerializeField] private SceneLoader SceneLoader;
    [SerializeField] private AudioSource portalAudio;
    [SerializeField] private string sceneName;
    private bool portalPassed;

    void Start()
    {
        portalPassed = false;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(!portalPassed && Vector2.Distance(transform.position, player.transform.position) < 0.2f)
        {
            GM.SetGameDeactive();
            portalPassed = true;

            portalAudio.Play();
            StartCoroutine(PortalPass());
        }
    }

    IEnumerator PortalPass()
    {
        player.GetComponent<PlayerControl>().portalPassed = portalPassed;

        Instantiate(passParticle, transform.position, Quaternion.identity);

        if(sceneName == "End")
        {
            GM.SetGameDeactive();
            gameDone.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            SceneLoader.LoadSceneByName(sceneName);
        }
    }
}
