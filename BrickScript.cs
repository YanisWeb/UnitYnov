using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour
{
    public int points;
    public int hitsBreak;
    public Sprite hitSprite;

    public void BreakBrick() {
        hitsBreak--;
        //Changement du sprite pour les briques nécessitant deux coups.
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
}

