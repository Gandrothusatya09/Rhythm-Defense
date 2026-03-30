using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Condition System")]
    public int maxConditions = 12;
    private int currentConditions = 0;
    float targetFill;

    public TMP_Text conditionText;
    public Image fillImage;

    [Header("Environment")]
    public GameObject betterEnv;
    public GameObject goodEnv;
    public GameObject perfectEnv;

    public Material betterSkybox;
    public Material goodSkybox;
    public Material perfectSkybox;

    public Transform playerTf;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject perfectPanel;
    public GameObject scorePopup;

    int score = 0;
    GameObject currentEnv;

    void Start()
    {
        betterEnv.SetActive(false);
        goodEnv.SetActive(false);
        perfectEnv.SetActive(false);

        UpdateUI();
        UpdateEnvironment();
    }

    void Update()
    {
        Debug.Log("Update running");

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * 5f);

        UpdateColor();
    }

    public void AddCondition()
    {
        if (currentConditions < maxConditions)
        {
            currentConditions++;
            score += 10;

            UpdateUI();
            UpdateColor();
            UpdateEnvironment();

            StartCoroutine(ShowScorePopup("+10"));
        }
    }

    
    public void HitBomb()
    {
        if (currentConditions > 8)
        {
            currentConditions = 8;
        }
        else if (currentConditions > 4)
        {
            currentConditions = 4;
        }
        else
        {
            currentConditions = 0;
        }

        score = 0; 

        UpdateUI();
        UpdateColor();        
        UpdateEnvironment(); 
        UpdateEnvironment();
    }

    void UpdateUI()
    {
        conditionText.text = currentConditions + " / " + maxConditions;
        targetFill = (float)currentConditions / maxConditions;
        scoreText.text = "Score: " + score;
    }

    void UpdateColor()
    {

        if (currentConditions <= 4)
        {
            fillImage.color = Color.red;
            Debug.Log("RED");
        }
        else if (currentConditions <= 8)
        {
            fillImage.color = Color.yellow;
            Debug.Log("YELLOW");
        }
        else
        {
            fillImage.color = Color.green;
            Debug.Log("GREEN");
        }
    }

    void UpdateEnvironment()
    {
        
        if (currentConditions <= 4)
        {
            if (currentEnv != betterEnv)
                ApplyEnvironment(betterEnv, betterSkybox);
        }
      
        else if (currentConditions <= 8)
        {
            if (currentEnv != goodEnv)
            {
                ApplyEnvironment(goodEnv, goodSkybox);
                StartCoroutine(ShowPerfectPanel());
            }
        }
        
        else
        {
            if (currentEnv != perfectEnv)
            {
                ApplyEnvironment(perfectEnv, perfectSkybox);
                StartCoroutine(ShowPerfectPanel());
            }
        }
    }

    void ApplyEnvironment(GameObject env, Material skybox)
    {
        if (env == null) return;

        // Disable all
        betterEnv.SetActive(false);
        goodEnv.SetActive(false);
        perfectEnv.SetActive(false);

       
        Vector3 envPos = env.transform.position;

        env.transform.position = new Vector3(
            playerTf.position.x,   
            envPos.y,              
            playerTf.position.z    
        );

        env.SetActive(true);
        RenderSettings.skybox = skybox;

        currentEnv = env;
    }

    IEnumerator ShowPerfectPanel()
    {
        perfectPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        perfectPanel.SetActive(false);
    }

    IEnumerator ShowScorePopup(string message)
    {
        scorePopup.SetActive(true);
        scorePopup.GetComponent<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(1f);
        scorePopup.SetActive(false);
    }
}