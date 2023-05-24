using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGameplayStarsHolder : MonoBehaviour
{
    public Image[] stars = new Image[3];

    public void OnStarCollected(int starNumber)
    {
        this.stars[starNumber - 1].color = Color.yellow;
    }
}
