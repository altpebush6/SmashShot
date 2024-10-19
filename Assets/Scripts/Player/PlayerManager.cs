using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private HealthManager HM;

    void Start()
    {
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

    void Update()
    {
        if(transform.position.y <= -20f)
        {
            HM.Die();
            gameObject.SetActive(false);
        }
    }
}
