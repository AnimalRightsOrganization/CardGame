// 牌面属性
[System.Serializable]
public class CardAttribute
{
    public int cardid; //0~52序号（大小王去掉）
    public Colors colors; //花色
    public Weight weight; //牌面点数
    //public int value; //J11，Q12，K13

    // 根据id，序列化牌面属性
    public void SerializeCard()
    {
        if (this.cardid <= 13)
        {
            this.colors = Colors.Spade;
            this.weight = (Weight)(this.cardid % 14); //1-13
        }
        else if (this.cardid > 13 && this.cardid <= 26)
        {
            this.colors = Colors.Heart;
            this.weight = (Weight)(this.cardid % 14);
        }
        else if (this.cardid > 26 && this.cardid <= 39)
        {
            this.colors = Colors.Club;
            this.weight = (Weight)(this.cardid % 14);
        }
        else if (this.cardid > 39 && this.cardid <= 52)
        {
            this.colors = Colors.Square;
            this.weight = (Weight)(this.cardid % 14);
        }
    }
    public override string ToString()
    {
        return $"{colors}_{weight}:{cardid}";
    }
}

/// <summary>
/// 花色
/// </summary>
public enum Colors //黑桃>红桃>梅花>方块
{
    Spade,  //黑桃
    Heart,  //红桃
    Club,   //梅花
    Square, //方片
    King,   //王
}

/// <summary>
/// 权值 3,4,5,6,7,8,9,10,J,Q,K,A,2,SmallJoker,BigJoker
/// </summary>
public enum Weight
{
    //Ace     = 1,
    //Two     = 2,
    Three   = 3,
    Four    = 4,
    Five    = 5,
    Six     = 6,
    Seven   = 7,
    Eight   = 8,
    Nine    = 9,
    Ten     = 10,
    Jack    = 11,
    Queen   = 12,
    King    = 13,
    Ace     = 14,
    Two     = 15,
    SJoker  = 16,
    BJoker  = 17,
}

/// <summary>
/// 牌型
/// </summary>
public enum CardType
{
    None,
    Single,         //单张
    Double,         //对子
    DoubleMore,     //连对
    ThreeWithTwo,   //三带二
    Airplane,       //飞机
    Straight,       //顺子
    Boom,           //炸弹
}