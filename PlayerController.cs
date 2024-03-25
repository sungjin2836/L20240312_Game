using SDL2;

class PlayerController : Component
{
    public SpriteRenderer spriteRenderer;

    public void start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public override void Update()
    {

        

        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (transform == null)
        {
            return;
        }

        int oldX = transform.x;
        int oldY = transform.y;


        if (Input.GetKey(SDL.SDL_Keycode.SDLK_a))
        {
            transform.Translate(-1, 0);
            Engine.GetInstance().moveIndex = 0;
            //spriteRenderer.currentIndex = 0; 스프라이트랜더러에서 받아올경우
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_d))
        {
            transform.Translate(1, 0);
            Engine.GetInstance().moveIndex = 1;
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_w))
        {
            transform.Translate(0, -1);
            Engine.GetInstance().moveIndex = 2;
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_s))
        {
            transform.Translate(0, 1);
            Engine.GetInstance().moveIndex = 3;
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_ESCAPE))
        {
            Engine.GetInstance().Stop();
        }
        transform.x = Math.Clamp(transform.x, 0, 80);
        transform.y = Math.Clamp(transform.y, 0, 80);

        //Collider2D 컴포넌트를 가지는 모든 게임 오브젝트를 찾는다
        //자기 자신은 제외
        //Player와의 충돌 체크
        foreach (GameObject findGameObject in Engine.GetInstance().gameObjects)
        {
            if(findGameObject == gameObject)
            {
                //자신 오브젝트는 제외
                 continue;
            }
            Collider2D? findComponent = findGameObject.GetComponent<Collider2D>();
            if (findComponent != null)
            {
                if (findComponent.Check(gameObject) && findComponent.isTrigger == false)
                {
                    //충돌
                    transform.x = oldX;
                    transform.y = oldY;
                    //OnCollide(GameObject other);
                    break;
                }
                if (findComponent.Check(gameObject) && findComponent.isTrigger == true)
                {
                    OnTrigger(findGameObject);
                }
            }
        }
        //find new x, new y 해당하는 게임오브젝트 탐색
        
        //찾은 게임 오브젝트에서 Collider2D 그리고 충돌 체크

    }



    public void OnTrigger(GameObject other)
    {
        //겹쳤을때 처리 할 로직
        if(other.name == "Monster")
        {
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isGameOver = true;
            Console.WriteLine("Monster");
            //GameOver
        }
        else if (other.name == "Goal")
        {
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isNextStage = true;
            //Console.WriteLine("Goal");
            
            //다음판
        }
    }
}
