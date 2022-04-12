using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FunktionenDieZuStartGeladenWerden : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<GameManager>().SetzeObjects();
    }

    private void Start()
    {
        FindObjectOfType<GameManager>().SetzeElementefalse();
    }
}
