using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // 에너미 상태 상수
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // 에너미 상태 변수
    EnemyState m_State;

    // 플레이어 발견 범위
    public float findDistance = 20f;

    // 플레이어 트랜스폼
    Transform player;

    // 공격 가능 범위
    public float attackDistance = 10f;

    // 캐릭터 콘트롤러 컴포넌트
    CharacterController cc;

    // 이동 속도
    public float moveSpeed = 5f;

    // 누적 시간
    float currentTime = 0;

    // 공격 딜레이 시간
    float attackDelay = 2f;

    // 에너미 공격력
    public int attackPower = 30;

    // 초기 위치 저장용 변수
    Vector3 originPos;
    Quaternion originRot;

    // 이동 가능 범위
    public float moveDistance = 20f;

    // 에너미의 체력
    public float hp = 50;

    // 에너미의 최대 체력
    public float maxHp = 50;

    // 에너미 hp Slider 변수
    public Slider hpSlider;

    // 애니메이터 변수
    Animator anim;

    public Text cango;



    // 플레이어 정보 가져오기
    public PlayerInfo playerinfo;
    // 피격 이미지 가져오기
    public Image damageImage;

    public string wallDisable = "wizwl";

    private bool wallDisabled2 = false;


    void Start()
    {
        // 최초의 에너미 상태는 대기 상태(Idle)로 한다.
        m_State = EnemyState.Idle;

        // 플레이어의 트랜스폼 컴포넌트 받아오기
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
        cc.stepOffset = 0.1f;

        // 자신의 초기 위치와 회전값을 저장하기
        originPos = transform.position;
        originRot = transform.rotation;

        // 자식 오브젝트로부터 애니메이터 변수 받아오기
        anim = transform.GetComponentInChildren<Animator>();

        hpSlider.value = 1;
    }

    void Update()
    {
        // 현재 상태를 체크하여 해당 상태별로 정해진 기능을 수행하게 하고 싶다.
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
     

        // 현재 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        // 만일, 플레이어와의 거리가 액션 시작 범위 이내라면 Move 상태로 전환한다.
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");

            // 이동 애니메이션으로 전환하기
            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        // 만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면...
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // 현재 상태를 Return 상태로 전환한다.
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        // 만일, 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동한다.
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 이동 방향 설정
            Vector3 dir = (player.position - transform.position).normalized;

            // 수직 방향을 제거하여 수평 방향으로만 회전하도록 합니다.
            dir.y = 0f;

            // 캐릭터 콘트롤러를 이용하여 이동하기
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // 플레이어를 향하여 부드럽게 방향 전환합니다.
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        // 그렇지 않다면, 현재 상태를 Attack 상태로 전환한다.
        else
        {
            transform.LookAt(player);
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");

            // 누적 시간을 공격 딜레이 시간만큼 미리 진행시켜놓는다.
            currentTime = attackDelay;

            // 공격 대기 애니메이션 플레이
            anim.SetTrigger("MoveToAttackDelay");
        }
    }


    void Attack()
    {
        // 만일, 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정한 시간마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;

                // 공격 애니메이션 플레이
                anim.SetTrigger("StartAttack");
            }
        }
        // 그렇지 않다면, 현재 상태를 Move 상태로 전환한다(재 추격 실시).
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;

            // 이동 애니메이션 플레이
            anim.SetTrigger("AttackToMove");
        }
    }

    // 플레이어의 스크립트의 데미지 처리 함수를 실행하기
    public void AttackAction()
    {
        player.GetComponent<PlayerInfo2>().TakeDamage(attackPower);
    }

    void Return()
    {
        // 만일, 초기 위치에서의 거리가 0.1f 이상이라면 초기 위치 쪽으로 이동한다.
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            Vector3 moveDirection = dir * moveSpeed * Time.deltaTime;

            // 이동 방향으로 이동한다.
            cc.Move(moveDirection);

            // 복귀 지점으로 방향을 전환한다.
            // 복귀 지점으로 방향을 전환한다.
            Vector3 targetDirection = originPos - transform.position;
            targetDirection.y = 0; // 수평 방향으로 회전하기 위해 y 축 값을 0으로 설정합니다.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


            //anim.SetTrigger("MoveToIdle");
        }

        // 그렇지 않다면, 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기 상태로 전환한다.
        else
        {
            // 위치 값과 회전 값을 초기 상태로 변환한다.
            transform.position = originPos;
            transform.rotation = originRot;

            // hp를 다시 회복한다.
            //hp = maxHp;

            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");

            // 대기 애니메이션으로 전환하는 트랜지션을 호출한다.
            anim.SetTrigger("MoveToIdle");
        }
    }

    // 데미지 실행 함수
    public void HitEnemy(int hitPower)
    {
        // 만일, 이미 피격 상태이거나 사망 상태 또는 복귀 상태라면 아무런 처리도 하지 않고 함수를 종료한다.
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        // 플레이어의 공격력만큼 에너미의 체력을 감소시킨다.
        hp -= hitPower;

        // 에너미의 체력이 0보다 크면 피격 상태로 전환한다.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any state -> Damaged");

            // 피격 애니메이션을 플레이한다.
            anim.SetTrigger("Damaged");
            Damaged();
        }
        // 그렇지 않다면, 죽음 상태로 전환한다.
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");

            // 죽음 애니메이션을 플레이한다.
            anim.SetTrigger("Die");
            Die();
        }
    }

    void Damaged()
    {
        // 피격 상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DamageProcess());
    }

    // 데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        // 피격 모션 시간만큼 기다린다.
        yield return new WaitForSeconds(0.1f);

        // 땅에 꺼지는 애니메이션 재생 등의 처리를 추가한다.
        // 예시로 캐릭터를 땅으로 내리기 위해 위치를 조정하는 코드를 추가하였습니다.
        Vector3 downPos = transform.position - new Vector3(0, 1, 0); // 캐릭터를 아래로 내리기 위한 위치
        float duration = 0.5f; // 땅에 내려가는 시간

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(transform.position, downPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 다시 올라오는 애니메이션 재생 등의 처리를 추가한다.
        // 예시로 캐릭터를 다시 올리기 위해 위치를 조정하는 코드를 추가하였습니다.
        Vector3 upPos = transform.position + new Vector3(0, 1, 0); // 캐릭터를 위로 올리기 위한 위치
        duration = 0.5f; // 다시 올라오는 시간

        elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(transform.position, upPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 현재 상태를 이동 상태로 전환한다.
        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }


    // 죽음 상태 함수
    void Die()
    {
        cango.enabled = false;
        if (!wallDisabled2)
        {
            GameObject wallToDisable = GameObject.Find(wallDisable);
            if (wallToDisable != null)
            {
                Collider wallCollider = wallToDisable.GetComponent<Collider>();
                if (wallCollider != null)
                {
                    wallCollider.enabled = false;
                    wallDisabled2 = true;
                }
                else
                {
                    Debug.LogWarning("Wall collider not found on the specified wall game object.");
                }
            }
        }
        

        // 진행중인 피격 코루틴을 중지한다.
        StopAllCoroutines();

        // 죽음 상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // 캐릭터 콘트롤러 컴포넌트를 비활성화한다.
        cc.enabled = false;

        // 2초 동안 기다린 뒤에 자기 자신을 제거한다.
        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }
    public void gethit()
    {
        ShowdamageUI();
    }
    public void ShowdamageUI()
    {
        StartCoroutine(DamageEvent());
    }
    IEnumerator DamageEvent()
    {

        damageImage.enabled = true;
        yield return new WaitForSeconds(0.3f);
        damageImage.enabled = false;
        playerinfo.health = playerinfo.health - 5;
        playerinfo.hpSlider.value = playerinfo.health / 100;
        Debug.Log(playerinfo.health);

        StopCoroutine(DamageEvent());
    }
}