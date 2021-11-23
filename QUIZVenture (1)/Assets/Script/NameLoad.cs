using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameLoad : MonoBehaviour
{
    public static string playernamestr;
    public Text playername;
    
    public static float scoreCount;
    public Text userScore;
    public static string userScorestr;

    // Start is called before the first frame update
    void Start()
    {
        playername.text = playernamestr;
        userScore.text = userScorestr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
