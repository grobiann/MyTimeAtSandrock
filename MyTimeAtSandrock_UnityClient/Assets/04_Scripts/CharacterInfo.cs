public class CharacterInfo : ITableInfo
{
    public int key;
    public string name;
    public int hp;
    public int damage;
    public int moveSpeed;
    public string resourcePath;

    public int Key => key;
}


public interface ITableInfo
{
    public int Key { get; }
}

public class ConstInfo
{

}

public class BuildInfo : ITableInfo
{
    public int key;

    public int Key => key;
}