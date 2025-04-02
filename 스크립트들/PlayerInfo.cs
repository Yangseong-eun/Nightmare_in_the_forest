using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public float health;
    private bool isGameOver = false;
    public Slider hpSlider;
    public Image DmgImg;
    public TextMeshProUGUI die;

    Transform player;
    Transform gretel;
    public Image endimage;
    public Text endText;
    private int endqst;

    private void Start()
    {
        hpSlider.value = 1;
        health = 100;
        endimage.enabled = false;
        endText.enabled = false;
        endqst = 0;
    }

    private void Update()
    {
       

        // 플레이어의 다른 행동 코드 작성

        if (health <= 0)
        {
            DmgImg.enabled = true;
            die.enabled = true;
           
            GameOver();

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
            {

                RestartGame();
            }
        }

        gretel = GameObject.FindGameObjectWithTag("Gretel").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Oculus 컨트롤러의 트리거 버튼 입력 상태 감지
        if (Vector3.Distance(player.position, gretel.position) < 3f)
        {
            endimage.enabled = true;
            endText.enabled = true;
            endqst++;
            
        }
        if (endqst >= 1)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                Debug.Log("종료");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
            }
        }



        // 왼쪽 컨트롤러의 X 키를 눌렀을 때 게임 재시작
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void GameOver()
    {
        isGameOver = true;
       

        // 게임을 멈추는 코드 작성
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // 게임을 재시작하는 코드 작성
        Time.timeScale = 1f; // 게임 시간 비율을 원래대로 돌림
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬을 다시 로드
    }
}
