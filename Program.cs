using System.Reflection;

class Program
{
    
    static void Main(string[] args)
    {
        Engine engine = Engine.GetInstance();

        engine.RenderSort();
        engine.Init(); //자동으로 될 수도 있음 생성자
        engine.LoadScene("level03.map");
        engine.Run();
        engine.Term();
        Engine.GetInstance().Term(); //정리하기 싫으면 소멸자로 해도 됨
    }
      
}

