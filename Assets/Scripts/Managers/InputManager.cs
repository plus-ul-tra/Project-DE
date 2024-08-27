using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action MouseAction = null;

    public void OnUpdate()
    {
        if(Input.anyKey && KeyAction != null)
        {
            // Key 동작이 있는 경우 Invoke()
            KeyAction.Invoke();
        }

        // 마우스 클릭이 있는 경우 밑에 추가
        
    }
}
