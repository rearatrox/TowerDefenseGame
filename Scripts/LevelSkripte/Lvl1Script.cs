using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lvl1Script : MonoBehaviour
{
    public int level = 3;
    private void OnMouseUp()
    {
        // FindObjectOfType<GameManager>().SetzeElementefalse();
        if (PlayerPrefs.GetInt("HöchstesLevel") <= level)
        {
            PlayerPrefs.SetInt("HöchstesLevel", level);
        }

        if (PlayerPrefs.GetInt("HöchstesLevel") == level)
            FindObjectOfType<GameManager>().SchonGespielt = false;
        else
            FindObjectOfType<GameManager>().SchonGespielt = true;



        FindObjectOfType<GameManagerMapOverlay>().SterneLvl1_Abruf();
        FindObjectOfType<GameManagerMapOverlay>().PopUpPanel.SetActive(true);
        LeanTween.scale(FindObjectOfType<GameManagerMapOverlay>().PopUpPanel, new Vector3(1, 1, 1), .5f);
            FindObjectOfType<GameManagerMapOverlay>().PopUpText.GetComponent<Text>().text = gameObject.tag;
            FindObjectOfType<GameManagerMapOverlay>().StartButton.GetComponent<Button>().onClick.AddListener
            (FindObjectOfType<GameManager>().StartLvl1);
    }
}
