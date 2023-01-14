using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using CustomExtensions;

public class GameBehaviour : MonoBehaviour, IManager
{
    public string labelText = "4つのアイテムを集めて自由を勝ち取ろう！";
    public const int maxItems = 4;
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public Stack<string> lootStack = new Stack<string>();
    public void Initialize()
    {
        _state = "Managerの初期化を終えました...";
        _state.FancyDebug();
        debug(_state);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");

        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
        playerBehaviour.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("プレイヤーがジャンプした...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("デバッグ出力を委任する...");
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("{0} をゲットした！次にみつかるのは、きっと {1} だ！", currentItem, nextItem);

        Debug.LogFormat("お宝が {0} つ、きみを待っているぞ！", lootStack.Count);
    }

    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }

        set {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            if(_itemsCollected >= maxItems)
            {
                labelText = "アイテムを全部みつけたね！";
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                labelText = "アイテムをみつけたね。あと " + (maxItems - _itemsCollected) + " つだよ！";
            }
        }
    }

    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set {
            _playerHP = value;

            if(_playerHP <= 0)
            {
                labelText = "もうひとつのライフが欲しい？";
                showLossScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "いてて...やられたぜ。";
            }
            // Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "プレイヤーのHP: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "あつめたアイテム: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if(showWinScreen)
        {
            if(GUI.Button(
                new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100),
                "きみの勝ちだ！"
            ))
            {
                Utilities.RestartLevel(0);
            }
        }

        if(showLossScreen)
        {
            if(GUI.Button(
                new Rect(Screen.width / 2 - 100,
                Screen.height / 2 -50, 200, 100), "きみの負けだ..."
            ))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("レベルの再開に成功...");
                }
                catch(System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("シーンを0に戻す: " + e.ToString());
                }
                finally
                {
                    debug("リスタートを処理した...");
                }

            }
        }
    }
}
