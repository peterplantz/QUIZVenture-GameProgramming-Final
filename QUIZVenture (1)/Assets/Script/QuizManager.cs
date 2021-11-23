using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public Text QuestionTxt;

    public GameObject quizPanel;
    public GameObject gameoverPanel;
    public GameObject deadPanel;

    public Animator playerAttack;
    public Animator playerHit;
    public Animator enemyAttack;
    public Animator enemyHit;

    public float playerHealth = 4f;
    public float currentHealth;
    public float enemyHealth = 10f;
    public float enemycurrentHealth;

    [SerializeField] Image timeImage;
    [SerializeField] TMP_Text timeText;
    public float duration = 30f;
    public float timeLeft;
    public float timeMultiply = 10f;

    public TMP_Text scoreText;
    public TMP_Text totalScore;
    public TMP_Text totalScore_over;
    public float scoreCount;

    public GameObject healButton;
    public GameObject freeattackButton;

    public string updatescore;
    public AudioSource nice;
    public AudioSource hurt;
    public AudioSource failed;
    public AudioSource bg;

    private void Start()
    {
        gameoverPanel.SetActive(false);
        deadPanel.SetActive(false);
        generateQuestion();
        timeLeft = duration;
        timeText.text = timeLeft.ToString();
        scoreText.text = "Score " + (int)scoreCount;
        currentHealth = playerHealth;
        enemycurrentHealth = enemyHealth;

    }

    public void GameOver()
    {
        quizPanel.SetActive(false);
        totalScore.text = "Your score " + (int)scoreCount;
        gameoverPanel.SetActive(true);
        PlayerPrefs.SetInt($"{PlayerPrefs.GetString("name")}score", PlayerPrefs.GetInt($"{PlayerPrefs.GetString("name")}score") + (int)scoreCount);
    }

    public void DeadScene()
    {
        quizPanel.SetActive(false);
        totalScore_over.text = "Your score " + (int)scoreCount;
        deadPanel.SetActive(true);
        bg.Stop();
        failed.Play();

        PlayerPrefs.SetInt($"{PlayerPrefs.GetString("name")}score", PlayerPrefs.GetInt($"{PlayerPrefs.GetString("name")}score") - (int)scoreCount);
    }

    public void correct()
    {
        scoreText.text = "Score " + (int)scoreCount;
        scoreCount += timeMultiply * timeLeft;
        Debug.Log(scoreCount);

        playerAttack.SetTrigger("isAttack");
        enemyHit.SetTrigger("isHit");
        enemycurrentHealth -= 1;
        Debug.Log(enemycurrentHealth);
        nice.Play();

        QnA.RemoveAt(currentQuestion);
        generateQuestion();

        isEnemyDead();
    }

    public void wrong()
    {

        playerHit.SetTrigger("isHit");
        enemyAttack.SetTrigger("isAttack");
        currentHealth -= 1;

        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        hurt.Play();

        isPlayerDead();
    }

    public void Heal()
    {
        currentHealth += 2;
        healButton.gameObject.SetActive(false);
        isFullHealth();
        Debug.Log(currentHealth);
    }

    public void FreeAttack()
    {
        playerAttack.SetTrigger("isAttack");
        enemyHit.SetTrigger("isHit");
        enemycurrentHealth -= 1;
        freeattackButton.gameObject.SetActive(false);
        Debug.Log(enemycurrentHealth);
        isEnemyDead();
    }

    public void isFullHealth()
    {
        if (currentHealth >= playerHealth)
        {
            currentHealth = playerHealth;
        }
    }

    public void isPlayerDead()
    {
        if (currentHealth <= 0)
        {
            DeadScene();
        }
    }

    public void isEnemyDead()
    {
        if (enemycurrentHealth <= 0)
        {
            GameOver();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswers == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
            timeLeft = duration;
        }
        else
        {
            Debug.Log("Out of Question");
            GameOver();
        }

    }
    void Update()
    {
        timeLeft -= 1 * Time.deltaTime;
        timeText.text = timeLeft.ToString("0");
        timeImage.fillAmount = Mathf.InverseLerp(0, duration, timeLeft);
        scoreText.text = "Score " + (int)scoreCount;
        updatescore = PlayerPrefs.GetInt($"{PlayerPrefs.GetString("name")}score").ToString();
        NameLoad.userScorestr = updatescore;
    }
}
