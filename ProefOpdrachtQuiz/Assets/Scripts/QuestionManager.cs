using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private CSVReader cvsReader;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI answerA;
    [SerializeField] private TextMeshProUGUI answerB;
    [SerializeField] private TextMeshProUGUI answerC;
    [SerializeField] private TextMeshProUGUI answerD;
    
    [Header("Sfx")]
    [SerializeField] private AudioClip correctSfx;
    [SerializeField] private AudioClip wrongSfx;
    [SerializeField] private AudioClip confettiSfx;
    [SerializeField] private AudioSource source;
    
    [Header("End Screen")]
    [SerializeField] private GameObject hideOnEnd;
    [SerializeField] private GameObject showOnEnd;
    [SerializeField] private GameObject confetti;
    private int currentQuestion;
    private int pointsForCurrentQuestion;
    private int totalPoints;
    private int maxAchievablePoints;
    private string correctAnswer;
    
    void Start()
    {
        currentQuestion = 1;
        UpdateQuestion();
        
        print(cvsReader.GetRowCount());
    }

    public void CheckAnswer(string answer)
    {
        if (answer == correctAnswer)
        {
            if (currentQuestion == (cvsReader.GetRowCount()-1))
            {
                EndQuiz();
                totalPoints += pointsForCurrentQuestion;
                UpdateQuestion();
                source.PlayOneShot(confettiSfx);
            }
            else
            {
                totalPoints += pointsForCurrentQuestion;
                currentQuestion += 1;
                UpdateQuestion();
            }
            
            source.PlayOneShot(correctSfx);
        }
        else
        {
            if (currentQuestion == (cvsReader.GetRowCount()-1))
            {
                EndQuiz();
                UpdateQuestion();
                source.PlayOneShot(confettiSfx);
            }
            else
            {
                currentQuestion += 1;
                UpdateQuestion();
            }
            
            source.PlayOneShot(wrongSfx);
        }
    }

    void EndQuiz()
    {
        print("EndQuiz");
        hideOnEnd.SetActive(false);
        showOnEnd.SetActive(true);
        confetti.SetActive(true);
        float finalScore = ((float)totalPoints / maxAchievablePoints) * 10f;
        finalScoreText.text = ("Cijfer:\n" + Math.Round(finalScore,1) + "/10");
    }

    void UpdateQuestion()
    {
        questionText.text = cvsReader.GetCell(currentQuestion, 0);
        answerA.text = cvsReader.GetCell(currentQuestion, 1);
        answerB.text = cvsReader.GetCell(currentQuestion, 2);
        answerC.text = cvsReader.GetCell(currentQuestion, 3);
        answerD.text = cvsReader.GetCell(currentQuestion, 4);
        correctAnswer = cvsReader.GetCell(currentQuestion, 5);
        string questionPoints = (cvsReader.GetCell(currentQuestion, 6));
        if (int.TryParse(questionPoints, out pointsForCurrentQuestion))
        {
            print("succesfully changed questionpoints to int");
        }
        
        pointText.text = ("Totaal punten: " + totalPoints);

        if(currentQuestion != (cvsReader.GetRowCount()-1))
            maxAchievablePoints += pointsForCurrentQuestion;
        print("maxachievablepoints: "+maxAchievablePoints);
    }
}
