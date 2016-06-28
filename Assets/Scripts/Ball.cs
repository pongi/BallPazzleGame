using UnityEngine;
using System.Collections;

public class Ball : Token
{

    static string[] spriteName = {
        "ball_blue",
        "ball_green",
        "ball_orange",
        "ball_purple",
        "ball_red",
    };

    /// プレハブ
    static GameObject _prefab = null;

    public static Ball newInstance(float x, float y)
    {
        // プレハブを取得
        _prefab = GetPrefab(_prefab, "Ball");
        // プレハブからインスタンスを生成
        Ball obj = CreateInstance2<Ball>(_prefab, x, y);
        int id = Random.Range(0, spriteName.Length);
        string filename = "Sprites/" + spriteName[id];
        obj.SetSprite(Util.GetSprite(filename, ""));
        obj.name = "" + filename;
        return obj;
    }

    public void Destroy()
    {
        // パーティクルを生成
        for (int i = 0; i < 32; i++)
        {
            Particle.Add(X, Y);
        }

        // 破棄する
        Vanish();
        DestroyObj();
        
    }


    // Use this for initialization
    void Start()
    {
        Revive();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver()
    {
    }
}
