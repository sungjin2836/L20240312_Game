class Component
{
    public Component()
    {
        //gameObject = null;
        //transform = null;
        //gameObject = new GameObject();
        //transform = new Transform();
    }
    ~Component()
    {
    }
    
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    //내가 어디 속해 있는지 확인하는 용도
    public GameObject gameObject;
    //내가 속해 있는 게임오브젝트의 이동을 처리하기 위해
    public Transform transform;

    
}
