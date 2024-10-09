using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100; // 찍어내는 적의 개수

    private List<GameObject> props = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for(int i = 0; i<count; i++){
            Spawn();
        }

        area.enabled = false; //생성후 콜라이더 제거

    }

    private void Spawn(){
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        Vector3 spawnPos = GetRandomPosition();
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        props.Add(instance);
    }

    private Vector3 GetRandomPosition(){
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x/2f, size.x/2f); //x 길이 왼쪽, 오른쪽 절반
        float posY = basePosition.y + Random.Range(-size.y/2f, size.y/2f); //y 길이 왼쪽, 오른쪽 절반
        float posZ = basePosition.z + Random.Range(-size.z/2f, size.z/2f); //z 길이 왼쪽, 오른쪽 절반

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    public void Reset(){
        for(int i = 0; i<props.Count; i++){
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}