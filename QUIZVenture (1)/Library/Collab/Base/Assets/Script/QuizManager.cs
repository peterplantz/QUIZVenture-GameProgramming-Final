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

    public int playerHealth = 4;
    public int currentHealth;
    public int enemyHealth = 10;
    public int enemycurrentHealth;

    [SerializeField] Image timeImage;
    [SerializeField] TMP_Text timeText;
    public float duration = 30f;
    public float timeLeft;
    public float timeMultiply = 10f;

    public TMP_Text scoreText;
    public TMP_Text totalScore;
    public float scoreCount;

    public GameObject healButton;
    public GameObject doubledamageButton;

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
    }

    public void DeadScene()
    {
        quizPanel.SetActive(false);
        totalScore.text = "Your score " + (int)scoreCount;
        deadPanel.SetActive(true);
    }

    public void correct()
    {
        scoreText.text = "Score " + (int)scoreCount;
        scoreCount += timeMultiply * timeLeft;
        Debug.Log(scoreCount);

        playerAttack.SetTrigger("isAttack");
        enemyHit.SetTrigger("isHit");
        enemycurrentHealth -= 1;

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

        isPlayerDead();
    }

    public void isTimeout()
    {
        if (timeLeft <= 0)
        {
            playerHit.SetTrigger("isHit");
            enemyAttack.SetTrigger("isAttack");
            currentHealth -= 1;

            QnA.RemoveAt(currentQuestion);
            generateQuestion();

            isPlayerDead();
        }
    }

    public void Heal()
    {
        currentHealth += 2;
    }

    public void DoubleDamage()
    {
        enemycurrentHealth -= 1;
    }

    public void CutChoice()
    {

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
    }
}
