using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSkillController : MonoBehaviour
{
    public float m_CD_0; // 冷却时间
    public float m_CD_Left;
    public float m_CD_Right;
    public bool CD_Trigger;
    public bool Use_Trigger;
    public Image m_Masks;
    public Text m_Texts;
    public string s;
    public AudioSource audio;

    void Awake()
    {
        CD_Trigger = false; // 初始化为false，在Update()中，跳过冷却计时代码；
        Use_Trigger = false;
        m_CD_Left = m_CD_0; // 初始化为对应冷却时间
        m_CD_Right = m_CD_0 * 0.25f;
        m_Masks.enabled = false; // 禁用Mask，确保开始时，技能图标效果为可用；
        m_Texts.enabled = false; // 禁用Text，确保开始时，技能图标上无冷却数字显示；
    }

    void FixedUpdate()
    {
        if (Variable.IsPause == false)
        {
            if (!CD_Trigger)
            {
                // 如果技能不再冷却中，则释放技能并开始冷却计时；如果技能在冷却，则跳过；
                if (Variable.IsArrowSkillTrigger)
                {
                    Variable.IsArrowSkillTrigger = false;
                    audio.Play ();
                    CD_Trigger = true; // 赋值为True，下一个frame，开始冷却计时
                    Use_Trigger = true;
                    m_Masks.enabled = true; // 启用Mask（Image）
                    m_Masks.fillAmount = 1; // FillAmount设为1，确保效果显示正确
                    m_Texts.enabled = true; // 启用Text，显示冷却数字
                }
            }

            // 此处我有多个按钮需要不同的CD，因此用了数组;
            // CD_Trigger[0] 是一个bool值，用于判断是否开始冷却计时；
            if (CD_Trigger)
            {
                m_CD_Left -= Time.deltaTime; // m_CD_Left 冷却开始后，计算冷却剩余时间
                m_CD_Right -= Time.deltaTime;
                m_Masks.fillAmount = m_CD_Left / m_CD_0;
                // 更新对应mask的Image.FillAmount, 由于FillAmount是[0,1]，要换算成对应范围的小数
                m_Texts.text = string.Format("{0:F1}", m_CD_Left) + "s";
                // 更新技能文本中的数字显示，采用string.Format，详情可参考C#官方文档，“F1”表示一位小数

                if (m_CD_Left < 0)
                {
                    Variable.IsArrowSkilling = false;
                    // 如果剩余冷却时间为0，则停止冷却，并重新初始化相关变量。
                    CD_Trigger = false; // 下一个frame开始将不再执行if (CD_Trigger[0]){...}语句块的代码；
                    m_CD_Left = m_CD_0; // 剩余冷却时间重新赋值为初始值
                    m_CD_Right = m_CD_0 * 0.25f;
                    m_Masks.enabled = false; // Mask被禁用，不显示在图标上
                    m_Texts.enabled = false; // Text被禁用，不显示数值
                }

                if (m_CD_Right < 0 && Use_Trigger)
                {
                    Use_Trigger = false;
                }
            }
        }
    }
}