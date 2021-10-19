using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(LogicManager))]
public class LogicManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //显示默认所有参数

        LogicManager demo = (LogicManager)target;
        
        if (GUILayout.Button("开局"))
        {
            demo.StartGame();
        }
        else if (GUILayout.Button("洗牌"))
        {
            demo.Shuffle();
        }
        else if (GUILayout.Button("发牌"))
        {
            demo.Deal();
        }
        else if (GUILayout.Button("出牌"))
        {
            demo.Play();
        }
    }
}
#endif
public class LogicManager : MonoBehaviour
{
    public static LogicManager instance;

    public const int cardCount = 52; //一副牌54张，去除大小王
    public int nextTurn = 0; //流程控制，当前出牌玩家id

    BotDatabase botDatabase;
    [SerializeField, Range(2, 4)] int playerCount; //本局玩家数，开房间时确定
    [SerializeField] List<CardAttribute> libraryList = new List<CardAttribute>(); //牌库
    [SerializeField] List<CardAttribute> deskList = new List<CardAttribute>(); //桌上的牌
    [SerializeField] List<GamePlayer> playerList = new List<GamePlayer>(); //手牌

    void Awake()
    {
        instance = this;
        botDatabase = Resources.Load<BotDatabase>("BotDatabase");
    }

    // 创建房间，加入count个玩家
    public void CreateRoom(int count)
    {
        playerCount = count;

        ///TODO: 4位RoomID，由服务器分配
        int roomId = Random.Range(1000, 10000);

        ///TODO: 等待其他玩家加入
        
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
            int seed = Random.Range(0, startArray.Length - i); //从剩下的随机数里生成
            resultArray[i] = startArray[seed];//赋值给结果数组
            startArray[seed] = startArray[startArray.Length - i - 1]; //把随机数产生过的位置替换为未被选中的值。
        }

        // 创建当局玩家列表
        playerList.Clear();
        for (int i = 0; i < playerCount; i++)
        {
            GamePlayer player = new GamePlayer()
            {
                gameid = i,
                user_id = botDatabase.botList[resultArray[i]].user_id,
                avatar = (AvatarModel)resultArray[i],
                money = botDatabase.botList[resultArray[i]].money
            };

            //PlayerProvider scirpt = AvatarPool.instance.Spawn(player);
            //scirpt.mPlayer = player; //赋值
            //SpawnPoints.instance.SitDown(scirpt.transform); //入座

            playerList.Add(player);
        }
    }

    // 开局
    public void StartGame()
    {
        // roll一个数来确定庄家 ///TODO: 抢庄家
        int bankerid = UnityEngine.Random.Range(0, playerCount);
        Debug.Log(bankerid + " is lord");

        // 分配身份 //黑桃3先出
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].gameid == bankerid)
            {
                playerList[i].identity = Identity.LandLord;
            }
            else
            {
                playerList[i].identity = Identity.Farmer;
            }
        }
    }

    // 洗牌
    public void Shuffle()
    {
        // 重置
        libraryList.Clear();
        deskList.Clear();
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].handCardsList.Clear();
        }

        //哈希不重复的随机数
        System.Random id = new System.Random();
        Hashtable hashtable = new Hashtable();
        for (int i = 0; hashtable.Count < cardCount; i++) //取值范围1-52
        {
            int nValue = id.Next(cardCount + 1);
            if (!hashtable.ContainsValue(nValue) && nValue != 0)
            {
                hashtable.Add(nValue, nValue);
                CardAttribute card = new CardAttribute()
                {
                    cardid = nValue, //1-10 / 11->10 / 12->10 / 13->10
                };
                card.SerializeCard();
                libraryList.Add(card);
            }
        }
    }

    // 发牌，nextTurn升序一人一张轮着发
    public void Deal()
    {
        // 发五轮
        for (int k = 0; k < 5; k++)
        {
            // 共多少玩家
            for (int j = 0; j < playerCount; j++)
            {
                // 从牌库抽一张牌
                int index = Random.Range(1, libraryList.Count);
                Debug.Log("抽第" + index + "张，当前剩余" + libraryList.Count);

                CardAttribute card = libraryList[index];
                Debug.Log("[Player" + nextTurn + "]抽到 " + card.cardid);

                // 把牌放入玩家手中
                playerList[nextTurn].handCardsList.Add(card);

                // 把牌从牌库中移除
                libraryList.Remove(card);

                // 下一轮
                Debug.Log("----- 下一轮 -----");
                nextTurn++;
                if (nextTurn > playerCount - 1)
                {
                    nextTurn = 0;
                }
            }
        }

        ///TODO: 如果牌库不足10张，执行洗牌
    }

    // 出牌
    public void Play()
    {
        for (int i = 0; i < playerCount; i++)
        {
            int value = CardValue(i);
            Debug.Log("Player" + i + "：" + value);
        }
    }

    // 算分
    int CardValue(int _gameid)
    {
        // 手牌分两组，一组三张，一组两张
        // 三张这组加起来要是10/20/30
        for (int i = 0; i < playerList[_gameid].handCardsList.Count - 2; i++)
        {
            // C53=>10种可能性，找出是否有三张加起来为10/20/30
            for (int j = i + 1; j < playerList[_gameid].handCardsList.Count - 1; j++)
            {
                for (int k = j + 1; k < playerList[_gameid].handCardsList.Count; k++)
                {
                    int add = playerList[_gameid].handCardsList[i].value
                             + playerList[_gameid].handCardsList[j].value
                             + playerList[_gameid].handCardsList[k].value;

                    if(add % 10 == 0)
                    {
                        // 有牛，计算点数
                        int result = 0;
                        for (int x = 0; x < playerList[_gameid].handCardsList.Count; x++)
                        {
                            result += playerList[_gameid].handCardsList[x].value;
                        }
                        return result % 10;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        // 清空手牌，放桌面库
        for (int i = 0; i < playerList[_gameid].handCardsList.Count; i++)
        {
            deskList.Add(playerList[_gameid].handCardsList[i]);
        }
        //playerList[_gameid].handCardsList.Clear();

        return -1;
    }
}

// 牌面属性
[System.Serializable]
public class CardAttribute
{
    public int cardid; //0~52序号（大小王去掉）
    public int value; //JQK对应的值
	public Colors colors; //花色
    public Weight weight; //牌面点数

    // 根据id，序列化牌面属性
    public void SerializeCard()
    {
        int value = 0;
        if (this.cardid <= 13)
        {
            this.colors = Colors.Spade;
            this.weight = (Weight)(this.cardid % 13); //0-12 切分//1-10/0,11,12

            value = this.cardid % 13;
            this.value = value;
            if (value == 0 || value == 11 || value == 12)
            {
                this.value = 10;
            }
        }
        else if (this.cardid > 13 && this.cardid <= 26)
        {
            this.colors = Colors.Heart;
            this.weight = (Weight)(this.cardid % 13);

            value = this.cardid % 13;
            this.value = value;
            if (value == 0 || value == 11 || value == 12)
            {
                this.value = 10;
            }
        }
        else if (this.cardid > 26 && this.cardid <= 39)
        {
            this.colors = Colors.Club;
            this.weight = (Weight)(this.cardid % 13);

            value = this.cardid % 13;
            this.value = value;
            if (value == 0 || value == 11 || value == 12)
            {
                this.value = 10;
            }
        }
        else if (this.cardid > 39 && this.cardid <= 52)
        {
            this.colors = Colors.Square;
            this.weight = (Weight)(this.cardid % 13);

            value = this.cardid % 13;
            this.value = value;
            if (value == 0 || value == 11 || value == 12)
            {
                this.value = 10;
            }
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
        }
        */
    }
}

// 牌局玩家属性
[System.Serializable]
public class GamePlayer : Player
{
    public int gameid; //这盘中的顺位
    public Identity identity; //身份庄/闲
    public List<CardAttribute> handCardsList = new List<CardAttribute>(); //手牌List
}