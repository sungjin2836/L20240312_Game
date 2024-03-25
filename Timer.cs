using static Timer;

class Timer
{

    //delegate 사용법
    public ulong executeTime = 0;
    protected ulong elapsedTime = 0;

    public delegate void Callback(); // 
    public Callback callback;

    public Timer(ulong _executeTime, Callback _callback)
    {
        SetTimer(_executeTime, _callback);
    }

    public void SetTimer(ulong _executeTime, Callback _callback)
    {
        executeTime = _executeTime;
        callback = _callback;
    }
    


    public void Update()
    {
        elapsedTime += Engine.GetInstance().deltaTime;

        if(elapsedTime >= executeTime)
        {
            //실행

            //함수를 등록해서 그 함수가 실행 되게
            callback();
            elapsedTime = 0;
        }
    }

}
