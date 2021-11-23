using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public QuizManager quizManager;
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();

    }

    void Update()
    {
        healthBar.fillAmount = quizManager.enemycurrentHealth / quizManager.enemyHealth;
    }
}
