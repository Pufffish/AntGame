using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI MyScoreText;
    private int ScoreNum;

    void Start ()
    {
        ScoreNum = 0;
        MyScoreText.text = "Score:" + ScoreNum;
    }

     private void OnTriggerEnter2D(Collider2D collectable)
    {
        if (collectable.tag == "collectable")
         {

        ScoreNum += 1;
        Destroy(collectable.gameObject);
        MyScoreText.text = "Score:" + ScoreNum;
        }
    }
}
