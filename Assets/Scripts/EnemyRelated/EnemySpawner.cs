using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Spawn
{
    public Spawn(string name, float wait, GameObject gObject)
    {
        spawnName = name;
        spawnWait = wait;
        spawnObject = gObject;
    }

    public string spawnName;
    public float spawnWait;
    public GameObject spawnObject;
}

public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        /*
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        */
     
        ParseAllTextFilesAndSetupSpawnLists();
    }
    #endregion

    [Header("Separate each enemy waves with their own text scripts")]
    //public List<TextAsset> spawnScripts;
    public TextAsset spawnScript;
    private char[] delimiterChars = { ' ', ',', '\t' };
    private float spawnTimer;
    public float waveTimer; // the time in between each wave
    [SerializeField] // For debugging to see how long till next wave
    private float timer;

    [Header("Game objects to be spawned (fill with prefabs)")]
    public List<GameObject> spawnObjectList;    
    private List<List<Spawn>> listOfSpawnLists = new List<List<Spawn>>();
    private Spawn[] spawnArray;
    
    [Header("This is to offset certain models that have a different orientation")]
    public Transform offsetXMinus90;
        
    [HideInInspector]
    public List<Enemy> enemiesList;
    
    [SerializeField]
    private EnemyPathNode enemyPathNode;
    private int currentWave;

    private bool levelStarted;

    private void Start()
    {
        enemiesList = new List<Enemy>();
        enemyPathNode = GetComponent<EnemyPathNode>();
        //StartEnemySpawn(); // Spawn without dialog for debugging
        timer = waveTimer;
    }

    private void Update()
    {        
        if (levelStarted)
        {         
            if (enemiesList.Count <= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = waveTimer;
            }

            if (timer <= 0)
            {
                //StartEnemySpawn();
                levelStarted = false;
                //.instance.StartDialog();                
                timer = waveTimer;
            }
        }
    }

    // Parse text files and enter gameobjects into seperate lists
    private void ParseAllTextFilesAndSetupSpawnLists()
    {
        if (spawnScript == null)
        {
            Debug.Log("No spawn scripts attached!");
        }
       
        string[] lines = spawnScript.text.Split('\n');
        int wave = -1;
        for (int i = 0; i < lines.Length; ++i)
        {
            List<String> words = new List<string>();                 
            // Remove all empty strings
            foreach (var word in lines[i].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries))
            {
                words.Add(word.Trim());
                //Debug.Log(word);
            }
            
            // If there is a wave keyword then store the spawns in a new list
            if (words[0].ToLower().Contains("wave"))
            {
                //Debug.Log("next wave");
                listOfSpawnLists.Add(new List<Spawn>());
                wave++;
                continue;
            }

            //Debug.Log(lines[i] + " words length = " + words.Count);
         
            string name = words[0];               // Get name from file
            float wait = float.Parse(words[1]);   // Get and convert spawn wait from file
            int amount = 1;
            if (words.Count >= 3) // Amount to spawn is provided
            {
                amount = int.Parse(words[2]);
            }

            for (int j = 0; j < amount; ++j)
            {
                // Check if the string matches any of the attached prefabs
                foreach (GameObject go in spawnObjectList)
                {
                    // Find the matching gameobject based on the name string (case-insensitive)
                    if (go.name.ToString().ToLower().Equals(name.ToLower()))
                    {
                        listOfSpawnLists[wave].Add(new Spawn(name, wait, go));
                    }
                }
            }
        }
        

        // TODO read wave keywords and increment 

        /*
        for (int i = 0; i < spawnScripts.Count; ++i)
        {
            TextAsset spawnScript = spawnScripts[i];
            //listOfSpawnLists[i] = new List<Spawn>();
            // Parse each line in the textfile
            string[] lines = spawnScript.text.Split('\n');
            // Parse each word in that line
            foreach (string line in lines)
            {
                string[] words = line.Split(delimiterChars);
                string name = words[0];               // Get name from file
                float wait = float.Parse(words[1]);   // Get and convert spawn wait from file
                int amount = 1;
                if (words.Length == 3) // Amount to spawn is provided
                {
                    amount = int.Parse(words[2]);
                }

                for (int j = 0; j < amount; ++j)
                {
                    // Check if the string matches any of the attached prefabs
                    foreach (GameObject go in spawnObjectList)
                    {
                        // Find the matching gameobject based on the name string (case-insensitive)
                        if (go.name.ToString().ToLower().Equals(name.ToLower()))
                        {
                            listOfSpawnLists[i].Add(new Spawn(name, wait, go));
                        }
                    }
                }
            }
        }
        */
    }

    public void StartEnemySpawn()
    {
        if (currentWave < listOfSpawnLists.Count)
        {
            StartCoroutine(SpawnEnemy(currentWave));
            currentWave++;
        }
        levelStarted = true;
    }

    private IEnumerator SpawnEnemy(int wave)
    {
        spawnArray = listOfSpawnLists[wave].ToArray();
        for(int i = 0; i < spawnArray.Length; i++)
        {
            Spawn spawn = spawnArray[i];

            if (spawn != null)
            {
                GameObject spawnObject;
                if (spawn.spawnObject.GetComponent<Enemy>().isByAutarca)
                {
                    spawnObject = Instantiate(spawnArray[i].spawnObject, offsetXMinus90);
                    spawnObject.transform.position = enemyPathNode.wayPoints[0]; ;
                }
                else
                    spawnObject = Instantiate(spawnArray[i].spawnObject,
                        enemyPathNode.wayPoints[0], Quaternion.identity);
                enemiesList.Add(spawnObject.GetComponent<Enemy>());
                spawnObject.GetComponent<EnemyPathing>().Path = enemyPathNode;
                spawnObject.GetComponent<Enemy>().enemySpawner = this;
                yield return new WaitForSeconds(spawn.spawnWait);
            }
        }
    }

    public IEnumerator SpawnCarrierSubEnemy(GameObject enemyPrefab, Vector3 position, int currentIndex, int number, float delay)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject enemy;
            if (enemyPrefab.GetComponent<Enemy>().isByAutarca)
            {
                enemy = Instantiate(enemyPrefab, offsetXMinus90);
                enemy.transform.position = position;
            }
            else
            {
                enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            }

            enemiesList.Add(enemy.GetComponent<Enemy>());
            enemy.GetComponent<EnemyPathing>().Path = enemyPathNode;
            enemy.GetComponent<EnemyPathing>().currentPathIndex = currentIndex;
            enemy.GetComponent<Enemy>().enemySpawner = this;
            yield return new WaitForSeconds(delay);
        }
        
    }
}