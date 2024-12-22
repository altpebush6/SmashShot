using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text name;

    public void SetName(string name)
    {
        this.name.text = name;
    }
}
