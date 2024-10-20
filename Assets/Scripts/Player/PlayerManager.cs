using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private HealthManager HM;
    [SerializeField] private float minHeight;

    void Start()
    {
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

    void Update()
    {
        if(transform.position.y <= minHeight)
        {
            HM.Die();
            gameObject.SetActive(false);
        }
    }
}
