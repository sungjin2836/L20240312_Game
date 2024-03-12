class GameObject
{
    public int x; //접근하는게 싫으면 get;set; 사용해도 됨
    public int y;

    public GameObject()
    {
        x = 0;
        y = 0;
    }

    ~GameObject()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Render()
    {

    }

    public char shape;


}

