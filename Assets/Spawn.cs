using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour 
{
	//哈希不重复的随机数
	Hashtable hashtable = new Hashtable();
	System.Random id = new System.Random();

	public const int cardCount = 54; //一副牌54张
	public int playerCount = 2; //本局玩家数
	public int nextTurn = 0; //当前出牌玩家id
	public List<CardAttribute> libList = new List<CardAttribute>(); //牌库
	public List<CardAttribute> deskList = new List<CardAttribute>(); //桌上的牌
	public List<Player> playerList = new List<Player> (); //手牌

	void Start()
	{
		//庄/闲身份
		int bankerid = UnityEngine.Random.Range(0, playerCount);

		//加入玩家
		for(int i = 0; i<playerCount; i++)
		{
			Player player = new Player ();
			player.gameid = i;
			player.username = "";
			player.identity = i == bankerid ? Identity.LandLord : Identity.Farmer;
			playerList.Add (player);
		}

		//洗牌
		Shuffle (); //TODO...和发牌合并

		//发牌

	}

	/// <summary>
	/// 洗牌
	/// </summary>
	void Shuffle ()
	{
		for (int i = 0; hashtable.Count < cardCount; i++) //取值范围1-54
		{
			int nValue = id.Next(cardCount + 1); 
			if (!hashtable.ContainsValue(nValue) && nValue != 0) 
			{ 
				hashtable.Add(nValue, nValue);
				//Debug.Log(nValue.ToString());

				CardAttribute card = new CardAttribute();
				card.cardid = nValue; //1-10 / 11->10 / 12->10 / 13->10
				SerializeCard(card);

				libList.Add (card);
			} 
		}
	}

	/// <summary>
	/// 发牌，轮着发，每轮1人5张
	/// </summary>
	[ContextMenu("Deal")]
	void Deal()
	{
		for (int a = 0; a < playerCount * 5; a++) 
		{
			CardAttribute card = new CardAttribute();
			card.cardid = Random.Range (1, libList.Count + 1); //包前不包后
			SerializeCard(card);

			//把牌放入玩家手中
			playerList [nextTurn].handCardsList.Add (card);

			//把牌从牌库中移除
			for (int b = 0; b < libList.Count; b++)
			{
				if (libList [b].cardid == card.cardid) 
				{
					libList.RemoveAt (b);
				}
			}

			nextTurn++;
			if (nextTurn > playerCount - 1) 
			{
				nextTurn = 0;
			}
		}
	}

	/// <summary>
	/// 计算牌面值
	/// </summary>
	int CardValue(int _gameid)
	{
		//每三张相加。即随机去掉2张，剩下3张，并不重复的可能性。10种
		//去掉1，再去掉2/3/4/5
		//去掉2，再去掉3/4/5
		//去掉3，再去掉4/5
		//去掉4，再去掉5
		//去掉5，null
		int id0 = Random.Range (1, 5); //1,2,3,4
		int id1 = Random.Range (id0 + 1, 6); //包前不包后

		int ten = 0;
		for (int a = 0; a < 5; a++) 
		{
			if (id0 == a || id1 == a) 
			{
				continue;
			}
			else 
			{
				ten += playerList [_gameid].handCardsList [a].value;
			}
		}
		Debug.Log ("[gameid]" + _gameid + "\n[id1]" + id0 + "[id1]" + id1 + "\n");
		return ten;
	}

	[ContextMenu("GetValue")]
	void GetValue()
	{
		for (int i = 0; i < playerCount; i++) 
		{
			CardValue(i);
			//Debug.Log ("[player]" + CardValue(i));
		}
	}

	/// <summary>
	/// 比大小点数
	/// </summary>
	/// <param name="_card">Card.</param>
	public static void SerializeCard(CardAttribute _card) //在cardid随机出来之后执行
	{
		int value;
		if (_card.cardid <= 13)
		{
			_card.colors = Colors.Spade;
			_card.weight = (Weight)(_card.cardid % 13); //0-12 切分//1-10/0,11,12

			value = _card.cardid % 13;
			_card.value = value;
			if (value == 0||value == 11||value == 12) 
			{
				_card.value = 10;
			}
		} 
		else if (_card.cardid > 13 && _card.cardid <= 26) 
		{
			_card.colors = Colors.Heart;
			_card.weight = (Weight)(_card.cardid % 13);

			value = _card.cardid % 13;
			_card.value = value;
			if (value == 0||value == 11||value == 12) 
			{
				_card.value = 10;
			}
		} 
		else if (_card.cardid > 26 && _card.cardid <= 39) 
		{
			_card.colors = Colors.Club;
			_card.weight = (Weight)(_card.cardid % 13);

			value = _card.cardid % 13;
			_card.value = value;
			if (value == 0||value == 11||value == 12) 
			{
				_card.value = 10;
			}
		}
		else if (_card.cardid > 39 && _card.cardid <= 52) 
		{
			_card.colors = Colors.Square;
			_card.weight = (Weight)(_card.cardid % 13);

			value = _card.cardid % 13;
			_card.value = value;
			if (value == 0||value == 11||value == 12) 
			{
				_card.value = 10;
			}
		}
		else if (_card.cardid == 53) //53,54
		{
			_card.colors = Colors.King;
			_card.weight = Weight.SJoker;
			_card.value = 10;
		}
		else if (_card.cardid == 54) //53,54
		{
			_card.colors = Colors.King;
			_card.weight = Weight.BJoker;
			_card.value = 10;
		}
	}
}

[System.Serializable]
public class CardAttribute
{
	public int cardid;
	public int value; //TODO?...三个属性全部根据cardid {get;set;}
	public Colors colors; //
	public Weight weight; //
}

[System.Serializable]
public class Player
{
	public int gameid; //这盘中的顺位
	public string username; //注册的用户名
	public Identity identity;
	public List<CardAttribute> handCardsList = new List<CardAttribute>(); //手牌List
}
