using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CornState
{
    IDLE, //������ ������
    RUN,  //�޸���(�ȴ�) ��
    FLOAT, //���߿� �� (������ �� �Ѿ����� ����)
    BLOW, //�������� ƨ���� ���ư� (������ �� �Ѿ���)
    FALL, //�Ѿ����� �Ͼ�� ���� ���� (���� �Ұ���)
    POP //������ �� ����
}
