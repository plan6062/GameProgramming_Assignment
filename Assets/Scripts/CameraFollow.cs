using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 따라갈 캐릭터의 Transform
    public Vector3 offset;    // 캐릭터와 카메라 사이의 거리 (오프셋)
    public float minDistance = 0.5f; // 카메라가 캐릭터에 접근할 수 있는 최소 거리
    public LayerMask obstacleMask;   // 장애물로 간주할 레이어 마스크

    private Vector3 desiredPosition;

    void OnEnable()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // 카메라가 위치할 목표 위치 계산 (기본 오프셋 위치)
        desiredPosition = target.position + offset;

        // 카메라와 캐릭터 사이에 장애물이 있는지 Raycast로 확인
        RaycastHit hit;
        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out hit, offset.magnitude, obstacleMask))
        {
            // 장애물이 있을 경우, 장애물 위치에 카메라를 더 가까이 배치
            float distanceToObstacle = Vector3.Distance(target.position, hit.point) - 0.1f; // 살짝 여유를 둠
            transform.position = target.position + (desiredPosition - target.position).normalized * Mathf.Max(distanceToObstacle, minDistance);
        }
        else
        {
            // 장애물이 없을 경우, 기본 위치로 카메라 이동
            transform.position = desiredPosition;
        }
    }
}
