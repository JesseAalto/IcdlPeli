using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    // T‰m‰ muuttuja tallentaa tiedon siit‰, onko vastaus oikein vai ei.
    public bool isCorrect = false;

    // Viittaus QuizManageriin, jotta voimme kutsua sen metodeja.
    public QuizManager quizManager;

    // Funktio, joka suoritetaan, kun k‰ytt‰j‰ valitsee vastauksen.
    public void Answer()
    {
        // Tarkistetaan, onko vastaus oikein.
        if (isCorrect)
        {
            // Jos vastaus on oikein, tulostetaan konsoliin "Correct Answer".
            Debug.Log("Correct Answer");

            // Kutsutaan QuizManagerin correct()-funktiota, joka p‰ivitt‰‰ pisteet ja generoi uuden kysymyksen.
            quizManager.correct();
        }
        else
        {
            // Jos vastaus on v‰‰rin, tulostetaan konsoliin "Wrong Answer".
            Debug.Log("Wrong Answer");

            // Kutsutaan QuizManagerin wrong()-funktiota, joka k‰sittelee v‰‰r‰n vastauksen.
            quizManager.wrong();
        }
    }
}

