using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Chicks
{
    Emo,
    Nerd,
    Girl
};

// Use this to call to our dialog controller
public class DialogController : MonoBehaviour
{
    public static DialogController instance;
    //public RPGTalk rpgTalk;

    [Header("Text file containing dialog and wave separators")]
    public TextAsset dialogScript;
    private char[] delimiterChars = { ' ', ',', '\t' };

    public List<List<string>> itemDialogs = new List<List<string>>();
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
                itemDialogs[currItem].Add(lines[++lineNum]);
            }

        }
    }
}
