using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Chicks
{
    Emo,
    Girl,
    Nerd
};

// Use this to call to our dialog controller
public class DialogController : MonoBehaviour
{
    public static DialogController instance;
    //public RPGTalk rpgTalk;

    [Header("Text file containing dialog and wave separators")]
    public TextAsset dialogScript;
    private char[] delimiterChars = { ' ', ',', '\t' };

    private List<List<string>> itemDialogs = new List<List<string>>();
    [Header("Debugging info")]
    //public List<int> waveStartLines = new List<int>();
    
    //public int wave;
    private int startLine, endLine;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
      
        for (int i = 0; i < 10;  ++i)
        {
            itemDialogs.Add(new List<string>());
        }

        ParseTextfileForLineNums();
    }

    public void Start()
    {       
        
    }

    public string GetItemDialogForChickType(Chicks type, int itemIndex)
    {
        return itemDialogs[itemIndex][(int)type];
    }

    private void ParseTextfileForLineNums()
    {
        string[] lines = dialogScript.text.Split('\n');
        int currItem = -1;
        for (int lineNum = 0; lineNum < lines.Length; ++lineNum)
        {         
            string[] words = lines[lineNum].Split(delimiterChars);
            if (words.Length <= 0)
            {
                continue;
            }

            Debug.Log(lines[lineNum]);
            string firstWord = words[0];
            if (firstWord.ToLower().Contains("item"))
            {
                currItem++;
            }

            // Check if the first word is one of our keywords:       
            if (firstWord.ToLower().Contains("emo") || 
                firstWord.ToLower().Contains("nerd") ||
                firstWord.ToLower().Contains("girl"))                   
            {
                //Debug.Log("Contains keyword");
                //Debug.Log(lineNum);
                itemDialogs[currItem].Add(lines[++lineNum]);
            }

        }
    }
}
