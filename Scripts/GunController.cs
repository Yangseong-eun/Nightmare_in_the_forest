using UnityEngine;
using System.Collections;
using OculusSampleFramework;
// 사용자가 발사 버튼을 누르면 총을 쏘고 싶다.
// 필요 속성: 총알 파편, 총알 파편 효과, 총알 발사 사운드
public class GunController : MonoBehaviour
{
    public Transform bulletImpact; // 총알 파편 효과
    ParticleSystem bulletEffect; // 총알 파편 파티클 시스템
    AudioSource bulletAudio; // 총알 발사 사운드
    public int weaponPower = 5;
    public GameObject crosshair;  // Crosshair 오브젝트의 레퍼런스
    public OVRCameraRig cameraRig;  // OVRCameraRig 오브젝트의 레퍼

    public Animator mush;
    public Animator mush2;
    public Animator cactus1;
    public Animator cactus2;
    public Animator cactus3;

    public mushinfo mushinfo;
    public mushinfo mushinfo2;
    public mushinfo cactusinfo;
    public mushinfo cactusinfo2;
    public mushinfo cactusinfo3;

    public EnemyFSM enemy;


    void Start()
    {
        // 총알 효과 파티클 시스템 컴포넌트 가져오기
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
        // 총알 효과 오디오 소스 컴포넌트 가져오기
    }

    void Update()
    {
        OVRInput.Controller rightController = OVRInput.Controller.RTouch;
        Vector3 rightControllerPosition = cameraRig.rightHandAnchor.position;
        Quaternion rightControllerRotation = cameraRig.rightHandAnchor.rotation;

        // Crosshair 오브젝트의 위치 업데이트
        crosshair.transform.position = rightControllerPosition + rightControllerRotation * new Vector3(0, 0, 0.5f);
        crosshair.transform.rotation = rightControllerRotation;

        if (ARAVRInput.GetDown(ARAVRInput.Button.One))
        {
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);
            
            bulletAudio.Stop();
            bulletAudio.Play();

            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            // Ray의 충돌 정보를 저장하기 위한 변수 지정
            RaycastHit hitInfo;
            // 플레이어 레이어 얻어오기
            int playerlayer = 1 << LayerMask.NameToLayer("player");
            int layerMask = playerlayer;
            // 타워 레이어 얻어오기

            if (Physics.Raycast(ray, out hitInfo, 200, ~layerMask))
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Debug.Log("활성화");
                    var hitObject = hitInfo.transform;
                    // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                    bulletImpact.forward = hitInfo.normal;
                    // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                    bulletImpact.position = hitInfo.point;
                    if (hitInfo.transform.name=="Angrymush")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        Debug.Log("여기서");
                        mush.SetTrigger("hit");
                        mushinfo.health = mushinfo.health - 2;
                        mushinfo.hpSlider.value = mushinfo.health / 10;
                        Debug.Log(mushinfo.hpSlider.value);
                        if (mushinfo.health <= 0)
                        {
                            animstop(mush);
                        }
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }
                    if (hitInfo.transform.name == "Angrymush2")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        Debug.Log("여기서");
                        mush2.SetTrigger("hit");
                        mushinfo2.health = mushinfo2.health - 2;
                        mushinfo2.hpSlider.value = mushinfo2.health / 10;
                        Debug.Log(mushinfo2.hpSlider.value);
                        if (mushinfo2.health <= 0)
                        {
                            animstop(mush2);
                        }
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }
                    if (hitInfo.transform.name == "Cactus")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        Debug.Log("여기서");
                        cactus1.SetTrigger("hit");
                        cactusinfo.health = cactusinfo.health - 2;
                        cactusinfo.hpSlider.value = cactusinfo.health / 10;
                        Debug.Log(cactusinfo.hpSlider.value);
                        if (cactusinfo.health <= 0)
                        {
                            animstop(cactus1);
                        }
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }
                    if (hitInfo.transform.name == "Enemy")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        //Debug.Log("여기서");
                        enemy.HitEnemy(weaponPower);
                        enemy.hpSlider.value = enemy.hp / enemy.maxHp;
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }
                    if (hitInfo.transform.name == "Cactus2")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        Debug.Log("여기서");
                        cactus2.SetTrigger("hit");
                        cactusinfo2.health = cactusinfo2.health - 2;
                        cactusinfo2.hpSlider.value = cactusinfo2.health / 10;
                        Debug.Log(cactusinfo2.hpSlider.value);
                        if (cactusinfo2.health <= 0)
                        {
                            animstop(cactus2);
                        }
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }
                    if (hitInfo.transform.name == "Cactus3")
                    {
                        bulletEffect.Stop();
                        bulletEffect.Play();
                        Debug.Log("여기서");
                        cactus3.SetTrigger("hit");
                        cactusinfo3.health = cactusinfo3.health - 2;
                        cactusinfo3.hpSlider.value = cactusinfo3.health / 10;
                        Debug.Log(cactusinfo3.hpSlider.value);
                        if (cactusinfo3.health <= 0)
                        {
                            animstop(cactus3);
                        }
                        // 부딪힌 지점의 방향으로 총알의 이펙트 방향을 설정
                        bulletImpact.forward = hitInfo.normal;
                        // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                        bulletImpact.position = hitInfo.point;
                    }

                }

            }
            
        }
    }
    public void animstop(Animator animate)
    {
        StartCoroutine(stopanim(animate));
    }
    IEnumerator stopanim(Animator animate)
    {

        animate.SetTrigger("die");
        yield return new WaitForSeconds(0.75f);
        animate.enabled = false;
        StopCoroutine(stopanim(animate));
    }

}