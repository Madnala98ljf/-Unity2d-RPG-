using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variable
{
    //是否运行暂停
    public static bool CanPaues = true;
    
    //是否暂停中
    public static bool IsPause = false;
    
    //背景音量
    public static float BackAsound = 1.0f;
    
    //游戏音量
    public static float GameAsound = 1.0f;
    
    //冰球
    public static bool IsIceSkilling = false;
    public static bool IsIceSkillTrigger = false;
    
    //火球
    public static bool IsFireSkilling = false;
    public static bool IsFireSkillTrigger = false;
}
