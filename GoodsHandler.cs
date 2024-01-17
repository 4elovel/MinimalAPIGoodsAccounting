namespace MinimalAPIGoodsAccounting;

public class GoodsHandler
{
    List<Good> goods;
    public GoodsHandler(List<Good> goods)
    {
        this.goods = goods;
    }
    public void AddGood(Good good)
    {
        goods.Add(good);
    }
    public void RemoveGood(int goodId)
    {
        foreach (Good good in goods)
        {
            if (good.id == goodId)
            {
                goods.Remove(good);
            }
        }
    }
    public void UpdateGood(int goodId, string newName)
    {
        foreach (Good good in goods)
        {
            if (good.id == goodId)
            {
                good.name = newName;
            }
        }
    }
    public List<Good> GetGoods()
    {
        return goods;
    }
    public Good GetGood(int goodId)
    {
        foreach (Good good in goods)
        {
            if (good.id == goodId)
            {
                return good;
            }
        }
        return new Good(null);
    }
}