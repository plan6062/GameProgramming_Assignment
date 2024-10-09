using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CornState
{
    IDLE, //가만히 서있음
    RUN,  //달리는(걷는) 중
    FLOAT, //공중에 뜸 (착지할 때 넘어지지 않음)
    BLOW, //공중으로 튕겨져 날아감 (착지할 때 넘어짐)
    FALL, //넘어져서 일어나기 전의 상태 (조작 불가능)
    POP //팝콘이 된 상태
}
