using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PlayerColors
{
    private static Dictionary<int, Color> playerColors = new Dictionary<int, Color>();

    public static Color GetPlayerColor(int id)
    {
        playerColors[0] = new Color(0.4f, 1f, 0.9f);
        playerColors[1] = new Color(1f, 0.4f, 0.4f);
        playerColors[2] = new Color(0.4f, 1f, 0.4f);
        playerColors[3] = new Color(1f, 1f, 0.4f);

        return playerColors[id];
    }
}