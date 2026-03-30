using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class environmentmanager : MonoBehaviour
{
  [Header("Environments")]
  public GameObject betterEnv;
  public GameObject goodEnv;
  public GameObject perfectEnv;
  public Transform bettermid;
  public Transform goodmid;

  public Material betterSkybox;
  public Material goodSkybox;
  public Material perfectSkybox;

  [Header("UI")]
  public TextMeshProUGUI scoreText;
  public GameObject perfectPanel;
  public GameObject scorePopup;

  [Header("Player")]
  public Transform playerTf;

  int score = 0;
  float moveTime = 0, idleTime = 0;
  bool envchanged = false;

  GameObject currentEnv;

  void Start()
  {
    betterEnv.SetActive(false);
    goodEnv.SetActive(false);
    perfectEnv.SetActive(false);



  }

  void Update()
  {
    HandleMovement();
    UpdateEnvironment();
  }

  void HandleMovement()

  {
    bool move = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

    if (move)
    {
      moveTime += 2 * Time.deltaTime;
      idleTime = 0f;
    }
    else
    {
      idleTime += Time.deltaTime;
      moveTime = 0f;
    }

    if (moveTime > 5f)
    {
      score += 10;
      moveTime = 0f;
      scoreText.text = "Score: " + score;
      StartCoroutine(ShowScorePopup("+10"));
    }

    if (idleTime > 5f)
    {
      score -= 10;
      idleTime = 0f;
      scoreText.text = "Score: " + score;
      StartCoroutine(ShowScorePopup("-10"));
    }
  }
  IEnumerator ShowScorePopup(string message)
  {
    scorePopup.SetActive(true);
    scorePopup.GetComponent<TextMeshProUGUI>().text = message;
    yield return new WaitForSeconds(1f);
    scorePopup.SetActive(false);
  }
  void UpdateEnvironment()
  {
       

    if (score < 100)
    {
      if (currentEnv != betterEnv)
        ApplyEnvironment(betterEnv, betterSkybox);
      if (envchanged)
      {
        playerTf.position = bettermid.position;
        envchanged = false;
      }


    }
    else if (score < 200)
    {
      if (currentEnv != goodEnv)
        ApplyEnvironment(goodEnv, goodSkybox);
      StartCoroutine(ShowPerfectPanel());
      if (envchanged)
      {
        playerTf.position = goodmid.position;
        envchanged = false;
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
    betterEnv.SetActive(false);
    goodEnv.SetActive(false);
    perfectEnv.SetActive(false);
    Vector3 envPos = env.transform.position;
    env.transform.position = new Vector3(playerTf.position.x, envPos.y, envPos.z);

    env.SetActive(true);
    RenderSettings.skybox = skybox;

    currentEnv = env;
    envchanged = true;
  }

  IEnumerator ShowPerfectPanel()
  {
    perfectPanel.SetActive(true);
    yield return new WaitForSeconds(1f);
    perfectPanel.SetActive(false);
  }
}
