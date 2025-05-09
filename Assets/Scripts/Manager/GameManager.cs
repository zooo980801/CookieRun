using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameOver = false;
    private float timeSinceLastDifficultyIncrease = 0f;

    [Header("난이도 설정")]
    public float difficultyIncreaseInterval = 10f; // 난이도 상승 주기
    public float obstacleSpawnRate = 2f; // 초기 스폰간격 (초)
    public float obstacleSpeed = 5f; // 초기 장애물 속도
    public float playerGodMode = 1f; // 초기 무적 시간 (초)

    [Header("난이도 변화량")]
    public float spawnRateDecrease = 0.1f;
    public float speedIncrease = 0.5f;
    public float playerGodModeDecrease = 0.05f;

    private int coinCount = 0;
    private int score = 0;
    private UIManager uiManager;
    //UI 매니저 생성후 연결해주세요

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0,0);
    }

    private void Update()
    {
        timeSinceLastDifficultyIncrease += Time.deltaTime;

        if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            timeSinceLastDifficultyIncrease = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        //스폰 간격 줄임. 근데 최소값보장
        obstacleSpawnRate = Mathf.Max(0.5f, obstacleSpawnRate - spawnRateDecrease);
        // 속도는 계속 증가 가능
        obstacleSpeed += speedIncrease;
        // 무적시간은 너무낮아지지않게 최소값보장.
        playerGodMode = Mathf.Max(0.3f, playerGodMode - playerGodModeDecrease);
    }

    public void AddScore(int value)
    {
        if (!isGameOver)
        {
            score += value;
            coinCount++;
            uiManager.UpdateScore(score,coinCount);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        //게임정지
        Time.timeScale = 0f;
        uiManager.SetGameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //GameOverPanel UIManager연결해주세요.
    //GameOverUI속 Button에 GameManager.RestartGame() 함수 선택해주세요.
    //re:Ui에서 버튼클릭이벤트 연결해두고 게임매니저메소드 호출하는식으로 변경했습니다.

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
