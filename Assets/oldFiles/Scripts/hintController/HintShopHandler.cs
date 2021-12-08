using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintShopHandler : MonoBehaviour
{
	[System.Serializable]
	public class _hintsInfo
	{
		public string _name;
		public int _noOfHints;
		public int _price;
	}

	public List<_hintsInfo> _hintShop = new List<_hintsInfo> ();
	private int _shopCounter=0;
	public Transform _hintShopContainer;


	private int _avaliableCoins;

	// Use this for initialization
	void Start () 
	{
		foreach (Transform _shop in _hintShopContainer) 
		{
			_shop.GetComponent<HintShop> ()._noOfhints = _hintShop [_shopCounter]._noOfHints;
			_shop.GetComponent<HintShop> ()._priceOfHints = _hintShop [_shopCounter]._price;
			_shop.GetChild (1).GetComponent<Text> ().text = _hintShop [_shopCounter]._noOfHints.ToString ();
			_shop.GetChild (2).GetChild (0).GetComponent<Text> ().text = _hintShop [_shopCounter]._price.ToString ();
			_shop.GetChild (2).GetComponent<Button> ().onClick.AddListener (delegate() {
				BuyHints(_shop.GetComponent<HintShop> ()._noOfhints, _shop.GetComponent<HintShop> ()._priceOfHints);});
			_shopCounter++;
		}
	}
		
	void BuyHints(int _hintAmt,int _hintCost)
	{
		_avaliableCoins = PlayerPrefs.GetInt ("TotalCoinsInGame");
		if (_avaliableCoins >= _hintCost) 
		{
			PlayerPrefs.SetInt ("TotalHints", PlayerPrefs.GetInt ("TotalHints") + _hintAmt);
		}
	}
}
