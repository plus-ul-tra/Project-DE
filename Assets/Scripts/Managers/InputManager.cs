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
            // Key ������ �ִ� ��� Invoke()
            KeyAction.Invoke();
        }

        // ���콺 Ŭ���� �ִ� ��� �ؿ� �߰�
        
    }
}
