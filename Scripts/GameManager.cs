using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using JetBrains.Annotations;



public class GameManager : MonoBehaviour
{
    
    public int coins = 200;
   // public GameObject Transition;
   // public Animator TransAnim;


    public bool gameOver = false;
    public int wave = 1;
    public int toteGegner = 0;
    public int AnzSpawns = 0;
    public int AnzSpawnsMax = 5;
    public int AnzSpawnsCountdownStart;
    public int AnzSpawnsErhöhen;
    public float countdownZeit;
    public bool startTimer = false;
    public bool startroutine = true;
   
    public int Leben = 5;
    public int LebenMax = 5;
    public int coinsProKill;
    public float SpeedFaktorGegner;
    public float SpeedFaktorGegnerVerringern;
    public int wave3Sterne;
    public int wave2Sterne;
    public int wave1Sterne;

    public float Spawnzeit;
    public float SpawnzeitVerringern;

    [Header("Button")]
    public GameObject[] TurmButtons;
    public GameObject aktTurmBut;
    public GameObject oldTurmBut;
    public GameObject NeustartButton;
    public GameObject ZurückButton;
    public GameObject HomeButton;
    public GameObject MapOverlayButton1;
    public GameObject EinfahrenButton;
    public GameObject AusfahrenButton;
    public GameObject SchließenButton;
    public GameObject JaButton;
    public GameObject QuitButton;
    public int TurmButtonIndex;
    // public GameObject CanonturmPre;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject WavePanel;
    public GameObject AusfahrenPanel;
    public GameObject AuswahlMenuTowerPlacement;
    public GameObject[] Medal;
    public GameObject BistDuSicherPanel;

    public GameObject coinsLabel;
    public GameObject LebenText;
    public GameObject waveText;
    public GameObject PopUpText;
    public GameObject[] PreisText;

    public bool SchonGespielt;
    private bool HöchstesLevelEinmaligSetzen;
    public int VergleichMitSpeicherwert;
    public int aktuellesLevel;
    public int Kosten;
    int i;

    public float timer = 1.0f;

    // Start is called before the first frame update
    void Start()
       
    {
        AudioManager.instance.BackgroundMusic.Play();
        SetzeObjects();
        SetzeElementefalse();
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        SetWave(wave);
        ShowCoins();
        ShowLeben();
        ShowWave();
        aktuelleOutlineButtonSetzen();
        timer -= Time.deltaTime;
        

    }
    
    public void SetzeElementefalse()
    {
        if(gameOverPanel != null || WavePanel != null || AusfahrenPanel != null)
        {
            gameOverPanel.SetActive(false);
            WavePanel.SetActive(false);
            AusfahrenPanel.SetActive(false);
            BistDuSicherPanel.SetActive(false);
        }
       
    }

    public void SetzeElementetrue()
    {
        if (gameOverPanel != null || WavePanel != null || AusfahrenPanel != null)
        {
            gameOverPanel.SetActive(true);
            WavePanel.SetActive(true);
            AusfahrenPanel.SetActive(true);
        }

    }

    public void SetzeObjects()
    {
        //Texte/Labels
        LebenText = GameObject.FindGameObjectWithTag("lebenText");
        waveText = GameObject.FindGameObjectWithTag("waveText");
        coinsLabel = GameObject.FindGameObjectWithTag("CoinsLabel");
        PopUpText = GameObject.FindGameObjectWithTag("PopUpText");
        PreisText[0] = GameObject.FindGameObjectWithTag("PreisText1");
        PreisText[3] = GameObject.FindGameObjectWithTag("PreisText3");

      //  Transition = GameObject.Find("LevelLoader");

        //Buttons
        TurmButtons[0] = GameObject.FindGameObjectWithTag("BogenturmButton");
        TurmButtons[1] = GameObject.FindGameObjectWithTag("CanonturmButton");
        aktTurmBut = GameObject.FindGameObjectWithTag("BogenturmButton");
        HomeButton = GameObject.FindGameObjectWithTag("HomeButton");
        MapOverlayButton1 = GameObject.FindGameObjectWithTag("MapOverlayButton");
        NeustartButton = GameObject.FindGameObjectWithTag("ButtonNeustart");
        ZurückButton = GameObject.FindGameObjectWithTag("ButtonZurück");
        EinfahrenButton = GameObject.FindGameObjectWithTag("EinfahrenButton");
        AusfahrenButton = GameObject.FindGameObjectWithTag("AusfahrenButton");
        SchließenButton = GameObject.FindGameObjectWithTag("SchließenButton");
        JaButton = GameObject.Find("JaBistDuSicher");
        QuitButton = GameObject.Find("Button_Quit");
        //Panels
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        BistDuSicherPanel = GameObject.FindGameObjectWithTag("BistDuSicherPanel");
        WavePanel = GameObject.FindGameObjectWithTag("WavePanel");
        AusfahrenPanel = GameObject.FindGameObjectWithTag("AusfahrenPanel");
        AuswahlMenuTowerPlacement = GameObject.FindGameObjectWithTag("AuswahlPanel");

        Medal[0] = GameObject.FindGameObjectWithTag("Medal1");
        Medal[1] = GameObject.FindGameObjectWithTag("Medal2");
        Medal[2] = GameObject.FindGameObjectWithTag("Medal3");

       // TransAnim =  Transition.transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        if(HomeButton != null)
             HomeButton.GetComponent<Button>().onClick.AddListener(Home);
        
        if (TurmButtons[0] != null)
            TurmButtons[0].GetComponent<Button>().onClick.AddListener(BogenturmButtonDrücken);
        
        if (TurmButtons[1] != null)
            TurmButtons[1].GetComponent<Button>().onClick.AddListener(CanonturmButtonDrücken);
        
        if ( EinfahrenButton!= null)
            EinfahrenButton.GetComponent<Button>().onClick.AddListener(AuswahlMenuTowerPlacementEinfahren);
        
        if (AusfahrenButton != null)

            AusfahrenButton.GetComponent<Button>().onClick.AddListener(AusfahrenPanelAusfahren);
        
        if (ZurückButton != null)
            ZurückButton.GetComponent<Button>().onClick.AddListener(MapOverlayButton);

        if (NeustartButton != null)
            NeustartButton.GetComponent<Button>().onClick.AddListener(Restart);
       
        if (MapOverlayButton1 != null)
            MapOverlayButton1.GetComponent<Button>().onClick.AddListener(BistDuSicherPanelÖffnen);
        if (SchließenButton != null)
            SchließenButton.GetComponent<Button>().onClick.AddListener(FensterSchließen);
        if (JaButton != null)
            JaButton.GetComponent<Button>().onClick.AddListener(MapOverlayButton);
        if (QuitButton != null)
            QuitButton.GetComponent<Button>().onClick.AddListener(Quit);
    }

  /* IEnumerator LoadLevel(int levelIndex)
    {
        TransAnim.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(levelIndex);
    } */



    public void SetLeben(int value)
    {
        Leben = value;

        if(Leben <= 0 && !gameOver)
        {
            gameOver = true;
            Time.timeScale = 0;
            WinBildschirm();
           
        }
    }

    public void SetWave(int value)
    {
        wave = value;

        if (toteGegner >= AnzSpawnsCountdownStart)
        {
            startTimer = true;
            
           if(WavePanel != null && startTimer == true)
            {
                WavePanel.SetActive(true);
                if (countdownZeit < 0)
                    newWave();
            }
            
        }

    }

    public void newWave()
    {
        if(Spawnzeit>0.3f)
                Spawnzeit -= SpawnzeitVerringern;
        if (SpeedFaktorGegner > 0.4f)
            if (wave % 2 == 0)
                SpeedFaktorGegner -= SpeedFaktorGegnerVerringern;
        //FindObjectOfType<GegnerMovelvl1>().speed += 0.1f;
        LebenMax = Leben;
        
        countdownZeit = 5.0f;
        FindObjectOfType<Spawner>().i = 0;
        FindObjectOfType<Spawner>().startCou = true;
        toteGegner = 0;
        startroutine = true;
        startTimer = false;
        AnzSpawns = 0;
        if(AnzSpawnsMax <= 20)
            if(wave % 2 == 0)
                AnzSpawnsMax += AnzSpawnsErhöhen;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        wave++;
        WavePanel.SetActive(false);
    }

    public void ShowWave()
    {
        if (waveText != null)
            waveText.GetComponent<Text>().text = "Welle " + wave.ToString(); 
    }

    public void ShowLeben()
    {
        if(LebenText != null)
             LebenText.GetComponent<Text>().text = Leben.ToString();
    }

    public void SetCoins(int value)
    {
        coins = value;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void ShowCoins()
    {
        if(coinsLabel != null)
            coinsLabel.GetComponent<Text>().text = coins.ToString();
    }



    //Versch. Level laden bzw. pausieren
    public void Quit()
    {
        Application.Quit();
    }
    public void Home()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("HomeScreen");
    }

    public void Restart()
    {

        if (SceneManager.GetActiveScene().name == "Level0")
            StartLvl1();
        else if (SceneManager.GetActiveScene().name == "Level1")
            StartLvl2();
        Time.timeScale = 1;

    }

    public void StartLvl1()
    {
        startTimer = false;
        gameOver = false;
        startroutine = true;
        Leben = 5;
        LebenMax = 5;
        coins = 250;
        wave = 1;
        toteGegner = 0;
        AnzSpawns = 0;
        AnzSpawnsMax = 5;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        AnzSpawnsErhöhen = 2;
        wave1Sterne = 3;
        wave2Sterne = 5;
        wave3Sterne = 8;
        
        coinsProKill = 10;
        SpeedFaktorGegner = 1.4f;
        SpeedFaktorGegnerVerringern = 0.2f;
        Spawnzeit = 2;
        SpawnzeitVerringern = 0.15f;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        // StartCoroutine(LoadLevel(3));
        SceneManager.LoadScene(3);


    }

    public void StartLvl2()
    {
        startTimer = false;
        gameOver = false;
        startroutine = true;
        Leben = 5;
        LebenMax = 5;
        coins = 250;
        wave = 1;
        toteGegner = 0;
        AnzSpawns = 0;
        AnzSpawnsMax = 5;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        AnzSpawnsErhöhen = 2;
        wave1Sterne = 5;
        wave2Sterne = 7;
        wave3Sterne = 11;
        coinsProKill = 15;
        SpeedFaktorGegner = 1.4f;
        SpeedFaktorGegnerVerringern = 0.2f;
        Spawnzeit = 1.8f;
        SpawnzeitVerringern = 0.15f;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(4);

    }

    public void StartLvl3()
    {
        startTimer = false;
        gameOver = false;
        startroutine = true;
        Leben = 5;
        LebenMax = 5;
        coins = 250;
        wave = 1;
        toteGegner = 0;
        AnzSpawns = 0;
        AnzSpawnsMax = 5;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        AnzSpawnsErhöhen = 3;
        wave1Sterne = 3;
        wave2Sterne = 5;
        wave3Sterne = 8;

        coinsProKill = 10;
        SpeedFaktorGegner = 1.4f;
        SpeedFaktorGegnerVerringern = 0.2f;
        Spawnzeit = 1.8f;
        SpawnzeitVerringern = 0.15f;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(5);


    }

    public void StartLvl4()
    {
        startTimer = false;
        gameOver = false;
        startroutine = true;
        Leben = 5;
        LebenMax = 5;
        coins = 250;
        wave = 1;
        toteGegner = 0;
        AnzSpawns = 0;
        AnzSpawnsMax = 5;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        AnzSpawnsErhöhen = 2;
        wave1Sterne = 3;
        wave2Sterne = 5;
        wave3Sterne = 8;

        coinsProKill = 10;
        SpeedFaktorGegner = 1.4f;
        SpeedFaktorGegnerVerringern = 0.2f;
        Spawnzeit = 2;
        SpawnzeitVerringern = 0.15f;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(6);


    }

    public void StartLvl5()
    {
        startTimer = false;
        gameOver = false;
        startroutine = true;
        Leben = 5;
        LebenMax = 5;
        coins = 250;
        wave = 1;
        toteGegner = 0;
        AnzSpawns = 0;
        AnzSpawnsMax = 5;
        AnzSpawnsCountdownStart = AnzSpawnsMax - 2;
        AnzSpawnsErhöhen = 2;
        wave1Sterne = 8;
        wave2Sterne = 10;
        wave3Sterne = 13;

        coinsProKill = 10;
        SpeedFaktorGegner = 1.4f;
        SpeedFaktorGegnerVerringern = 0.2f;
        Spawnzeit = 2;
        SpawnzeitVerringern = 0.15f;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(7);


    }


    //Panels steuern
    public void AusfahrenPanelAusfahren()
    {
        AuswahlMenuTowerPlacement.SetActive(true);
        LeanTween.moveY(AuswahlMenuTowerPlacement, -5, .3f);
        AusfahrenPanel.SetActive(false);
    }


    public void AuswahlMenuTowerPlacementEinfahren()
    {
        // AuswahlMenuTowerPlacement.SetActive(false);
        StartCoroutine(Lol());
        
    }

    IEnumerator Lol()
    {
        LeanTween.moveY(AuswahlMenuTowerPlacement, -280, .3f);
        yield return new WaitForSeconds(.3f);
        AusfahrenPanel.SetActive(true);
    }




    //Knöpfe drücken
    public void BogenturmButtonDrücken()
    {
        TurmButtonIndex = 0;
        aktuellesLevel = 1;
        //aktTurmBut = BogenTurmBut;
    }
    public void CanonturmButtonDrücken()
    {
        TurmButtonIndex = 1;
        aktuellesLevel = 2;

       // aktTurmBut = CanonTurmBut;
        aktTurmBut.GetComponent<Outline>().enabled = true;

    }

    public void MapOverlayButton()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void BistDuSicherPanelÖffnen()
    {
        BistDuSicherPanel.SetActive(true);
    }

    public void FensterSchließen()
    {
        BistDuSicherPanel.SetActive(false);
    }





    //Richtige Outline setzen
    public void aktuelleOutlineButtonSetzen()
    {
        for (int i = 0; i < TurmButtons.Length; i++)
        {
            if (TurmButtons[i] != null)
            {
                if (i == TurmButtonIndex)
                {
                    TurmButtons[i].GetComponent<Outline>().enabled = true;
                }
                else
                {
                    TurmButtons[i].GetComponent<Outline>().enabled = false;
                }
            }
              
        }
       
           
    }

    public void WinBildschirm()
    {
        int alteSterne = PlayerPrefs.GetInt("Sterne");
        Debug.Log("Sterne vor WinBildschirm: " +alteSterne);
        SetzeElementetrue();
    
        // Debug.Log(i);
        // Debug.Log("SterneLvl" + i.ToString());
        if (wave >= wave3Sterne)
        {
            VergleichMitSpeicherwert = 3;
            if(VergleichMitSpeicherwert >= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
            {
                 if (VergleichMitSpeicherwert > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                {

                    PlayerPrefs.SetInt("Sterne", 
                        alteSterne + VergleichMitSpeicherwert - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
                        //aktuelle Anzahl unserer Sterne + erspielte Sterne - bereits erspielte Sterne im Level
                }
                
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 3);
                Debug.Log(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
                Debug.Log(SceneManager.GetActiveScene().name);

            }

            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
           
            for (int j = 0; j < Medal.Length; j++)
            {
                Medal[j].GetComponent<Image>().color = Color.yellow;
            }
            PopUpText.GetComponent<Text>().text = "Perfekt!";



        }
        else if (wave > wave2Sterne && wave < wave3Sterne)
        {
            VergleichMitSpeicherwert = 2;
            if (VergleichMitSpeicherwert >= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
            {
                if (VergleichMitSpeicherwert > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                {

                    PlayerPrefs.SetInt("Sterne",
                         alteSterne + VergleichMitSpeicherwert - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
                    //aktuelle Anzahl unserer Sterne + erspielte Sterne - bereits erspielte Sterne im Level
                }
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 2);
            }
           
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            for (int j = 0; j < Medal.Length - 1; j++)
            {
                Medal[j].GetComponent<Image>().color = Color.yellow;
            }
            PopUpText.GetComponent<Text>().text = "Spitze!";
        }
        else if (wave > wave1Sterne && wave <= wave2Sterne)
        {
            VergleichMitSpeicherwert = 1;
            if (VergleichMitSpeicherwert >= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                
            {
                if (VergleichMitSpeicherwert > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                {

                    PlayerPrefs.SetInt("Sterne",
                         alteSterne + VergleichMitSpeicherwert - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
                    //aktuelle Anzahl unserer Sterne + erspielte Sterne - bereits erspielte Sterne im Level
                }
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            }
               


            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            for (int j = 0; j < Medal.Length - 2; j++)
            {
                Medal[j].GetComponent<Image>().color = Color.yellow;
            }
            PopUpText.GetComponent<Text>().text = "Geschafft!";
        }
        else
        {

            VergleichMitSpeicherwert = 0;
            if (VergleichMitSpeicherwert >= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                
            {

                if (VergleichMitSpeicherwert >= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                {

                    PlayerPrefs.SetInt("Sterne",
                        alteSterne + VergleichMitSpeicherwert - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
                    //aktuelle Anzahl unserer Sterne + erspielte Sterne - bereits erspielte Sterne im Level
                }
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 0);
            }

            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            PopUpText.GetComponent<Text>().text = "Nicht geschafft!";
        }
        Debug.Log("Sterne nach WinBildschirm: " + PlayerPrefs.GetInt("Sterne"));

        if (PlayerPrefs.GetInt("Sterne") > alteSterne && SchonGespielt == false)
        {
            PlayerPrefs.SetInt("HöchstesLevel", PlayerPrefs.GetInt("HöchstesLevel") + 1);
        }
    }

    
}
