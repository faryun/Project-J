using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    bool isClick;
    public TMP_Text DiceText;
    public TMP_Text TargetText;
    public Image[] diceSlotImages;
    public int[] diceTypes = new int[] { 0 }; // 기본적으로 0번 주사위만 존재
    public DiceEye[][] dices = new DiceEye[5][]; // 주사위 눈 저장 배열 (최대 5개)

    int targetValue = 0; // 목표값
    List<string> globalBuffs = new List<string>(); // 전역 버프 리스트

    public void Roll()
    {
        targetValue = 0; // 새로 굴릴 때마다 목표값 초기화
        string diceTextValue = "";

        for (int i = 0; i < diceTypes.Length; i++)
        {
            int diceType = diceTypes[i];
            DiceEye rolledEye;

            // 주사위 눈이 비어있으면 기본 1로 설정
            if (dices[diceType].Length == 0)
            {
                rolledEye = DiceList.defaultEyes[0];
            }
            else
            {
                int rand = Random.Range(0, dices[diceType].Length);
                rolledEye = dices[diceType][rand];
            }

            // 주사위 눈 효과 적용
            ApplyEyeEffect(rolledEye, i);

            diceTextValue += diceType + ":" + rolledEye.value + " ";
        }

        DiceText.text = diceTextValue;
        TargetText.text = "Sum: " + targetValue;

        // 주사위 슬롯 UI 업데이트
        UpdateDiceSlots(); 
        // 전역 버프 효과 적용
        ApplyGlobalBuffs();
    }

    void ApplyEyeEffect(DiceEye eye, int diceIndex)
    {
        // 기본 값 적용
        targetValue += eye.value;

        // 태그별 효과 적용
        foreach (string tag in eye.tags)
        {
            switch (tag)
            {
                case "보물상자":
                    // 열쇠와 함께 나왔는지 확인
                    bool hasKey = false;
                    for (int i = 0; i < diceTypes.Length; i++)
                    {
                        if (i != diceIndex && dices[i].Contains(DiceList.key))
                        {
                            hasKey = true;
                            break;
                        }
                    }
                    if (hasKey)
                        targetValue += 10;
                    break;
                case "7":
                    // 인접한 주사위에 7이 두개 있는지 확인
                    int sevenCount = 0;
                    for (int i = 0; i < diceTypes.Length; i++)
                    {
                        if (i != diceIndex && dices[i].Contains(DiceList.seven))
                        {
                            sevenCount++;
                        }
                    }
                    if (sevenCount >= 2)
                        targetValue += 74;
                    break;
                case "헉":
                    // 전역 버프 '헉' 추가
                    globalBuffs.Add("헉");
                    break;
                // 다른 태그에 대한 효과 추가
            }
        }
    }

    void ApplyGlobalBuffs()
    {
        // 전역 버프 '헉' 효과 적용
        if (globalBuffs.Count >= 3)
        {
            targetValue += 12;
            globalBuffs.Clear();
        }
    }

    private int nextDiceType = 1;

    public void AddDiceType()
    {
        int newType = nextDiceType;
        nextDiceType++;

        int[] newDiceTypes = new int[diceTypes.Length + 1];
        System.Array.Copy(diceTypes, newDiceTypes, diceTypes.Length);
        newDiceTypes[newDiceTypes.Length - 1] = newType;
        diceTypes = newDiceTypes;
        dices[newType] = new DiceEye[0]; // 새로운 주사위 눈 배열 초기화

        // 새로운 주사위에 DefaultEyes 추가
        for (int i = 0; i < DiceList.defaultEyes.Length; i++)
        {
            AddEyeToType(newType, DiceList.defaultEyes[i]);
        }

        UpdateDiceSlots();
    }

    public void AddEyeToType(int diceType, DiceEye eye)
    {
        // 해당 주사위 종류에 새로운 눈 추가
        DiceEye[] newEyes = new DiceEye[dices[diceType].Length + 1];
        System.Array.Copy(dices[diceType], newEyes, dices[diceType].Length);
        newEyes[newEyes.Length - 1] = eye;
        dices[diceType] = newEyes;
    }

    void UpdateDiceSlots()
    {
        for (int i = 0; i < diceTypes.Length; i++)
        {
            int diceType = diceTypes[i];
            Image slotImage = diceSlotImages[diceType];
            
            if (dices[diceType].Length == 0)
            {
                slotImage.sprite = null; // 주사위 눈이 없으면 비움
            }
            else
            {
                DiceEye eye = dices[diceType][0]; // 첫 번째 주사위 눈 가져옴
                slotImage.sprite = GetSpriteForEye(eye); // 해당 주사위 눈의 스프라이트 설정
            }
        }
    }

    Sprite GetSpriteForEye(DiceEye eye)
    {
        // 여기에 각 주사위 눈에 대한 스프라이트를 가져오는 로직 구현
        switch (eye.value)
        {
            case 1: return Resources.Load<Sprite>("DiceEyes/One");
            case 2: return Resources.Load<Sprite>("DiceEyes/Two");
            // ... 다른 눈 값에 대한 스프라이트 로드 추가
            // 기본 스프라이트 반환
            default: return Resources.Load<Sprite>("DiceEyes/Default");
        }
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

    void Start()
    {
        // dices 배열 초기화
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i] = new DiceEye[0]; // 각 인덱스에 빈 배열 할당
        }

        // 기본 주사위 (0번) 초기화
        for (int i = 0; i < DiceList.defaultEyes.Length; i++)
        {
            AddEyeToType(0, DiceList.defaultEyes[i]);
        }

        UpdateDiceSlots();
    }

    private void Update() 
    {
        //버튼 누르는 동안 & 스페이스바 누른 동안 계속 굴림
        if(isClick || Input.GetKey(KeyCode.Space)) Roll();
    }
}
