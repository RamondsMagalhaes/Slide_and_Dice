using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    [TextArea(3,10)]
    public string[] sentences;
    public Texture[] images;

    public Dialogue(string sentence, Texture image)
    {
        string[] singleSentence = { sentence };
        sentences = singleSentence;
        Texture[] singleTexture = { image };
        images = singleTexture;

    }
}
