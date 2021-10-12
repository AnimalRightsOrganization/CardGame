using UnityEngine;

public class MainEntrance : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(640, 360, true); //设置分辨率
        Screen.fullScreen = false; //设置成全屏
        QualitySettings.vSyncCount = 0; //只能是0,1,2，0为不等待垂直同步
        Application.targetFrameRate = 90;
    }

    void Start()
    {
        //release版关闭，debug版打开
        //Debug.unityLogger.logEnabled = false;

        UIManager.GetInstance().Push<UI_MainMenu>();
    }
}