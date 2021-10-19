using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;

    public const int CARD_COUNT = 52; //一副牌54张，去除大小王，三张2，一张A。每人16张。
    //public int nextTurn = 0; //流程控制，当前出牌玩家id

    BotDatabase botDatabase;
    [SerializeField, Range(2, 4)] int playerCount = 3; //本局玩家数，开房间时确定
    [SerializeField] List<GamePlayer> playerList = new List<GamePlayer>(); //玩家
    [SerializeField] List<CardAttribute> libraryList = new List<CardAttribute>(); //牌库
    private int rollIndex;

    void Awake()
    {
        Instance = this;
        botDatabase = Resources.Load<BotDatabase>("BotDatabase");
    }

    // 创建房间
    public void CreateRoom()
    {
        //TODO: 4位RoomID，由服务器分配
        //int roomId = Random.Range(1000, 10000);

        //TODO: 等待其他玩家加入
        
        // 从总玩家数max中，随机取count个数
        int max = System.Enum.GetNames(typeof(AvatarModel)).GetLength(0);
        int[] startArray = new int[max];
        int[] resultArray = new int[playerCount]; //结果存放在里面
        for (int i = 0; i < max; i++)
        {
            startArray[i] = i;
        }
        for (int i = 0; i < playerCount; i++)
        {
            Random rd = new Random();
            //int seed = Random.Range(0, startArray.Length - i);
            int seed = rd.Next(startArray.Length - i); //从剩下的随机数里生成
            resultArray[i] = startArray[seed];//赋值给结果数组
            startArray[seed] = startArray[startArray.Length - i - 1]; //把随机数产生过的位置替换为未被选中的值。
        }

        // 创建当局玩家列表
        playerList.Clear();
        for (int i = 0; i < playerCount; i++)
        {
            GamePlayer player = new GamePlayer()
            {
                SeatID = i,
                user_id = botDatabase.botList[resultArray[i]].user_id,
                avatar = (AvatarModel)resultArray[i],
                money = botDatabase.botList[resultArray[i]].money
            };

            playerList.Add(player);
            Debug.Log($"Add Player: {i}");
        }
    }

    // 洗牌
    public void Shuffle()
    {
        // 重置
        libraryList.Clear();
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].HandCards.Clear();
        }

        // 哈希不重复的随机数
        Random rd = new Random();
        Hashtable hashtable = new Hashtable();
        for (int i = 0; hashtable.Count < CARD_COUNT; i++) //取值范围1-52
        {
            int nValue = rd.Next(CARD_COUNT + 1);
            if (!hashtable.ContainsValue(nValue) && nValue != 0)
            {
                hashtable.Add(nValue, nValue);
                var card = new CardAttribute
                {
                    cardid = nValue,
                };
                card.SerializeCard();
                libraryList.Add(card);
            }
        }

        // 去掉三张2，一张A。
        Debug.Log($"去掉前，牌库：{libraryList.Count}"); //52
        int TwoCount = 3;
        int OneCount = 1;
        for (int i = 0; i < libraryList.Count; i++)
        {
            var card = libraryList[i];
            if (card.weight == Weight.Two && TwoCount > 0)
            {
                TwoCount--;
                libraryList.Remove(card);
                Debug.Log($"<color=red>去掉：{card.ToString()}</color>");
            }
            if (card.weight == Weight.One && OneCount > 0)
            {
                OneCount--;
                libraryList.Remove(card);
                Debug.Log($"<color=red>去掉：{card.ToString()}</color>");
            }
        }
        Debug.Log($"去掉后，牌库：{libraryList.Count}"); //48，cardid是不连续的
    }

    // 抽牌
    public void Roll()
    {
        // 48张中，roll一张明牌
        Random rd = new Random();
        rollIndex = rd.Next(libraryList.Count); //左闭右开[,)

        var rollCard = libraryList[rollIndex];
        Debug.Log($"<color=yellow>Roll出来的明牌：第{rollIndex}张，{rollCard.ToString()}</color>");
    }

    // 发牌
    public void Deal()
    {
        int seatIndex = 0;
        for (int i = libraryList.Count - 1; i >= 0; i--)
        {
            var card = libraryList[i];
            if (i == rollIndex)
            {
                // 得到明牌的玩家先出
                Debug.Log($"<color=yellow>玩家{seatIndex}拥有明牌{card.ToString()}，先出</color>");
            }

            // 把牌放入玩家手中
            playerList[seatIndex].HandCards.Add(card);

            // 把牌从牌库中移除
            libraryList.Remove(card);

            seatIndex++;
            if (seatIndex > playerCount - 1)
            {
                seatIndex = 0;
            }

            Debug.Log($"[{i}轮]，发给玩家{seatIndex}，{card.ToString()}");
        }
    }

    // 手牌排序
    public void SortHandCard()
    {

    }

    // 手牌显示
    public void ShowHandCard()
    {

    }
}

// 牌面属性
[System.Serializable]
public class CardAttribute
{
    public int cardid; //0~52序号（大小王去掉）
    //public int value; //JQK对应的值11，12，13
	public Colors colors; //花色
    public Weight weight; //牌面点数

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
        /*
        else if (this.cardid == 53) //53,54
        {
            this.colors = Colors.King;
            this.weight = Weight.SJoker;
            this.value = 10;
        }
        else if (this.cardid == 54) //53,54
        {
            this.colors = Colors.King;
            this.weight = Weight.BJoker;
            this.value = 10;
        }*/
    }
    public override string ToString()
    {
        return $"{colors}_{weight}:{cardid}";
    }
}

// 牌局玩家属性
[System.Serializable]
public class GamePlayer : DBPlayer
{
    public int SeatID; //这盘中的顺位
    public List<CardAttribute> HandCards = new List<CardAttribute>(); //手牌List
}