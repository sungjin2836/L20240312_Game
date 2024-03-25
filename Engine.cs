using SDL2;
using System;
class Engine
{
    protected Engine()
    {
        gameObjects = new List<GameObject>();
        isRunning = true;
    }

    ~Engine()
    {

    }

    
    private static Engine? instance;

    public static Engine GetInstance()
    {
        if(instance == null)
        {
            instance = new Engine();
        }
        return instance;

        //return instance ?? (instance = new Engine()); null이면 new Engine() 반환,아니면 instance 리턴
    }

    public int stageNum = 1;

    public List<GameObject> gameObjects;
    public bool isRunning;

    public bool isNextLoading = false;
    public string nextSceneName = string.Empty;

    public IntPtr myWindow;
    public IntPtr myRenderer;
    public SDL.SDL_Event myEvent;

    public ulong deltaTime;
    protected ulong lastTime;

    public int moveIndex;

    public void NextLoadScene(string _nextSceneName)
    {
        isNextLoading = true;
        nextSceneName = _nextSceneName;
    }

    public void Init() //default 생성자
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
        {
            Console.WriteLine("Init fail.");
            return;
        } // 하드웨어 초기화


        myWindow = SDL.SDL_CreateWindow("2D Engine", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        myRenderer = SDL.SDL_CreateRenderer(myWindow, -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
            SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

        Input.Init();

        lastTime = SDL.SDL_GetTicks64();
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void LoadScene(string sceneName)
    {

//#if DEBUG // debug할때 구간 디버그에 사용

        string Dir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
        string[] map = File.ReadAllLines(Dir + "/../data/"+sceneName);
        //Console.WriteLine(Dir);
        //#endif
        GameObject newGameObject;
        for (int y = 0; y < map.Length; ++y)
        {
            for (int x = 0; x < map[y].Length; ++x)
            {
                if (map[y][x] == '*')
                {
                    newGameObject = Instantiate<GameObject>(); // 생성은 Instantiate<T>에서 하게 변경
                    newGameObject.name = "Wall";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = '*';
                    renderer.Load("wall.bmp");
                    renderer.renderOrder = RenderOrder.Wall;
                    newGameObject.AddComponent<Collider2D>();

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = ' ';
                    renderer.Load("floor.bmp");

                    renderer.renderOrder = RenderOrder.Floor;

                }
                else if (map[y][x] == ' ') // 벽 밑에도 바닥이 있음
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = ' ';
                    renderer.Load("floor.bmp");

                    renderer.renderOrder = RenderOrder.Floor;
                }
                else if (map[y][x] == 'G')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Goal";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.renderOrder = RenderOrder.Goal;
                    renderer.shape = 'G';
                    renderer.Load("coin.bmp");
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = ' ';
                    renderer.Load("floor.bmp");

                    renderer.renderOrder = RenderOrder.Floor;
                }
                else if (map[y][x] == 'P')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Player";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = 'P';
                    renderer.colorKey.g = 0;
                    renderer.Load("test.bmp");
                    renderer.isMultiple = true;
                    renderer.spriteCount = 5;
                   


                    renderer.renderOrder = RenderOrder.Player;
                    newGameObject.AddComponent<PlayerController>();
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = ' ';
                    renderer.Load("floor.bmp");


                    renderer.renderOrder = RenderOrder.Floor;
                }
                else if (map[y][x] == 'M')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Monster";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.renderOrder = RenderOrder.Monster;
                    renderer.shape = 'M';
                    renderer.Load("Slime.bmp");

                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;
                    newGameObject.AddComponent<AIController>();

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;
                }
            }
        }

        newGameObject = Instantiate<GameObject>();
        newGameObject.name = "GameManager";
        newGameObject.AddComponent<GameManager>();

        RenderSort(); // 직접 Sort() 구현
        //gameObjects.Sort();
    }

    public void RenderSort()
    {
        

        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = i + 1; j < gameObjects.Count; j++)
            {
                //GameObject prevObject = gameObjects[i]; // 임시값이라서 이걸 변경해도 의미 없음
                //GameObject nextObject = gameObjects[j];
                SpriteRenderer? prevRender = gameObjects[i].GetComponent<SpriteRenderer>();
                SpriteRenderer? nextRender = gameObjects[j].GetComponent<SpriteRenderer>();
                if(prevRender != null && nextRender != null)
                {
                    if ((int)prevRender.renderOrder > (int)nextRender.renderOrder)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }
                }

                
            }
        }
        //for (int i = 0;i < number.Length; i++)
        //{
        //    Console.WriteLine(number[i]);
        //}
    }

    public void Run()
    {
        bool isFirst = true;
        while (isRunning)
        {
            if (isFirst)
            {
                StartInAllComponent();
                isFirst = false;
            }
            
            ProcessInput(); //생성 로드
            Update();
            Render();
            if (isNextLoading)
            {
                gameObjects.Clear();
                LoadScene(nextSceneName);
                isNextLoading = false;
                nextSceneName = string.Empty;
            }
            //ulong lastTime = SDL.SDL_GetTicks64();
        } // frame 
    }

    protected void StartInAllComponent()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Start();
            }
        }
    }

    public void Term()
    {
        gameObjects.Clear();

        SDL.SDL_DestroyRenderer(myRenderer);
        SDL.SDL_DestroyWindow(myWindow);
        SDL.SDL_Quit();
    }

    public T Instantiate<T>() where T : GameObject, new() //생성을 여기서 하고 LoadScene에서는 로드만
    {
        T newObject = new T();
        gameObjects.Add( newObject );
        return newObject;
    }

    //public GameObject Instantiate(GameObject newGameObject)
    //{
    //    gameObjects.Add(newGameObject);
        
    //    return newGameObject;
    //}

    public void ProcessInput()
    {   
        SDL.SDL_PollEvent(out myEvent); //윈도우 화면 이벤트 입력
        //Input.keyInfo = Console.ReadKey();

    }

    public void Update()
    {
        deltaTime = SDL.SDL_GetTicks64() - lastTime;
        //Console.WriteLine(deltaTime);
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Update();
            }
        }
        lastTime = SDL.SDL_GetTicks64();
    }

    public void Render()
    {
        //Console.Clear();
        SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
        SDL.SDL_RenderClear(myRenderer);
        foreach (GameObject gameObject in gameObjects)
        {
            Renderer? renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.Render();
            }
        }

        SDL.SDL_RenderPresent(Engine.GetInstance().myRenderer);
    }

    public GameObject? Find(string name)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if(gameObject.name == name)
            {
                return gameObject;
            }
        }

        return null;
    }
     


    //protected void Render()
    //{
    //    //for (int i = 0; i < gameObjects.Count; i++) //Dictionary면 순서대로 안됨
    //    //{
    //    //    gameObjects[i].Render();
    //    //}
    //    Console.Clear();

    //    foreach (GameObject gameObject in gameObjects)
    //    {
    //        gameObject.Render();
    //    }
    //}




}

