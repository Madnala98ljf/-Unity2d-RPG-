using UnityEngine;
using UnityEngine.UI;

public class TestScroll : MonoBehaviour
{
    //设置ScrollRect变量

    ScrollRect rect;

    void Start()
    {
        //获取 ScrollRect变量
        rect = this.GetComponent<ScrollRect>();
    }

    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
            return;
        //在FixedUpdate函数中调用ScrollValue函数
        ScrollValue();
    }

    private void ScrollValue()
    {
        //当对应值超过1，重新开始从 0 开始
        if (rect.verticalNormalizedPosition <= 0.0f)
        
        {
            rect.verticalNormalizedPosition = 1.0f;
        }

        //逐渐递增 ScrollRect 水平方向上的值
        rect.verticalNormalizedPosition = rect.verticalNormalizedPosition - 0.04f * Time.deltaTime;
    }
}