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


    public List<GameObject> gameObjects;
    public bool isRunning;
    


    public void Init() //default 생성자
    {

        Input.Init();
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

        for (int y = 0; y < map.Length; ++y)
        {
            for (int x = 0; x < map[y].Length; ++x)
            {
                if (map[y][x] == '*')
                {
                    GameObject newGameObject = Instantiate(new GameObject());
                    newGameObject.name = "Wall";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = '*';

                    
                }
                else if (map[y][x] == ' ')
                {
                    GameObject newGameObject = Instantiate(new GameObject());
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    //SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    //renderer.shape = ' ';
                }
                else if (map[y][x] == 'G')
                {
                    GameObject newGameObject = Instantiate(new GameObject());
                    newGameObject.name = "Goal";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = 'G';
                }
                else if (map[y][x] == 'P')
                {
                    GameObject newGameObject = Instantiate(new GameObject());
                    newGameObject.name = "Player";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = 'P';
                    newGameObject.AddComponent<PlayerController>();
                }
                else if (map[y][x] == 'M')
                {
                    GameObject newGameObject = Instantiate(new GameObject());
                    newGameObject.name = "Monster";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddComponent<SpriteRenderer>();
                    renderer.shape = 'M';
                    //newGameObject.AddComponent<PlayerController>();
                }
            }
        }

        //gameObjects.Sort();
    }

    public void Run()
    {
        while (isRunning)
        {
            ProcessInput(); //생성 로드
            Update();
            Render();
        } // frame 
    }

    public void Term()
    {
        gameObjects.Clear();
    }

    public GameObject Instantiate(GameObject newGameObject)
    {
        gameObjects.Add(newGameObject);
        
        return newGameObject;
    }

    public void ProcessInput()
    {
        Input.keyInfo = Console.ReadKey();

    }

    public void Update()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Update();
            }
        }
    }

    public void Render()
    {
        Console.Clear();
        foreach (GameObject gameObject in gameObjects)
        {
            Renderer? renderer = gameObject.GetComponent<Renderer>();
            if(renderer != null)
            {
                renderer.Render();
            }
        }
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

