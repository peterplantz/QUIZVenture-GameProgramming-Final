using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public QuizManager quizManager;
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = quizManager.currentHealth / quizManager.playerHealth;
    }
}
