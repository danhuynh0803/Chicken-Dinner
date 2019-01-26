using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Use this to call to our dialog controller
public class DialogController : MonoBehaviour
{
    public static DialogController instance;
    public RPGTalk rpgTalk;

    [Header("Text file containing dialog and wave separators")]
    public TextAsset dialogScript;
    private char[] delimiterChars = { ' ', ',', '\t' };

    [Header("Debugging info")]
    public List<int> waveStartLines = new List<int>();
    
    public int wave;
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
        StartDialog();
    }

    public void StartDialog()
    {
        //Debug.Log("Starting Dialog at wave " + wave);
        // subtract from waveStartLine due us storing the start keyword
        if (wave + 1 < waveStartLines.Count-1)
        {
            startLine = waveStartLines[wave] + 1;
            endLine = waveStartLines[wave + 1] - 1;        
            
            rpgTalk.NewTalk(startLine.ToString(),
                            endLine.ToString(),
                            rpgTalk.txtToParse, this, "StartSpawn");

            wave++;
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        startLine = waveStartLines[waveStartLines.Count - 2] + 1;
        endLine = waveStartLines[waveStartLines.Count - 1] - 1;
        // Move to next level after finishing dialog
        rpgTalk.NewTalk(startLine.ToString(),
                        endLine.ToString(),
                        rpgTalk.txtToParse, this, "OpenEndPanel");
              
    }

    public void OpenEndPanel()
    {
        Debug.Log("Open end panel");
        MenuEvents.instance.OpenWinPanel();
    }

    public void StartSpawn()
    {
        Debug.Log("Start Spawning");
        EnemySpawner.instance.StartEnemySpawn();
    }

    private void ParseTextfileForLineNums()
    {
        string[] lines = dialogScript.text.Split('\n');
             
        for (int lineNum = 0; lineNum < lines.Length; ++lineNum)
        {         
            string[] words = lines[lineNum].Split(delimiterChars);
            string firstWord = words[0];

            // Check if the first word is one of our keywords: 
            // Start, Wave, End
            if (firstWord.ToLower().Contains("start") ||                   
                firstWord.ToLower().Contains("wave")  ||
                firstWord.ToLower().Contains("end"))
            {
                // Increment by one to match the actual line number of the textfile (starts at 1)
                waveStartLines.Add(lineNum+1);
            }

        }
    }
}
