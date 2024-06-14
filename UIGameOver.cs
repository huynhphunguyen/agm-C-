using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static Players;
public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN asm;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        asm = new ASM_MN();
    }

    private void Start()
    {
        scoreText.text = "you Scored:\n" + scoreKeeper.GetScore();
        ASM_MN.YC1(1, "Player1", 150, "Asia");
        ASM_MN.YC2();
        ASM_MN.YC3(200);
        ASM_MN.YC4(1);
        ASM_MN.YC5();
        ASM_MN.YC6();
        ASM_MN.YC7();

    }

}

    



