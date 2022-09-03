using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string trueString;

    public Color trueColor;
    public Color correctColor;
    public Color wrongColor;

    private string[] texts;

    private void Awake()
    {
        instance = this;
        TextAsset asset = Resources.Load<TextAsset>("WordList");
        texts = asset.text.Split('\n');
        trueString = texts[Random.Range(0, texts.Length)];
    }

}
