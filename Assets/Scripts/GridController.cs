using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    public Transform[] rows;
    public int row;
    public int grid;
    // 输入的结果
    public string inputAnswer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rows = new Transform[transform.childCount];
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = transform.GetChild(i);
            for(int j = 0; j < GameManager.instance.trueString.Length - 1; j++)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/BoxBorder"), rows[i]);
            }
        }
    }

    public void KeyDown(string key)
    {
        if (grid > 4) return;

        rows[row].GetChild(grid).GetComponentInChildren<Text>().text = key;
        inputAnswer += key;

        if(grid < 5)
        {
            grid++;
        }
    }

    public void KeyDelete()
    {
        if(grid <= 0) return;

        rows[row].GetChild(grid - 1).GetComponentInChildren<Text>().text = "";
        inputAnswer = inputAnswer.Substring(0, inputAnswer.Length - 1);

        if(grid > 0)
        {
            grid--;
        }
    }

    public string KeyEnter()
    {
        if (inputAnswer.Length <= 4)
        {
            return "0";
        }

        string result = "";
        string tempString = GameManager.instance.trueString;

        for(int i = 0; i < rows[row].childCount; i++)
        {
            if (tempString[i] == inputAnswer[i])
            {
                result += "T";
                // 抹零
                tempString = tempString.Substring(0, i) + "0" + tempString.Substring(i + 1, tempString.Length - i - 1);
            }
            else
            {
                result += "F";
            }
        }

        for(int i = 0; i < rows[row].childCount; i++)
        {
            if (result[i].ToString() == "T")
            {
                continue;
            }
            else
            {
                for(int j = 0; j < rows[row].childCount; j++)
                {
                    if(tempString[j] == inputAnswer[i])
                    {
                        result = result.Substring(0, i) + "C" + result.Substring(i + 1, result.Length - i - 1);
                        tempString = tempString.Substring(0, j) + "0" + tempString.Substring(j + 1, tempString.Length - j - 1);
                    }
                }
            }
        }

        SetColor(result);

        string returnResult = result + "#" + inputAnswer;

        if (row < 6)
        {
            row++;
            grid = 0;
            inputAnswer = "";
        }

        return returnResult;

    }

    private void SetColor(string result)
    {
        for (int i = 0; i < rows[row].childCount; i++)
        {
            if (result[i] == 'T')
            {
                rows[row].GetChild(i).transform.Find("Box").GetComponent<Image>().color = GameManager.instance.trueColor;
            }
            else if (result[i] == 'C')
            {
                rows[row].GetChild(i).transform.Find("Box").GetComponent<Image>().color = GameManager.instance.correctColor;
            }
            else if (result[i] == 'F')
            {
                rows[row].GetChild(i).transform.Find("Box").GetComponent<Image>().color = GameManager.instance.wrongColor;
            }

            rows[row].GetChild(i).transform.Find("Text").GetComponent<Text>().color = Color.white;
        }
    }

}
