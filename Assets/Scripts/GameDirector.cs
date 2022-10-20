using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� ���� ���¸� ǥ���ϰ�, ���� ������ UI�� �����ϴ� ���� �Ŵ���
// ������ �� �ϳ��� ���� �Ŵ����� ������ �� �ִ�.
public class GameDirector : MonoBehaviour
{
    public static GameDirector instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false; // ���� ���� ����
    public Text scoreText; // ������ ����� UI �ؽ�Ʈ
    public Text healthText; // ü���� ����� UI �ؽ�Ʈ
    public GameObject healthBar; // ü�¹ٸ� ǥ���� UI ���� ������Ʈ
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ

    public int score = 0; // ���� ����
    public float health = 100.0f; // �÷��̾� ü��

    // ���� ���۰� ���ÿ� �̱����� ����
    void Awake()
    {
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���

            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        this.scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        this.healthText = GameObject.Find("HealthText").GetComponent<Text>();
        this.healthBar = GameObject.Find("Health");
        this.gameoverUI = GameObject.Find("gameoverUI");
        
        // ���� �� false�� �����Ͽ� �Ⱥ��̰� ����
        gameoverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseHealth();
    }

    // ������ ������Ű�� �޼���
    public void AddScore(int newScore)
    {
        // ���� ������ �ƴ϶��
        if (!isGameover)
        {
            // ������ ����
            score += newScore;
            scoreText.text = "Score : " + score.ToString();
        }
    }

    //  ü���� ������Ű�� �޼���
    public void AddHP(float newHP)
    {
        // ���� ������ �ƴ϶��
        if (!isGameover && health <= 100.0f)
        {
            if (health > 100.0f)
                health = 100.0f;
            else
            // ü���� ����
            health += newHP;

            // ���� ü���� ü�¹ٿ� ǥ��
            this.healthBar.GetComponent<Image>().fillAmount += newHP;

            // �ؽ�Ʈ�ε� ǥ�� �ϱ� ���� UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
    }

    // �λ� �� ü���� ���ҽ�Ű�� �޼���
    public void DamagedHP(float newHP)
    {
        // ���� ������ �ƴ϶��
        if (!isGameover && health > 0.0f)
        {
            // ü���� ����
            health -= newHP;

            // ���� ü���� ü�¹ٿ� ǥ��
            this.healthBar.GetComponent<Image>().fillAmount -= newHP;

            // �ؽ�Ʈ�ε� ǥ�� �ϱ� ���� UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
        else
        {
            // ü���� 0�� �Ǹ� �÷��̾��� ���� �Լ��� ȣ��
            if(health <= 0)
            {
                OnPlayerDead();
            }
        }
    }

    // ���������� ü���� ���ҽ�Ű�� �޼���
    public void DecreaseHealth()
    {
        if (!isGameover && health > 0.0f)
        {
            // ü�� ����
            health -= 0.02f;

            // ���� ü���� ü�¹ٿ� ǥ��
            this.healthBar.GetComponent<Image>().fillAmount -= 0.0002f;
            ;

            // �ؽ�Ʈ�ε� ǥ�� �ϱ� ���� UI text 
            this.healthText.text = string.Format("HP {0}/100", health);
        }
        else
        {
            // ü���� 0�� �Ǹ� �÷��̾��� ���� �Լ��� ȣ��
            OnPlayerDead();
        }
    }

    // ���� ������ ������ ����� �ϴ� �޼���
    public void Restart()
    {
            // ���� ���� ���¿��� gameoverUI ��ư�� Ŭ���ϸ� ���� �� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    // �÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼���
    public void OnPlayerDead()
    {
        // ���� ���¸� ���� ���� ���·� ����
        isGameover = true;

        // ���� ���� UI�� Ȱ��ȭ
        gameoverUI.SetActive(true);
    }
}
