using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LebenUI : MonoBehaviour
{

    public int maxLeben = 100;
    public float aktuellesLeben = 100;
    private float originalScale;
    public float tempScale;
    public GameObject PanelLeben;
    public GameManager gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        originalScale = PanelLeben.GetComponent<Image>().fillAmount;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        tempScale = ((float)gM.Leben / (float)gM.LebenMax) * originalScale;
        PanelLeben.GetComponent<Image>().fillAmount = tempScale;
    }
}
