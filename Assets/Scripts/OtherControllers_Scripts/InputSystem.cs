using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem
{
    public static void ProcessInput()
    {
        if (Variable.IsPause)
        {
            return;
        }

        InputKey.TriggerLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        InputKey.TriggerRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        InputKey.TriggerUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        InputKey.TriggerDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        InputKey.TriggerSkill = Input.GetKey(KeyCode.J);
        InputKey.TriggerArrowSkill = Input.GetKey(KeyCode.U);
        InputKey.TriggerIceSkill = Input.GetKey(KeyCode.K);
        InputKey.TriggerFireSkill = Input.GetKey(KeyCode.L);
    }
}
