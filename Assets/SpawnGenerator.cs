using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100; // 찍어내는 적의 개수
    public LayerMask obstacleLayer; // 장애물 레이어
    public float checkRadius = 0.5f; // 충돌 체크 반경
    public float spawnHeight = 1f; // 스폰 높이

    private List<GameObject> props = new List<GameObject>();

    void Start()
    {
        area = GetComponent<BoxCollider>();

        for (int i = 0; i < count; i++)
        {
            Spawn();
        }

        area.enabled = false; //생성후 콜라이더 제거
    }

    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        Vector3 spawnPos = GetValidSpawnPosition();
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        props.Add(instance);
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPos;
        int attempts = 0;
        int maxAttempts = 50; // 최대 시도 횟수

        do
        {
            spawnPos = GetRandomPosition();
            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("최대 시도 횟수를 초과했습니다. 마지막 위치를 사용합니다.");
                break;
            }
        }
        while (Physics.CheckSphere(spawnPos, checkRadius, obstacleLayer));

        return spawnPos;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x/2f, size.x/2f);
        float posZ = basePosition.z + Random.Range(-size.z/2f, size.z/2f);

        // 레이캐스트를 사용하여 지면 높이 확인
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(posX, basePosition.y + size.y, posZ), Vector3.down, out hit, size.y * 2, ~obstacleLayer))
        {
            return hit.point + Vector3.up * spawnHeight;
        }
        else
        {
            // 레이캐스트가 실패한 경우 기본 높이 사용
            return new Vector3(posX, basePosition.y, posZ);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetValidSpawnPosition();
            props[i].SetActive(true);
        }
    }
}