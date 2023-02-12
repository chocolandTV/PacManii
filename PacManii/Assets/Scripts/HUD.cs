using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI liveValueText;
    [SerializeField] private TextMeshProUGUI cherryValueText;
    [SerializeField] private TextMeshProUGUI secretValueText;
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI levelValueText;
    [SerializeField] private TextMeshProUGUI pelletValueText;
    [SerializeField] private TextMeshProUGUI PaciiStatusValueText;
    [SerializeField] private GameObject gameManagerObject;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager= gameManagerObject.GetComponent<GameManager>();
    }
    public enum TextType
    {
        live,
        cherry,
        secret,
        score,
        level, 
        pellet,
        paciiStatus
    }
    // Update is called once per frame
    private string PacManStatusText(int value)
    {
        if(value ==1)
            return "Normal";
        if(value ==2)
            return "Excited";
        if(value  == 3)
            return "Frightend";
        return "Normal";
    }
    public void OnValueChanged(int value, TextType type)
    {
        switch (type)
        {
            case TextType.live:
                liveValueText.text = gameManager.lives.ToString();
                break;
            case TextType.cherry:
                cherryValueText.text = gameManager.cherries.ToString();
                break;
            case TextType.secret:
                secretValueText.text = gameManager.secrets.ToString();
                break;
            case TextType.score:
                scoreValueText.text = gameManager.score.ToString();
                break;
            case TextType.level:
                levelValueText.text = gameManager.level.ToString();
                break;
            case TextType.pellet:
                pelletValueText.text = gameManager.pelletsRemaining.ToString();
                break;
            case TextType.paciiStatus:
                PaciiStatusValueText.text = PacManStatusText( gameManager.paciiStatus);
                break;
            default: 

            Debug.Log("NO INPUT");
            break;
        }
    }
}
