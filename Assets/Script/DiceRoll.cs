using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public TMP_Text DiceText;
    public TMP_Text SumText;
    int[] dice = new int[] {1,2,3,4,5,6};
    int sum = 0;
    public void Roll()
    {
        int rand = Random.Range(0,6);
        DiceText.text = dice[rand].ToString();
        sum = sum + dice[rand];
        SumText.text = sum.ToString();
    }
}
