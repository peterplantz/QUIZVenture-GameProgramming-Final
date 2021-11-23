using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public string nameOfplayer;
    public string saveName;

    public string scoreOfplayer;
    public string saveScore;

    public Text inputText;
    public Text loadedName;
    
    public Text loadedScore;

    public float sScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameOfplayer = PlayerPrefs.GetString("name", "none");
        loadedName.text = nameOfplayer;
        NameLoad.playernamestr = loadedName.text;
        scoreOfplayer = PlayerPrefs.GetInt($"{PlayerPrefs.GetString("name")}score").ToString();
        loadedScore.text = scoreOfplayer;
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name" , saveName);

    }
    
    public void SetScore()
    {
        saveScore = scoreOfplayer;
        PlayerPrefs.SetString($"{PlayerPrefs.GetString("name")}score", saveScore);
    }

}
