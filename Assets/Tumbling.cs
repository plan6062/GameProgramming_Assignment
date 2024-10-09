using UnityEngine;

public class Tumbling
{
    public float uprightForce = 30f;  // 일어나는 힘 (토크)
    public float maxTiltAngle = 0.1f;  // 최대 허용 기울기 각도
    public float torqueMultiplier = 10f;  // 토크의 강도 조절
    private Rigidbody rb;
    

    // 생성자에서 Rigidbody를 받아옴
    public Tumbling(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    // 캐릭터가 기울어졌는지 확인하는 메서드
    public bool IsTilted()
    {
        // 캐릭터의 Up 벡터와 월드 Up 벡터 간의 각도 계산
        float tiltAngle = Vector3.Angle(rb.transform.up, Vector3.up);
        return tiltAngle > maxTiltAngle;
    }

    // 캐릭터를 자동으로 서서히 일으키는 로직 (토크 사용)
    public void RightCharacter()
    {
        if (IsTilted()) // 기울어졌을 때만 작동
        {
            // 현재 기울기와 회복 방향 계산
            Vector3 uprightTorqueDirection = Vector3.Cross(rb.transform.up, Vector3.up);

            // 토크를 가하여 캐릭터를 세움
            rb.AddTorque(uprightTorqueDirection * uprightForce * torqueMultiplier, ForceMode.Acceleration);
        }
    }
}
