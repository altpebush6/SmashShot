using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallManager : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private TextMeshProUGUI ballCount;
    [SerializeField] private RawImage ballImage;
    [SerializeField] private GameObject outofball;

    [SerializeField] private Transform playerTF;

    [SerializeField] private float cooldownTime = 1f;

    [SerializeField] private int originalBallRight;
    private int ballRight;
    [SerializeField] private float offset;

    private bool instantiatable = true;
    

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        ballRight = originalBallRight;
        ballImage.color = Color.green;

        ballRight = originalBallRight;
        ballCount.text = ballRight.ToString();
    }

    void Update()
    {
        if(GM.isGameActive())
        {
            if(Input.GetButton("InstantiateBall") && instantiatable && ballRight > 0)
            {
                ballRight--;

                UpdateBallImageColor();

                StartCoroutine(InstantiateCooldown());

                Vector3 position = new Vector3(playerTF.transform.position.x, playerTF.transform.position.y + offset, playerTF.transform.position.z);

                Instantiate(ballPrefab, position, Quaternion.identity); 

                ballCount.text = ballRight.ToString();

                if(ballRight == 0)
                {
                    outofball.SetActive(true);
                }
            }
        }
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
