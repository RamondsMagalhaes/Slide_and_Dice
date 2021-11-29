using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageCycle
{
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten
};

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject tier1Prefab, tier2Prefab, tier3Prefab, tier4Prefab, tier5Prefab, projectilePrefab, deathRayPrefab;
    [SerializeField] Transform tier1Parent, tier2Parent, tier3Parent, tier4Parent, tier5Parent;
    StageCycle stageCycle;
    GameObject player;
    DialogueStageGeneric dialogueStage;
    void Start()
    {
        stageCycle = StageCycle.one;
        player = GameObject.Find("Player");
        dialogueStage = FindObjectOfType<DialogueStageGeneric>();
        if (GameManager.controller != 2)
        {
            GameObject.Find("Touch Canvas").SetActive(false);
        }
        StartCoroutine(StageCycleCoroutine());
        
    }
    IEnumerator StageCycleCoroutine()
    {
        switch (stageCycle)
        {
            case StageCycle.one:
                StartCoroutine(StageOne());
                stageCycle = StageCycle.two;
                break;
            case StageCycle.two:
                StartCoroutine(StageTwo());
                stageCycle = StageCycle.three;
                break;
            case StageCycle.three:
                StartCoroutine(StageThree());
                stageCycle = StageCycle.four;
                break;
            case StageCycle.four:
                StartCoroutine(StageFour());
                stageCycle = StageCycle.five;
                break;
            case StageCycle.five:
                StartCoroutine(StageFive());
                stageCycle = StageCycle.six;
                break;
            case StageCycle.six:
                StartCoroutine(StageSix());
                stageCycle = StageCycle.seven;
                break;
            case StageCycle.seven:
                StartCoroutine(StageSeven());
                stageCycle = StageCycle.eight;
                break;
            case StageCycle.eight:
                StartCoroutine(StageEight());
                stageCycle = StageCycle.nine;
                break;
            case StageCycle.nine:
                StartCoroutine(StageNine());
                stageCycle = StageCycle.ten;
                break;
            case StageCycle.ten:
                StartCoroutine(StageTen());
                stageCycle = StageCycle.one;
                break;
            default:
                break;
        }
        yield return null;
    }
    #region Stages Coroutines
    IEnumerator StageOne()
    {
        yield return null;
    }
    IEnumerator StageTwo()
    {
        yield return null;
    }
    IEnumerator StageThree()
    {
        yield return null;
    }
    IEnumerator StageFour()
    {
        yield return null;
    }
    IEnumerator StageFive()
    {
        yield return null;
    }
    IEnumerator StageSix()
    {
        yield return null;
    }
    IEnumerator StageSeven()
    {
        yield return null;
    }
    IEnumerator StageEight()
    {
        yield return null;
    }
    IEnumerator StageNine()
    {
        yield return null;
    }
    IEnumerator StageTen()
    {
        yield return null;
    }
    #endregion

    private void Update()
    {
        
    }
    public Vector3 RandomArea()
    {
        Vector3 area;
        float rangeX, rangeY, camOrtho = Camera.main.orthographicSize;
        rangeX = camOrtho + ((camOrtho / 5)+1);
        rangeY = camOrtho - 1;
        do
        { 
            area = new Vector3( Random.Range(-rangeX, rangeX),
                                Random.Range(-rangeY, rangeY),
                                0);
        } while ( Vector3.Distance(area, player.transform.position) < 5 );
        return area;
    }
    public Vector3 RandomAreaOnTop()
    {
        Vector3 area;
        float rangeX, rangeY, camOrtho = Camera.main.orthographicSize;
        rangeX = camOrtho + ((camOrtho / 5) + 1);
        rangeY = camOrtho - 1;
        area = new Vector3(Random.Range(-rangeX, rangeX),
                            rangeY,
                            0);
        return area;
    }

    public GameObject SpawnTierOne()
    {
        return Instantiate( tier1Prefab, RandomArea(),Quaternion.identity,tier1Parent);
    }
    public GameObject SpawnTierTwo()
    {
        return Instantiate(tier2Prefab, RandomArea(), Quaternion.identity, tier2Parent);
    }
    public GameObject SpawnTierThree()
    {
        return Instantiate(tier3Prefab, RandomArea(), Quaternion.identity, tier3Parent);
    }
    public GameObject SpawnTierFour()
    {
        return Instantiate(tier4Prefab, RandomArea(), Quaternion.identity, tier4Parent);
    }
    public GameObject SpawnTierFive()
    {
        return Instantiate(tier5Prefab, RandomArea(), Quaternion.identity, tier5Parent);
    }
    public GameObject SpawnProjectile()
    {
        return Instantiate(projectilePrefab, RandomArea(), Quaternion.identity);
    }
    public GameObject SpawnDeathRay()
    {
        return Instantiate(deathRayPrefab, RandomAreaOnTop(), Quaternion.identity);
    }

}
