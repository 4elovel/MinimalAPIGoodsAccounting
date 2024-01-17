namespace MinimalAPIGoodsAccounting;

public class Good
{
    public Good(string name)
    {
        this.name = name;
        _idIterator++;
        id = _idIterator;
    }
    public Good(string name, IFormFile photo)
    {
        this.name = name;
        _idIterator++;
        id = _idIterator;
        this.photo = photo;
    }
    public string name { get; set; }
    public IFormFile photo { get; set; }

    public int id { get; }
    static int _idIterator;

}
