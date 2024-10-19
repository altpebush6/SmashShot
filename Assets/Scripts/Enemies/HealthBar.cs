using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float offset = 1.2f;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + offset, target.position.z);
    }
}
