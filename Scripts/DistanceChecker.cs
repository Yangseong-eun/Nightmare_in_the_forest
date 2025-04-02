using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceChecker : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform 컴포넌트
    public Transform npc; // NPC의 Transform 컴포넌트
    public float activationDistance = 5f; // 활성화 거리

    public TextMeshProUGUI uiText; // UI 텍스트

    private bool isTextActive = false; // 텍스트 활성화 여부

    void Update()
    {
        // 플레이어와 NPC 사이의 거리 계산
        float distance = Vector3.Distance(player.position, npc.position);

        // 일정 거리 이하로 가까워지면 텍스트 활성화
        if (distance <= activationDistance && !isTextActive)
        {
            uiText.gameObject.SetActive(true);
            isTextActive = true;
        }
        // 일정 거리보다 멀어지면 텍스트 비활성화
        else if (distance > activationDistance && isTextActive)
        {
            uiText.gameObject.SetActive(false);
            isTextActive = false;
        }
    }
}