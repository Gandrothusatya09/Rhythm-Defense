using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EnvironmentSet
{
  public GameObject environment;
  public Material skybox;
}

public class environmentmanager : MonoBehaviour
{
  public EnvironmentSet Better;
  public EnvironmentSet Good;
  public EnvironmentSet Perfect;
  public TextMeshProUGUI scoreText;
  public GameObject scorePopup;

  [SerializeField] private Transform playerTf;

  int Score = 0;
  float movetime = 0f;
  float idletime = 0f;

  EnvironmentSet currentEnv;

  void Start()
  {
    Better.environment.SetActive(false);
    Good.environment.SetActive(false);
    Perfect.environment.SetActive(false);

    ApplyEnvironment(Better);
  }

  void Update()
  {
    HandleMovement();
    UpdateEnvironment();
  }

  void HandleMovement()
  {
    bool movementkey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

    if (movementkey)
    {
      movetime += 2 * Time.deltaTime;
      idletime = 0f;
    }
    else
    {
      idletime += Time.deltaTime;
      movetime = 0f;
    }

    if (movetime > 5f)
    {
      Score += 10;
      movetime = 0f;
      scoreText.text = "Score: " + Score;
      StartCoroutine(ShowScorePopup("+10"));
    }

    if (idletime > 5f)
    {
      Score -= 10;
      idletime = 0f;
      scoreText.text = "Score: " + Score;
      StartCoroutine(ShowScorePopup("-10"));
    }
  }

  void UpdateEnvironment()
  {
    EnvironmentSet targetEnv;

    if (Score < 100)
      targetEnv = Better;
    else if (100 <= Score && Score < 200)
      targetEnv = Good;
    else
      targetEnv = Perfect;
    if (currentEnv != targetEnv)
    {
      ApplyEnvironment(targetEnv);
    }
  }

  void ApplyEnvironment(EnvironmentSet env)
  {
    Better.environment.SetActive(false);
    Good.environment.SetActive(false);
    Perfect.environment.SetActive(false);
    env.environment.SetActive(true);
    Vector3 pos = env.environment.transform.position;
    env.environment.transform.position = new Vector3(playerTf.position.x, pos.y, pos.z);
    RenderSettings.skybox = env.skybox;
    DynamicGI.UpdateEnvironment();
    currentEnv = env;
  }

  IEnumerator ShowScorePopup(string message)
  {
    scorePopup.SetActive(true);
    scorePopup.GetComponent<TextMeshProUGUI>().text = message;
    yield return new WaitForSeconds(1f);
    scorePopup.SetActive(false);
  }
}
