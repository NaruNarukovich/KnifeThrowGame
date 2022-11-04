using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    private int knifeCount;

    [Header("Knife Spawning")]
    public Vector3 knifeSpawnPosition;
    [SerializeField]
    private GameObject knifeObject;
    public GameUI GameUI;

    public int score;
    public TMP_Text scoreUI;
    private void Awake()
    {
        if(Instance && GameUI != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            GameUI = GetComponent<GameUI>();
        }
    }

    private void Start()
    {
        GameUI.SetInitialDisplayKnifeCount(knifeCount);
        SpawnKnife();
    }

    private void Update()
    {
        scoreUI.text = score.ToString();
    }

    public void OnSuccessfulKnifeHit()
    {
        if(knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.Euler(0, 90, 180)); ;
        
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            int index = Random.Range(0, 4);
            SceneManager.LoadScene(index);
            Debug.Log("Scene Loaded");
        }
        else
        {
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
