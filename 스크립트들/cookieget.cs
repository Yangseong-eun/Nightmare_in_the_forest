using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class cookieget : MonoBehaviour
{

    public Text uiText; // UI Text 컴포넌트
    public int itemCountToDisableWall = 10; // 일정 값
    private int itemCount = 0; // 아이템 갯수를 세는 변수
    public string wallNameToDisable = "Wall";
    public PlayerInfo player;
  


   


    // Start is called before the first frame update
    void Start()
    {
    }
   

    private void Update()
    {
        
        //사용자가 IndexTrigger를 누르면 (마우스 기준 휠 버튼)
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            
            // 마우스 위치에서 레이캐스트를 발사합니다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo;

            // 레이캐스트를 통해 "cookie" 태그를 가진 물체를 클릭했는지 확인합니다.
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.CompareTag("cookie"))
            {
                // 물체를 비활성화하고 카운트 값을 증가시킵니다.
                GameObject cookie = hitInfo.collider.gameObject;
                cookie.SetActive(false);
                itemCount++;

                // 일정 값에 도달하면 벽을 비활성화합니다.
                if (itemCount >= itemCountToDisableWall)
                {
                    GameObject wallToDisable = GameObject.Find(wallNameToDisable);
                    if (wallToDisable != null)
                    {
                        Collider wallCollider = wallToDisable.GetComponent<Collider>();
                        if (wallCollider != null)
                        {
                            wallCollider.enabled = false;
                            Debug.Log("쿠키를 10개이상 수집하셨습니다!! 이제 마녀의집으로 이동할 수 있습니다.");
                            uiText.enabled = true; // UI Text 활성화                         

                        }
                        else
                        {
                            Debug.LogWarning("Wall collider not found on the specified wall game object.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Wall game object with the specified name not found.");
                    }
                }
            }


            
        }
    }
}
