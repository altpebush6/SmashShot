using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallManager : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private TextMeshProUGUI ballCount;
    [SerializeField] private Transform playerTF;

    [SerializeField] private float cooldownTime = 1f;

    private bool instantiatable = true;
    private int ballRight = 5;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(GM.isGameActive())
        {
            if(Input.GetButton("InstantiateBall") && instantiatable && ballRight > 0)
            {
                ballRight--;

                StartCoroutine(BounceCooldown());

                Vector2 position = new Vector2(playerTF.transform.position.x, playerTF.transform.position.y + 5f);

                Instantiate(ballPrefab, position, Quaternion.identity); 

                ballCount.text = ballRight.ToString();
            }
        }
    }

    IEnumerator BounceCooldown()
    {
        instantiatable = false;
        yield return new WaitForSeconds(cooldownTime);
        instantiatable = true;
    }
}
