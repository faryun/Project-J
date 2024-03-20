using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    bool isClick = false;
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

    public void ButtonDown()
    {
        Debug.Log("ButtonDown");
        isClick = true;
    }

    public void ButtonUP()
    {
        Debug.Log("ButtonUP");
        isClick = false;
    }

    private void Update() 
    {
        //버튼 누르는 동안 & 스페이스바 누른 동안 계속 굴림
        if(isClick || Input.GetKey(KeyCode.Space)) Roll();
    }
}
