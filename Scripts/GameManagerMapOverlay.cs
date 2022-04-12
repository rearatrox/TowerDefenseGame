using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerMapOverlay : MonoBehaviour
{

    public GameObject[] Level;
    public GameObject PopUpPanel;
    public GameObject SettingsPanel;
    public GameObject GesperrtPanel;
    public GameObject PopUpText;
    public GameObject PopUpTextGesperrt;
    public GameObject[] SterneLvl5;
    public GameObject[] SterneLvl4;
    public GameObject[] SterneLvl3;
    public GameObject[] SterneLvl2;
    public GameObject[] SterneLvl1;
    public GameObject[] PopUpSterneLvl1;
    public Material Gold;
    public Material Grau;
    public Material Standard;
    public Material Grün;

    [Header("Buttons")]
    public GameObject StartButton;
    public GameObject ZurückButton;
    public GameObject ZurückButtonGesperrt;
    public GameObject SchließenButton;
    public GameObject SchließenButtonGesperrt;
    public GameObject SettingsButton;
    public GameObject HomeButtonMap;

    public bool SchalteLevelFrei;
    private void Awake()
    {
       // PlayerPrefs.SetInt("Sterne", 0);
       SetzeOverlayObjekte();
        
       
       

    }

    public void Start()
    {
        PopUpPanel.SetActive(false);
        GesperrtPanel.SetActive(false);
        // zum Reseten
        //WerteReseten();
        Debug.Log("Höchstes level: " + PlayerPrefs.GetInt("HöchstesLevel"));
        SterneLvl1_Abruf();
        SterneLvl2_Abruf();
        SterneLvl3_Abruf();
        SterneLvl4_Abruf();
        SterneLvl5_Abruf();
        PopUpSterneLöschen();
        SterneAnzahl();

    }

    private void WerteReseten()
    {
          PlayerPrefs.SetInt("Sterne", 0);
          PlayerPrefs.SetInt("Level0", 0);
         PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
        PlayerPrefs.SetInt("Level3", 0);
        PlayerPrefs.SetInt("Level4", 0);
        PlayerPrefs.SetInt("HöchstesLevel", 0);
    }

    public void SetzeOverlayObjekte()
    {

        
        SterneLvl2[0] = GameObject.Find("1SternLvl2");
        SterneLvl2[1] = GameObject.Find("2SternLvl2");
        SterneLvl2[2] = GameObject.Find("3SternLvl2");

        SterneLvl3[0] = GameObject.Find("1SternLvl3");
        SterneLvl3[1] = GameObject.Find("2SternLvl3");
        SterneLvl3[2] = GameObject.Find("3SternLvl3");


        GesperrtPanel = GameObject.Find("Gesperrt_Fenster");
        PopUpPanel = GameObject.Find("PopUp_Fenster");
        PopUpText = GameObject.Find("Fenstertitel");
        PopUpTextGesperrt = GameObject.Find("Fenstertitel_gesperrt");


        //Wird in den jeweiligen LevelSkripten benötigt
        StartButton = GameObject.Find("StartButton");
        ZurückButton = GameObject.Find("Button_Cancel");
        ZurückButtonGesperrt = GameObject.Find("Button_Cancel_gesperrt");
        SchließenButton = GameObject.Find("Button_WindowClose");
        SchließenButtonGesperrt = GameObject.Find("Button_WindowClose_gesperrt");
        //SettingsButton noch nicht mit Funktion ausgestattet
        SettingsButton = GameObject.Find("Button_Settings");
        HomeButtonMap = GameObject.Find("Button_Home");

        if (HomeButtonMap != null)
            HomeButtonMap.GetComponent<Button>().onClick.AddListener(HomeVonMap);
        if (ZurückButton != null)
        {
           
            ZurückButton.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().MapOverlayButton);
            ZurückButton.GetComponent<Button>().onClick.AddListener(PopUpSterneLöschen);
            
        }
           
        if (ZurückButtonGesperrt != null)
        {
            ZurückButtonGesperrt.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().MapOverlayButton);
        }
            
        if (SchließenButtonGesperrt != null)
            SchließenButtonGesperrt.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().MapOverlayButton);

        if (SchließenButton != null)
        {
            SchließenButton.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().MapOverlayButton);
            SchließenButton.GetComponent<Button>().onClick.AddListener(PopUpSterneLöschen);
        }
           


    }
    public void ScaleDown()
    {
        LeanTween.scale(PopUpPanel, new Vector3(0, 0, 0), 0.5f);
        PopUpPanel.SetActive(false);
    }

    /* public void Lvl1_StartKnopf()
     {
         FindObjectOfType<GameManager>().StartLvl1();
     }

     public void Lvl2_StartKnopf()
     {

         FindObjectOfType<GameManager>().StartLvl2();
     }*/

    public void PopUpSchließen()
    {
      PopUpPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void SettingsStarten()
    {
        SettingsPanel.SetActive(true);
    }
    public void HomeVonMap()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void SterneAnzahl()
    {
       
        GameObject SterneAnzahl = GameObject.Find("TextSterne");
        SterneAnzahl.GetComponent<Text>().text = PlayerPrefs.GetInt("Sterne").ToString();
    }


    public void SterneLvl1_Abruf()
    {
        Level[0] = GameObject.Find("Lvl1");
         if (PlayerPrefs.GetInt("HöchstesLevel") == FindObjectOfType<Lvl1Script>().level || PlayerPrefs.GetInt("HöchstesLevel") == 0)
        {
            Level[0].GetComponent<MeshRenderer>().material = Gold;
        }
        else if (PlayerPrefs.GetInt("HöchstesLevel") >= FindObjectOfType<Lvl1Script>().level)
        {
         
            Level[0].GetComponent<MeshRenderer>().material = Grün;
        }
        
        else
        {
            Level[0].GetComponent<MeshRenderer>().material = Standard;
        }

        // reseten
        // PlayerPrefs.SetInt("Level0", 0);
        for (int i = 0; i < PlayerPrefs.GetInt("Level0"); i++)
        {
            if (i < PlayerPrefs.GetInt("Level0"))
            {
                SterneLvl1[i].GetComponent<MeshRenderer>().material = Gold;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                SterneLvl1[i].GetComponent<MeshRenderer>().material = Grau;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
            }
        }
    }




    public void SterneLvl2_Abruf()
    {
        Level[1] = GameObject.Find("Lvl2");
         if (PlayerPrefs.GetInt("HöchstesLevel") == FindObjectOfType<Lvl2Script>().level)
        {
            Level[1].GetComponent<MeshRenderer>().material = Gold;
        }
        else if (PlayerPrefs.GetInt("HöchstesLevel") >= FindObjectOfType<Lvl2Script>().level)
        {
            
            Level[1].GetComponent<MeshRenderer>().material = Grün;
        }
        
        else
        {
            Level[1].GetComponent<MeshRenderer>().material = Standard;
        }

        // reseten
        // PlayerPrefs.SetInt("Level1", 0);
        for (int i = 0; i < PlayerPrefs.GetInt("Level1"); i++)
        {
            if (i < PlayerPrefs.GetInt("Level1"))
            {
                SterneLvl2[i].GetComponent<MeshRenderer>().material = Gold;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                SterneLvl2[i].GetComponent<MeshRenderer>().material = Grau;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void SterneLvl3_Abruf()
    {
        //Zahl des Levels einfärben
        Level[2] = GameObject.Find("Lvl3");
        if (PlayerPrefs.GetInt("HöchstesLevel") == FindObjectOfType<Lvl3Script>().level)
        {
            Level[2].GetComponent<MeshRenderer>().material = Gold;
        }
        else if (PlayerPrefs.GetInt("HöchstesLevel") >= FindObjectOfType<Lvl3Script>().level)
        {

            Level[2].GetComponent<MeshRenderer>().material = Grün;
        }
        
        else
        {
            Level[2].GetComponent<MeshRenderer>().material = Standard;
        }

        //Die Sterne Gold färben
        for (int i = 0; i < PlayerPrefs.GetInt("Level2"); i++)
        {
            if (i < PlayerPrefs.GetInt("Level2"))
            {
                SterneLvl3[i].GetComponent<MeshRenderer>().material = Gold;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                SterneLvl3[i].GetComponent<MeshRenderer>().material = Grau;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void SterneLvl4_Abruf()
    {
        //Zahl des Levels einfärben
        Level[3] = GameObject.Find("Lvl4");
        if (PlayerPrefs.GetInt("HöchstesLevel") == FindObjectOfType<Lvl4Script>().level)
        {
            Level[3].GetComponent<MeshRenderer>().material = Gold;
        }
        else if (PlayerPrefs.GetInt("HöchstesLevel") >= FindObjectOfType<Lvl4Script>().level)
        {

            Level[3].GetComponent<MeshRenderer>().material = Grün;
        }
        else
        {
            Level[3].GetComponent<MeshRenderer>().material = Standard;
        }

        //Die Sterne Gold färben
        for (int i = 0; i < PlayerPrefs.GetInt("Level3"); i++)
        {
            if (i < PlayerPrefs.GetInt("Level3"))
            {
                SterneLvl4[i].GetComponent<MeshRenderer>().material = Gold;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                SterneLvl4[i].GetComponent<MeshRenderer>().material = Grau;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void SterneLvl5_Abruf()
    {
        //Zahl des Levels einfärben
        Level[4] = GameObject.Find("Lvl5");
        if (PlayerPrefs.GetInt("HöchstesLevel") == FindObjectOfType<Lvl5Script>().level)
        {
            Level[4].GetComponent<MeshRenderer>().material = Gold;
        }
        else if (PlayerPrefs.GetInt("HöchstesLevel") >= FindObjectOfType<Lvl5Script>().level)
        {

            Level[4].GetComponent<MeshRenderer>().material = Grün;
        }
        else
        {
            Level[4].GetComponent<MeshRenderer>().material = Standard;
        }

        //Die Sterne Gold färben
        for (int i = 0; i < PlayerPrefs.GetInt("Level4"); i++)
        {
            if (i < PlayerPrefs.GetInt("Level4"))
            {
                SterneLvl5[i].GetComponent<MeshRenderer>().material = Gold;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                SterneLvl5[i].GetComponent<MeshRenderer>().material = Grau;
                PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void PopUpSterneLöschen()
    {
        for(int i = 0; i < 3; i++)
        {
            PopUpSterneLvl1[i].GetComponent<Image>().color = Color.black;
        }
    }

    

}
