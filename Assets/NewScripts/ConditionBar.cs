using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConditionBar : MonoBehaviour
{
    public int maxConditions = 12;
    private int currentConditions = 0;
    float targetFill;

    public TMP_Text conditionText;
    public Image fillImage;

    void Start()
    {
        UpdateUI();
    }

    public void AddCondition()
    {
        if (currentConditions < maxConditions)
        {
            currentConditions++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        conditionText.text = currentConditions + " / " + maxConditions;
        targetFill = (float)currentConditions / maxConditions;
    }
    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * 5f);
        if (currentConditions <= 4)
        {
            fillImage.color = Color.red;
        }
        else if (currentConditions <= 8)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.green;
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

        UpdateUI();
    }
}
