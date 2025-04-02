using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestinfo : MonoBehaviour
{
    public Transform player;

    public Transform chestpink;
    public Transform chestpurple;
    public Transform chestorange;
    public Transform chestgreen;

    public PlayerInfo playerinfo;
    public Slider hpSlider;
    private int canpink;
    private int canorange;
    private int cangreen;
    private int canpuple;
    private bool isHealingpink = false;
    private bool isHealingorange = false;
    private bool isHealinggreen = false;
    private bool isHealingpuple = false;

    // Start is called before the first frame update
    void Start()
    {
        canpink = 0;
        canorange = 0;
        cangreen = 0;
        canpuple = 0;
    }

    // Update is called once per frame
    void Update()
    {
        chestpink = GameObject.FindGameObjectWithTag("chestpink").transform;
        chestpurple = GameObject.FindGameObjectWithTag("chestpuple").transform;
        chestorange = GameObject.FindGameObjectWithTag("chestorange").transform;
        chestgreen = GameObject.FindGameObjectWithTag("chestgreen").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector3.Distance(player.position, chestpink.position) < 3f)
        {
            if (canpink < 1 && !isHealingpink)
            {
                StartCoroutine(HealPlayerpink());
            }
        }
        if (Vector3.Distance(player.position, chestorange.position) < 3f)
        {
            if (canorange< 1 && !isHealingorange)
            {
                StartCoroutine(HealPlayerorange());
            }
        }
        if (Vector3.Distance(player.position, chestgreen.position) < 3f)
        {
            if (cangreen < 1 && !isHealinggreen)
            {
                StartCoroutine(HealPlayergreen());
            }
        }
        if (Vector3.Distance(player.position, chestpurple.position) < 3f)
        {
            if (canpuple < 1 && !isHealingpuple)
            {
                StartCoroutine(HealPlayerpuple());
            }
        }
    }

    IEnumerator HealPlayerpink()
    {
        isHealingpink = true;
        playerinfo.health += 3f;
        canpink++;
        hpSlider.value = playerinfo.health / 100f;

        yield return new WaitForSeconds(1f); // 1초 동안 대기

        isHealingpink = false;
    }
    IEnumerator HealPlayerorange()
    {
        isHealingorange = true;
        playerinfo.health += 3f;
        canorange++;
        hpSlider.value = playerinfo.health / 100f;

        yield return new WaitForSeconds(1f); // 1초 동안 대기

        isHealingorange = false;
    }
    IEnumerator HealPlayergreen()
    {
        isHealinggreen = true;
        playerinfo.health += 3f;
        cangreen++;
        hpSlider.value = playerinfo.health / 100f;

        yield return new WaitForSeconds(1f); // 1초 동안 대기

        isHealinggreen = false;
    }
    IEnumerator HealPlayerpuple()
    {
        isHealingpuple = true;
        playerinfo.health += 3f;
        canpuple++;
        hpSlider.value = playerinfo.health / 100f;

        yield return new WaitForSeconds(1f); // 1초 동안 대기

        isHealingpuple = false;
    }
}
