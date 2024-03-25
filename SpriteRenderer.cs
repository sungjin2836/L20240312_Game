using SDL2;

public enum RenderOrder
{
    None = 0,
    Floor = 100,
    Wall = 200,
    Goal = 300,
    Player = 400,
    Monster = 500,
}
class SpriteRenderer : Renderer
{
    public string textureName;

    public SDL.SDL_Color colorKey;

    public bool isMultiple = false;

    public int spriteCount = 0;

    protected int currentIndex = 0;

    public ulong currentTime = 0;

    public ulong executeTime = 250;


    //IntPtr mySurface;
    //IntPtr myTexture;
    public SpriteRenderer()
    {
        renderOrder = RenderOrder.None;
        textureName = "";
        colorKey.r = 255;
        colorKey.g = 255;
        colorKey.b = 255;
        colorKey.a = 255;

        
        //LoadTexture
        //string Dir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        //
        //mySurface = SDL.SDL_LoadBMP(Dir+"/data/"+"floor.bmp"); //
        //myTexture = SDL.SDL_CreateTextureFromSurface(Engine.GetInstance().myRenderer, mySurface); //Vram / 정사각형이 되야함
    }

    public void Load(string _textureName)
    {
        textureName = _textureName;

        ResourceManager.Load(textureName, colorKey);
    }

    public char? shape;
    public RenderOrder renderOrder;

    public override void Update()
    {
        if (isMultiple)
        {
            currentTime += Engine.GetInstance().deltaTime;
            if (currentTime >= executeTime)
            {
                currentIndex++;
                currentIndex = currentIndex % spriteCount;
                currentTime = 0;
            }
        }
    }

    public override void Render()
    {

        //SDL.SDL_GetTicks(); // int32로 받아서 40분이 넘어가면 0으로 바뀜

        if (transform != null)
        {
            //Console.SetCursorPosition(transform.x, transform.y); // 트랜스폼을 바로 가져올 수 있는 이유가 gameobject에
            //Console.WriteLine(shape);

            Engine myEngine = Engine.GetInstance();

            SDL.SDL_Rect destRect = new SDL.SDL_Rect();
            destRect.x = transform.x * 30;
            destRect.y = transform.y * 30;
            destRect.w = 30;
            destRect.h = 30;
            //SDL.SDL_RenderFillRect(myEngine.myRenderer, ref myRect);

            //unsafe
            //{
            //SDL.SDL_Surface* surface = (SDL.SDL_Surface*)mySurface;

            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            uint format = 0;
            int acess = 0;
            SDL.SDL_QueryTexture(ResourceManager.Find(textureName),
                    out format,
                    out acess,
                    out rect.w,
                    out rect.h);
            
            if (isMultiple)
            {
                //animation
                int spriteWidth = rect.w / spriteCount;
                int spriteHeight = rect.h / spriteCount;

                rect.x = spriteWidth * currentIndex;
                rect.y = spriteHeight * Engine.GetInstance().moveIndex; //방향
                rect.w = spriteWidth;
                rect.h = spriteHeight;
            }
            else
            {
                rect.x = 0;
                rect.y = 0;
            }

            SDL.SDL_RenderCopy(myEngine.myRenderer,
                ResourceManager.Find(textureName),
                ref rect,
                ref destRect);
            //}
        }
    }



}
