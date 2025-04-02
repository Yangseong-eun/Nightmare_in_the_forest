using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mushattack : MonoBehaviour
{

    public Transform player; // 플레이어의 Transform 컴포넌트
    public Transform mush; // NPC의 Transform 컴포넌트
    public float activationDistance = 5f; // 활성화 거리
    public Animator anim;
    public Text nameText;
    public Image damageimage;
    public PlayerInfo playerinfo;
    public mushinfo monster;
  

    // Start is called before the first frame update
    void Start()
    {
        
        nameText.color = Color.red;

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(monster.health>0)
        {
            transform.LookAt(player);
        }
        
        float distance = Vector3.Distance(player.position, mush.position);

        if (distance <= activationDistance)
        {
            anim.SetTrigger("attack");           
            
        }
        
        

    }
    public void gethit()
    {

        ShowDamageUI();
    }
    public void ShowDamageUI()
    {
        StartCoroutine(DamageEvent());
    }

    IEnumerator DamageEvent()
    {
        
        float distance = Vector3.Distance(player.position, mush.position);

        if (distance <= activationDistance-2f)
        {
            damageimage.enabled = true;
            yield return new WaitForSeconds(0.3f);
            damageimage.enabled = false;
            playerinfo.health = playerinfo.health - 2;
            playerinfo.hpSlider.value = playerinfo.health / 100;
            Debug.Log(playerinfo.hpSlider.value);

            StopCoroutine(DamageEvent());
        }
            
    }
    
}
