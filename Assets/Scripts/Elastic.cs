using UnityEngine;

public class Elastic
{
    // 기본 튕기는 힘의 배수
    public float baseBounceForce = 3f;

    // 벽과 바닥의 레이어 이름 설정
    public string[] excludedLayers = { "Wall", "Floor" };
    private int[] excludedLayerIndexes;
    private Rigidbody rb;

    // 생성자에서 Rigidbody를 받아옴
    public Elastic(Rigidbody rigidbody, string[] excludedLayers)
    {
        rb = rigidbody;
        this.excludedLayers = excludedLayers;

        // 제외할 레이어들의 인덱스 가져오기
        excludedLayerIndexes = new int[excludedLayers.Length];
        for (int i = 0; i < excludedLayers.Length; i++)
        {
            excludedLayerIndexes[i] = LayerMask.NameToLayer(excludedLayers[i]);
        }
    }

    // 충돌 시 탄성 효과를 처리하는 메서드
    public void HandleCollision(Collision collision)
    {
        // 충돌한 오브젝트가 벽 또는 바닥인지 확인
        if (!IsExcludedLayer(collision.gameObject.layer))
        {
            // 충돌한 상대 오브젝트의 Rigidbody 가져오기
            Rigidbody otherRigidbody = collision.rigidbody;

            // 상대가 Rigidbody를 가지고 있을 때만 작동
            if (otherRigidbody != null)
            {
                // 충돌 속도를 계산 (상대 속도)
                Vector3 relativeVelocity = collision.relativeVelocity;
                float impactSpeed = relativeVelocity.magnitude;

                // 충돌 속도에 따라 튕기는 힘을 계산
                float bounceForce = baseBounceForce * impactSpeed;

                // 튕기는 방향을 계산
                Vector3 bounceDirection = GetBounceDirection(otherRigidbody);

                // 상대 오브젝트에 튕기는 힘을 가하기
                ApplyBounceForce(otherRigidbody, bounceDirection, bounceForce);
            }
        }
    }

    // 제외할 레이어인지 확인하는 메서드
    private bool IsExcludedLayer(int layer)
    {
        foreach (int excludedLayer in excludedLayerIndexes)
        {
            if (layer == excludedLayer)
            {
                return true;
            }
        }
        return false;
    }

    // 튕기는 방향을 계산하는 메서드
    private Vector3 GetBounceDirection(Rigidbody otherRigidbody)
    {
        return (otherRigidbody.transform.position - rb.transform.position).normalized;
    }

    // 튕기는 힘을 가하는 메서드
    private void ApplyBounceForce(Rigidbody otherRigidbody, Vector3 bounceDirection, float bounceForce)
    {
        otherRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
