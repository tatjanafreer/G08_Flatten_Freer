using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionandAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GOPanel;

    public Text QuestionText;
    public Text ScoreText;

    int totalQuestions = 0;
    public int score;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     void GameOver()
    {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreText.text = score + "/" +  totalQuestions;
    }

    public void Correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());

    }

    public void Wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());

    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answer[i];
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;

        if (QnA[currentQuestion].CorrectAnswer == i+1)
                    {
            options[i].GetComponent<AnswerScript>().isCorrect = true;
        }
        }
    }
    void generateQuestion()
    {
        if(QnA.Count > 0)
        { 
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionText.text = QnA[currentQuestion].Question;
        SetAnswers();
        }
        else
        {
            Debug.Log("Out of Question");
            GameOver();
        }

    }




}
