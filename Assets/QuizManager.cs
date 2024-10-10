using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    // Lista kysymyksistä ja vastauksista (kustomoitu QuestionAndAnswers-luokka).
    public List<QuestionAndAnswers> QnA;

    // Taulukko vaihtoehtopainikkeista, joihin käyttäjä voi vastata.
    public GameObject[] options;

    // Muuttuja nykyisen kysymyksen indeksin tallentamiseksi.
    public int currentQuestion;

    // Viitteet peli- ja pelin loppu -paneeleihin.
    public GameObject Quizpanel;
    public GameObject GoPanel;

    // Tekstiobjektit, joissa näytetään kysymykset ja pisteet.
    public Text QuestionTxt;
    public Text ScoreTxt;

    // Kokonaismäärä kysymyksiä ja pelaajan pistemäärä.
    int totalQuestions = 0;
    public int score;

    // Alustusfunktio, joka suoritetaan pelin käynnistyessä.
    private void Start()
    {
        // Asetetaan kysymysten kokonaismäärä.
        totalQuestions = QnA.Count;

        // Piilotetaan pelin loppu -paneeli aluksi.
        GoPanel.SetActive(false);

        // Luodaan uusi kysymys.
        generateQuestion();
    }

    // Funktio, joka käynnistää pelin uudelleen.
    public void retry()
    {
        // Ladataan uudelleen nykyinen pelikohtaus.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Funktio pelin päättämiseksi, kun kaikki kysymykset on vastattu.
    void GameOver()
    {
        // Piilotetaan kyselypaneeli ja näytetään pelin loppu -paneeli.
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);

        // Päivitetään pisteteksti (pisteet / kokonaiskysymykset).
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    // Funktio oikean vastauksen käsittelemiseksi.
    public void correct()
    {
        // Kasvatetaan pistemäärää yhdellä.
        score += 1;

        // Poistetaan nykyinen kysymys listasta.
        QnA.RemoveAt(currentQuestion);

        // Luodaan uusi kysymys.
        generateQuestion();
    }

    // Funktio väärän vastauksen käsittelemiseksi.
    public void wrong()
    {
        // Poistetaan nykyinen kysymys listasta.
        QnA.RemoveAt(currentQuestion);

        // Luodaan uusi kysymys.
        generateQuestion();
    }

    // Asetetaan vastausvaihtoehdot.
    void SetAnswers()
    {
        // Käydään läpi kaikki vastausvaihtoehdot.
        for (int i = 0; i < options.Length; i++)
        {
            // Alustetaan vastaus vääräksi.
            options[i].GetComponent<AnswerScript>().isCorrect = false;

            // Asetetaan vaihtoehdon tekstiksi vastaus.
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            // Jos oikea vastaus löytyy, asetetaan isCorrect trueksi.
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    // Funktio uuden kysymyksen generoimiseksi.
    void generateQuestion()
    {
        // Jos kysymyksiä on jäljellä...
        if (QnA.Count > 0)
        {
            // Valitaan satunnainen kysymys listasta.
            currentQuestion = Random.Range(0, QnA.Count);

            // Asetetaan kysymysteksti.
            QuestionTxt.text = QnA[currentQuestion].Question;

            // Asetetaan vastausvaihtoehdot.
            SetAnswers();
        }
        else
        {
            // Jos kysymykset loppuvat, näytetään pelin loppu.
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}
