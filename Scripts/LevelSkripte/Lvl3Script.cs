using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl3Script : MonoBehaviour
{
   public int level = 5;

    private void OnMouseUp()
    {
        Debug.Log(PlayerPrefs.GetInt("HöchstesLevel"));
        if (level <= PlayerPrefs.GetInt("HöchstesLevel"))
        {
            if (PlayerPrefs.GetInt("HöchstesLevel") == level)
                FindObjectOfType<GameManager>().SchonGespielt = false;
            else
                FindObjectOfType<GameManager>().SchonGespielt = true;

            FindObjectOfType<GameManagerMapOverlay>().SterneLvl3_Abruf();
            FindObjectOfType<GameManagerMapOverlay>().PopUpPanel.SetActive(true);
            LeanTween.scale(FindObjectOfType<GameManagerMapOverlay>().PopUpPanel, new Vector3(1, 1, 1), .5f);
            FindObjectOfType<GameManagerMapOverlay>().PopUpText.GetComponent<Text>().text = gameObject.tag;
            FindObjectOfType<GameManagerMapOverlay>().StartButton.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().StartLvl3);
        }
        else
        {
            FindObjectOfType<GameManagerMapOverlay>().GesperrtPanel.SetActive(true);
            FindObjectOfType<GameManagerMapOverlay>().PopUpTextGesperrt.GetComponent<Text>().text = gameObject.tag;
        }

    }
}
