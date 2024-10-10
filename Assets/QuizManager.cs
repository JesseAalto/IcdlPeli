using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    // Lista kysymyksist‰ ja vastauksista (kustomoitu QuestionAndAnswers-luokka).
    public List<QuestionAndAnswers> QnA;

    // Taulukko vaihtoehtopainikkeista, joihin k‰ytt‰j‰ voi vastata.
    public GameObject[] options;

    // Muuttuja nykyisen kysymyksen indeksin tallentamiseksi.
    public int currentQuestion;

    // Viitteet peli- ja pelin loppu -paneeleihin.
    public GameObject Quizpanel;
    public GameObject GoPanel;

    // Tekstiobjektit, joissa n‰ytet‰‰n kysymykset ja pisteet.
    public Text QuestionTxt;
    public Text ScoreTxt;

    // Kokonaism‰‰r‰ kysymyksi‰ ja pelaajan pistem‰‰r‰.
    int totalQuestions = 0;
    public int score;

    // Alustusfunktio, joka suoritetaan pelin k‰ynnistyess‰.
    private void Start()
    {
        // Asetetaan kysymysten kokonaism‰‰r‰.
        totalQuestions = QnA.Count;

        // Piilotetaan pelin loppu -paneeli aluksi.
        GoPanel.SetActive(false);

        // Luodaan uusi kysymys.
        generateQuestion();
    }

    // Funktio, joka k‰ynnist‰‰ pelin uudelleen.
    public void retry()
    {
        // Ladataan uudelleen nykyinen pelikohtaus.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Funktio pelin p‰‰tt‰miseksi, kun kaikki kysymykset on vastattu.
    void GameOver()
    {
        // Piilotetaan kyselypaneeli ja n‰ytet‰‰n pelin loppu -paneeli.
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);

        // P‰ivitet‰‰n pisteteksti (pisteet / kokonaiskysymykset).
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    // Funktio oikean vastauksen k‰sittelemiseksi.
    public void correct()
    {
        // Kasvatetaan pistem‰‰r‰‰ yhdell‰.
        score += 1;

        // Poistetaan nykyinen kysymys listasta.
        QnA.RemoveAt(currentQuestion);

        // Luodaan uusi kysymys.
        generateQuestion();
    }

    // Funktio v‰‰r‰n vastauksen k‰sittelemiseksi.
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
        // K‰yd‰‰n l‰pi kaikki vastausvaihtoehdot.
        for (int i = 0; i < options.Length; i++)
        {
            // Alustetaan vastaus v‰‰r‰ksi.
            options[i].GetComponent<AnswerScript>().isCorrect = false;

            // Asetetaan vaihtoehdon tekstiksi vastaus.
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            // Jos oikea vastaus lˆytyy, asetetaan isCorrect trueksi.
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    // Funktio uuden kysymyksen generoimiseksi.
    void generateQuestion()
    {
        // Jos kysymyksi‰ on j‰ljell‰...
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
            // Jos kysymykset loppuvat, n‰ytet‰‰n pelin loppu.
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}
