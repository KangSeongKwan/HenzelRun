using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public AudioClip hurtClip; // 독극물 복용 시 재생할 오디오 클립
    public AudioClip jumpClip; // 다시 복원할 오디오 클립(점프)
    public AudioClip runClip; // 처음 시작할 때 재생할 오디오 클립
    public AudioClip itemClip; // 아이템(사탕,과자)을 먹었을 때 재생할 오디오 클립
    public AudioClip healClip; // 회복 아이템 복용 시 재생할 오디오 클립
    private AudioSource playerAudio;
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 430.0f;
    private int jumpCount = 0;
    private bool isGrounded = false; // 땅에 닿았는지 나타내는 상태변수
    private bool isDead = false; // 사망 상태

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.playerAudio = GetComponent<AudioSource>();
        PlaySound("RUN");
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            return;
        }
        // 점프한다.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpCount++;
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            PlaySound("JUMP");
        }
        animator.SetBool("Grounded", isGrounded);

        // 체력게이지가 0과 동일해 지거나 낮아질 경우 데드트리거 발동
        if (0 >= GameDirector.instance.health)
        {
            this.animator.SetTrigger("DeadTrigger");
            Die();
        }
    }

    private void Die()
    {
        // 사망 처리
        // 애니메이터의 Die 트리거 파라미터를 셋
        this.animator.SetTrigger("DeadTrigger");
        PlaySound("DEAD");

        // 속도를 제로로 변경
        rigid2D.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        // 게임 매니저의 게임오버 처리 실행
        GameDirector.instance.OnPlayerDead();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 파란색 포션을 먹었을 경우 체력을 회복하는 처리
        if (other.gameObject.tag.Equals("healPotion"))
        {
            GameDirector.instance.AddHP(0.05f);
            PlaySound("HEAL");
            other.gameObject.SetActive(false);
        }
        // 장애물에 부딪힐 경우 Hurt Animation 발동
        if (other.gameObject.tag.Equals("poisonPotion"))
        {
            this.animator.SetTrigger("HurtTrigger");
            PlaySound("HURT");
            GameDirector.instance.DamagedHP(0.05f);
            other.gameObject.SetActive(false);
        }
        // 디저트를 먹었을 경우 점수를 추가하는 처리
        if (other.gameObject.tag.Equals("Item"))
        {
            GameDirector.instance.AddScore(100);
            PlaySound("ITEM");
            other.gameObject.SetActive(false);
        }
        // 사망 오브젝트 충돌 시 사망
        if (other.gameObject.tag.Equals("DeathObject") || other.gameObject.tag.Equals("DeathObjectSide"))
        {
            this.animator.SetTrigger("DeadTrigger");
            Die();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았을 때 점프 초기화를 위한 메소드
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "JUMP":
                playerAudio.clip = jumpClip;
                break;
            case "HURT":
                playerAudio.clip = hurtClip;
                break;
            case "DEAD":
                playerAudio.clip = deathClip;
                break;
            case "RUN":
                playerAudio.clip = runClip;
                break;
            case "ITEM":
                playerAudio.clip = itemClip;
                break;
            case "HEAL":
                playerAudio.clip = healClip;
                break;
        }
        playerAudio.Play();
    }
}
