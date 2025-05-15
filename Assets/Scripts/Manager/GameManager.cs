using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private readonly string dataPath = "Data";
    public int BestScore { get; private set; }

    public static GameManager Instance;

    private bool isGameOver = false;
    private float timeSinceLastDifficultyIncrease = 0f;
    private PlayerController player;

    [Header("난이도 설정")]
    public float difficultyIncreaseInterval = 10f; // 난이도 상승 주기
    public float obstacleSpawnRate = 2f; // 초기 스폰간격 (초)
    public float obstacleSpeed = 5f; // 초기 장애물 속도
    public float playerGodMode = 1f; // 초기 무적 시간 (초)

    [Header("난이도 변화량")]
    public float spawnRateDecrease = 0.1f;
    public float speedIncrease = 0.5f;
    public float playerGodModeDecrease = 0.05f;


    public int coinCount = 0;
    public int score = 0;
    private UIManager uiManager;
    //UI 매니저 생성후 연결해주세요

    private string fileName = "Data.json";

    public class ScoreData
    {
        public int bestScore;
    }

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
        player = FindObjectOfType<PlayerController>();
        LoadBestScore();
        uiManager.UpdateScore(0, BestScore, 0);

        if (player != null)
            player.ActivateGodMode(playerGodMode); // ★ 무적 모드 적용
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

        if (player != null)
        {
            player.Speed += speedIncrease; // ★ 플레이어 속도도 증가
        }
    }

    public void AddScore(int value)
    {
        if (!isGameOver)
        {
            score += value;
            coinCount++;
            uiManager.UpdateScore(score, BestScore, coinCount);
        }
    }
    public void AddDistanceScore(int value)
    {
        if (!isGameOver)
        {
            score += value;
            uiManager.UpdateScore(score, BestScore, coinCount);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        //게임정지
        SaveBestScore();

        uiManager.UpdateScore(score, BestScore, coinCount);
        Time.timeScale = 0f;
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.enabled = false;
        uiManager.SetGameOver();
        SFXManager.Instance.HitSFX();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
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
public void SaveBestScore()
    {
        if (score > BestScore)
        {
            BestScore = score;
            ScoreData data = new ScoreData { bestScore = BestScore };

            string json = JsonUtility.ToJson(data, true);
            string fullPath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllText(fullPath, json);
        }
    }

    private void LoadBestScore()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            BestScore = data.bestScore;
        }
        else
        {
            Debug.LogWarning("저장된 점수 없음. 기본값 0 사용.");
            BestScore = 0;
        }
    }

}
