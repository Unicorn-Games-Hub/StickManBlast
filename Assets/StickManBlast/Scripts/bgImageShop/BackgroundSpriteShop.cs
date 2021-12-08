using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSpriteShop : MonoBehaviour
{
    public Transform _bgShopButtonContainer;
    private int _bgNumCount=0;
    public Sprite _lockIconSprite;
    public GameObject _purchaseErrorText;
    private bool _canAnimate=true;

    //dynamically assigning the sprites of background to the bg choose button
    public Sprite[] _bgSprites;
    public string[] _lockedMessage;

    [Header("Sound Effects")]
    private AudioSource _aud;
    public AudioClip[] _bgBtnSound;

    void Start()
    {   
        //deactivating the error text at the start of the game
        _purchaseErrorText.SetActive(false);
        _aud=GetComponent<AudioSource>();

        //for unlocking one background at the start of the game
        if(PlayerPrefs.GetInt("bgImageUnlocked"+0)==0)
        {
            PlayerPrefs.SetInt("bgImageUnlocked"+0,1);
        }
        foreach (Transform t in _bgShopButtonContainer)
        {
            t.GetChild(0).gameObject.GetComponent<BgButton>()._bgID=_bgNumCount;
            t.GetChild(0).gameObject.GetComponent<Image>().sprite=_bgSprites[_bgNumCount];
            t.GetChild(1).gameObject.SetActive(false);
             t.GetChild(2).GetChild(0).gameObject.GetComponent<Image>().sprite=_lockIconSprite;
             
            if(PlayerPrefs.GetInt("bgImageUnlocked"+_bgNumCount)==1)
            {
                t.GetChild(0).gameObject.GetComponent<Button>().interactable=true;
                t.GetChild(2).gameObject.SetActive(false);
                if(PlayerPrefs.GetInt("CurrentBGsprite")==_bgNumCount)
                {
                     t.GetChild(1).gameObject.GetComponent<Text>().text="Selected";
                     t.GetChild(2).gameObject.SetActive(false);
                    t.GetChild(1).gameObject.SetActive(true);
                }
            }
            else
            {
                t.GetChild(0).gameObject.GetComponent<Button>().interactable=false;
            }
            

            if(PlayerPrefs.GetInt("bgImageUnlocked"+_bgNumCount)==1)
            {
                //Debug.Log("Unlocked bg is : "+_bgNumCount);
            }

            t.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(delegate()
            {
                ChooseTheSelectedBackground(t.GetChild(0).gameObject.GetComponent<BgButton>()._bgID);
            });

            t.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate(){
              ShowPurchaseError(t.GetChild(0).gameObject.GetComponent<BgButton>()._bgID);
            });

            _bgNumCount++;
        }
    }
    
    public void ChooseTheSelectedBackground(int _id)
    {
        if(PlayerPrefs.GetInt("bgImageUnlocked"+_id)==1)
        {
            DisablingTheSelectedText(_id);
            PlayerPrefs.SetInt("CurrentBGsprite",_id);
           if(MenuController._instance!=null)
           {
              MenuController._instance.ChangeBackgroundImage();
           }
            PlaySelectedSound();
        }
    }

    void DisablingTheSelectedText(int _itemToActivate)
    {
        for(int i=0;i<_bgShopButtonContainer.childCount;i++)
        {
            _bgShopButtonContainer.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
        _bgShopButtonContainer.GetChild(_itemToActivate).transform.GetChild(1).gameObject.SetActive(true);
          _bgShopButtonContainer.GetChild(_itemToActivate).transform.GetChild(1).GetComponent<Text>().text="Selected";

    }

    void ShowPurchaseError(int _bgNum)
    {
        if(_canAnimate)
        {
            _purchaseErrorText.GetComponent<Text>().text=_lockedMessage[_bgNum];
            PlayLockedSound();
            StartCoroutine(TimeForErrorTextAnimation());
        }
    }

    IEnumerator TimeForErrorTextAnimation()
    {
        _canAnimate=false;
        _purchaseErrorText.SetActive(true);
        yield return new WaitForSeconds(2f);
        _purchaseErrorText.SetActive(false);
        _canAnimate=true;
    }
    
     private void PlaySelectedSound()
    {
         if(PlayerPrefs.GetInt("GameAudio")==0)
        {
            _aud.clip=_bgBtnSound[0];
            _aud.Play();
        }
    }

    private void PlayLockedSound()
    {
        if(PlayerPrefs.GetInt("GameAudio")==0)
        {
            _aud.clip=_bgBtnSound[1];
            _aud.Play();
        }
    }
}
