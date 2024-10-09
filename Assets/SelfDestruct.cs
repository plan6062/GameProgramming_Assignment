using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 10f;  // 오브젝트가 존재할 시간 (초)

    void Start()
    {
        // lifetime 후에 DestroyObject 메서드를 호출
        Invoke("DestroyObject", lifetime);
    }

    void DestroyObject()
    {
        // 이 스크립트가 붙어있는 게임 오브젝트를 제거
        Destroy(gameObject);
    }
}