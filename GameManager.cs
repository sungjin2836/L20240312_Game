using SDL2;

class GameManager : Component
{
    public bool isGameOver;
    public bool isNextStage;

    protected Timer gameOverTimer;
    protected Timer nextStageTimer;

    

    public GameManager()
    {
        isGameOver = false;
        isNextStage = false;
        gameOverTimer = new Timer(1000, ProcessGameOver);
        nextStageTimer = new Timer(1000, ProcessNextStage);
        //gameOverTimer.callback = ProcessGameOver;
    }

    public void ProcessGameOver()
    {
        //delegate를 void에 인자도 없이 만들어서 똑같이 만들어 줘야함
        Engine.GetInstance().Stop();
        Console.Clear();
        Console.WriteLine("GameOver");
    }

    public void ProcessNextStage()
    {
        Engine.GetInstance().stageNum++;
        int stageNumber = Engine.GetInstance().stageNum;
        SDL.SDL_RenderClear(Engine.GetInstance().myWindow);
        Console.WriteLine("Congraturation.");
        //SDL.SDL_FillRect(Engine.GetInstance().myRenderer, , SDL.SDL_MapRGB(screen->format, 255, 255, 255));
        //        isNextStage = false;
        Engine.GetInstance().NextLoadScene($"Level0{stageNumber}.map");
        if (stageNumber > 2)
        {
            Engine.GetInstance().stageNum = 0;
        }

    }

    public override void Update()
    {
        if(isGameOver)
        {
            gameOverTimer.Update();
        }

        if(isNextStage)
        {
            nextStageTimer.Update();
            //Engine.GetInstance().Term();
            //Engine.GetInstance().LoadScene("Level02.map");
        }
    }

}
