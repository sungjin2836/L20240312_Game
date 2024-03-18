class SpriteRenderer : Renderer
{
    public SpriteRenderer()
    {
    }

    public char? shape;

    public override void Render()
    {
        if(transform != null)
        { 
            Console.SetCursorPosition(transform.x, transform.y); // 트랜스폼을 바로 가져올 수 있는 이유가 gameobject에
            Console.WriteLine(shape);
        }
    }



}
