using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKey
{
    public static bool IsPressingSkill = false;
    
    public static bool TriggerLeft = false;
    public static bool TriggerRight = false;
    
    public static bool TriggerUp = false;
    public static bool TriggerDown = false;
    
    public static bool TriggerSkill = false;
    
    public static bool TriggerArrowSkill = false;
    
    public static bool TriggerIceSkill = false;
    
    public static bool TriggerFireSkill = false;
    
    public static int HorizontalDirection
    {
        get
        {
            if (TriggerLeft && TriggerRight)
            {
                return 0;
            }
            else if (TriggerLeft && !TriggerRight)
            {
                return -1;
            }
            else if (TriggerRight && !TriggerLeft)
            {
                return 1;
            }

            return 0;
        }
    }
    public static int VerticalDirection
    {
        get
        {
            if (TriggerUp && TriggerDown)
            {
                return 0;
            }
            else if (TriggerDown && !TriggerUp)
            {
                return -1;
            }
            else if (TriggerUp && !TriggerDown)
            {
                return 1;
            }

            return 0;
        }
    }
}
