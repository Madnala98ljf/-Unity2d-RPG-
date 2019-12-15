using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
 
 
public class Loadprogress : MonoBehaviour {
    private AsyncOperation aync;
    public Image load;//进度条的图片
    private int culload=0;//已加载的进度
    public Text loadtext;//百分制显示进度加载情况
    public int num;
    public AudioSource[] Gameasound;

	void Start () {
        for(int i=0;i<Gameasound.Length;i++){
            Gameasound[i].Stop();
        }
        //开启一个协程，既不是进程也不是线程，目前了解不够深刻。
        //目前简单的理解是，协程会根据迭代器中yield return 来判断什么时候暂时退出当前函数
        //然后在下一帧或者下一段时间中继续执行yield return 后面的函数代码
        //使用协程是为了简化代码的复杂度，将代码分成不同段在不同的帧里面执行以及实现延时的效果
        //startCoroutine会在第一次运行后一直执行，直到有代码控制它停止
    
        StartCoroutine("LoadScence");
	}
 
 
    //定义一个迭代器，每一帧返回一次当前的载入进度，同时关闭自动的场景跳转
    //因为LoadScenceAsync每帧加载一部分游戏资源，每次返回一个有跨越幅度的progress进度值
    //当游戏资源加载完毕后，LoadScenceAsync会自动跳转场景，所以并不会显示进度条达到了100%
    //关闭自动场景跳转后，LoadSceneAsync只能加载90%的场景资源，剩下的10%场景资源要在开启自动场景跳转后才加载
    IEnumerator LoadScence()
    {
        aync = SceneManager.LoadSceneAsync(num);//name为要跳转的场景
        aync.allowSceneActivation = false;
        yield return aync;
    }
	
	void Update () {
        //判断是否有场景正在加载
        if (aync == null)
        {
            return;
        }
        int progrssvalue = 0;
        //当场景加载进度在90%以下时，将数值以整数百分制呈现，当资源加载到90%时就将百分制进度设置为100，
        if (aync.progress < 0.9f)
        {
            progrssvalue = (int)aync.progress * 100;
        }
        else
        {
            progrssvalue = 100;
        }
        //每帧对进度条的图片和Text百分制数据进行更改，为了实现数字的累加而不是跨越，用另一个变量来实现。
        if (culload < progrssvalue)
        {
            culload++;
            load.fillAmount = culload / 100f;
            loadtext.text = culload.ToString() + "%";
        }
        //一旦进度到达100时，开启自动场景跳转，LoadSceneAsync会加载完剩下的10%的场景资源
        if (culload == 100)
        {
            aync.allowSceneActivation = true;
        }
	}
}