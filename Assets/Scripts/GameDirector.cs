using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameDirector : MonoBehaviour
{
    public static GameDirector instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text healthText; // 체력을 출력할 UI 텍스트
    public GameObject healthBar; // 체력바를 표시할 UI 게임 오브젝트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    public int score = 0; // 게임 점수
    public float health = 100.0f; // 플레이어 체력

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        this.scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        this.healthText = GameObject.Find("HealthText").GetComponent<Text>();
        this.healthBar = GameObject.Find("Health");
        this.gameoverUI = GameObject.Find("gameoverUI");
        
        // 시작 후 false로 설정하여 안보이게 설정
        gameoverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseHealth();
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        // 게임 오버가 아니라면
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score.ToString();
        }
    }

    //  체력을 증가시키는 메서드
    public void AddHP(float newHP)
    {
        // 게임 오버가 아니라면
        if (!isGameover && health <= 100.0f)
        {
            if (health > 100.0f)
                health = 100.0f;
            else
            // 체력을 증가
            health += newHP;

            // 현재 체력을 체력바에 표시
            this.healthBar.GetComponent<Image>().fillAmount += newHP;

            // 텍스트로도 표시 하기 위한 UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
    }

    // 부상 시 체력을 감소시키는 메서드
    public void DamagedHP(float newHP)
    {
        // 게임 오버가 아니라면
        if (!isGameover && health > 0.0f)
        {
            // 체력을 감소
            health -= newHP;

            // 현재 체력을 체력바에 표시
            this.healthBar.GetComponent<Image>().fillAmount -= newHP;

            // 텍스트로도 표시 하기 위한 UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
        else
        {
            // 체력이 0이 되면 플레이어의 죽음 함수를 호출
            if(health <= 0)
            {
                OnPlayerDead();
            }
        }
    }

    // 점진적으로 체력을 감소시키는 메서드
    public void DecreaseHealth()
    {
        if (!isGameover && health > 0.0f)
        {
            // 체력 감소
            health -= 0.02f;

            // 현재 체력을 체력바에 표시
            this.healthBar.GetComponent<Image>().fillAmount -= 0.0002f;
            ;

            // 텍스트로도 표시 하기 위한 UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
        else
        {
            // 체력이 0이 되면 플레이어의 죽음 함수를 호출
            OnPlayerDead();
        }
    }

    // 게임 오버시 게임을 재시작 하는 메서드
    public void Restart()
    {
            // 게임 오버 상태에서 gameoverUI 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        // 현재 상태를 게임 오버 상태로 변경
        isGameover = true;

        // 게임 오버 UI를 활성화
        gameoverUI.SetActive(true);
    }
}
