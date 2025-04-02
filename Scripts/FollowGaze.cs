using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FollowGaze : MonoBehaviour
{
    public Camera gazeCamera; // Oculus용 카메라 또는 헤드셋 컴포넌트
    public TextMeshProUGUI textUI;

    private void Update()
    {
        // 플레이어의 시선 위치 가져오기
        Vector3 gazePosition = gazeCamera.transform.position + gazeCamera.transform.forward * 10f;

        // 텍스트 UI 위치 업데이트
        textUI.transform.position = gazePosition;
    }
}
