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
    public SpriteRenderer()
    {
        renderOrder = RenderOrder.None;
    }

    public char? shape;
    public RenderOrder renderOrder;

    public override void Render()
    {
        if(transform != null)
        { 
            Console.SetCursorPosition(transform.x, transform.y); // 트랜스폼을 바로 가져올 수 있는 이유가 gameobject에
            Console.WriteLine(shape);
        }
    }



}
