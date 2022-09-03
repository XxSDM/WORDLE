using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public Button deleteButton;
    public Button enterButton;

    private Transform[] rows;

    private string firstRow = "QWERTYUIOP";
    private string secondRow = "ASDFGHJKL";
    private string thirdRow = "ZXCVBNM";

    private string keys = "QWERTYUIOPASDFGHJKLZXCVBNM";
    private string checkKeys = "QWERTYUIOPASDFGHJKLZXCVBNM";

    private void Awake()
    {
        rows = new Transform[transform.childCount];
        // 获取三行的transform
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = transform.GetChild(i);
        }

        // 设置键位
        SetKey();
    }

    private void Start()
    {
        deleteButton.onClick.AddListener(DeleteButtonClick);
        enterButton.onClick.AddListener(EnterButtonClick);
    }


    private void SetKey()
    {
        for(int i = 0; i < rows[0].childCount; i++)
        {
            int temp = i;
            rows[0].GetChild(i).GetComponentInChildren<Text>().text = firstRow[i].ToString();
            rows[0].GetChild(i).GetComponent<Button>().onClick.AddListener(() => KeyDown(temp));
        }

        for(int i = 0; i < rows[1].childCount; i++)
        {
            int temp = i;
            rows[1].GetChild(i).GetComponentInChildren<Text>().text = secondRow[i].ToString();
            rows[1].GetChild(i).GetComponent<Button>().onClick.AddListener(() => KeyDown(temp + 10));
        }

        for(int i = 0; i < rows[2].childCount; i++)
        {
            int temp = i;
            rows[2].GetChild(i).GetComponentInChildren<Text>().text = thirdRow[i].ToString();
            rows[2].GetChild(i).GetComponent<Button>().onClick.AddListener(() => KeyDown(temp + 19));
        }
    }

    private void KeyDown(int keyIndex)
    {
        GridController.instance.KeyDown(keys[keyIndex].ToString());
    }

    private void DeleteButtonClick()
    {
        GridController.instance.KeyDelete();
    }

    private void EnterButtonClick()
    {
        string returnResult = GridController.instance.KeyEnter();
        if (returnResult == "0") return;

        string[] res = returnResult.Split('#');
        for(int i = 0; i < res[0].Length; i++)
        {
            if (res[0][i] == 'T')
            {
                SetColor(GameManager.instance.trueColor, res[1][i], 1);
            }
            else if (res[0][i] == 'C')
            {
                SetColor(GameManager.instance.correctColor, res[1][i], 0);
            }
            else if (res[0][i] == 'F')
            {
                SetColor(GameManager.instance.wrongColor, res[1][i], 0);
            }
        }

    }

    private void SetColor(Color c, char key, int operation)
    {
        int index = 99;

        for(int i = 0; i < checkKeys.Length; i++)
        {
            if(checkKeys[i] == key)
            {
                index = i;
                if(operation == 1)
                {
                    checkKeys = checkKeys.Substring(0, i) + "0" + checkKeys.Substring(i + 1);
                }
            }
        }

        if(index < 10)
        {
            rows[0].GetChild(index).GetComponent<Image>().color = c;
            rows[0].GetChild(index).transform.Find("Text").GetComponent<Text>().color = Color.white;
        }
        else if(index < 19)
        {
            rows[1].GetChild(index - 10).GetComponent<Image>().color = c;
            rows[1].GetChild(index - 10).transform.Find("Text").GetComponent<Text>().color = Color.white;
        }
        else if (index < 26)
        {
            rows[2].GetChild(index - 19).GetComponent<Image>().color = c;
            rows[2].GetChild(index - 19).transform.Find("Text").GetComponent<Text>().color = Color.white;
        }

    }

}
