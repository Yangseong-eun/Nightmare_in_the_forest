using UnityEngine;
using OculusSampleFramework;

public class GameStarter : MonoBehaviour
{
    public Transform rightAnchor; // 오른쪽 컨트롤러의 rightAnchor
    public Transform gun; // 총 모델

    private void Start()
    {
        // 총 모델을 오른쪽 컨트롤러 rightAnchor 아래로 이동시킴
        gun.SetParent(rightAnchor);
        gun.localPosition = Vector3.zero;
        gun.localRotation = Quaternion.identity;
    }
}
