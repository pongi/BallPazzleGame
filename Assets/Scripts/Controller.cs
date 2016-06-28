using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public static List<Ball> balls = new List<Ball>();
    static public bool mousdown;

    static uint NUM_OF_BALL = 80;

    // Use this for initialization
    void Start()
    {
    }

    List<Ball> removableList = new List<Ball>();
    string currentColor = "";
    Ball lastBall;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && removableList.Count == 0)
        {
            //ボールをドラッグしはじめたとき
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //ボールをドラッグし終わったとき
            OnDragEnd();
        }
        else if (removableList.Count != 0)
        {
            //ボールをドラッグしている途中
            OnDragging();
        }
        else
        {
            AddRemainsBall();
        }

    }

    Ball checkBall(GameObject obj)
    {
        foreach (Ball ball in balls)
        {
            if (ball.gameObject.Equals(obj))
            {
                return ball;
            }

        }
        return null;
    }

    void OnDragStart()
    {
        Collider2D col = GetCurrentHitCollider();
        if(col != null )
        {
            Ball ball = checkBall(col.gameObject);
            if(ball != null)
            {
                currentColor = ball.name;
                lastBall = ball;
                removableList.Add(ball);
                ChangeColor(ball.gameObject, 0.5f);
            }
        }
    }

    void OnDragEnd()
    {
        int length = removableList.Count;
        if(length >= 3)
        {
            foreach(Ball ball in removableList)
            {
                RemoveBall(ball);
            }
        }
        currentColor = "";
        lastBall = null;
        foreach(Ball ball in removableList)
            ChangeColor(ball.gameObject, 1.0f);

        removableList.Clear();
    }

    void OnDragging()
    {
        Collider2D col = GetCurrentHitCollider();
        if (col == null)
            return;

        Ball ball = checkBall(col.gameObject);
        var dist = Vector2.Distance(lastBall.transform.position, ball.transform.position); //直前のボールと現在のボールの距離を計算
        Debug.Log("dist: " + dist);
        if (dist > 0.5)
            return;

        if (!currentColor.Equals(ball.name))
            return;

        if (!removableList.Contains(ball))
        {
            lastBall = ball;
            removableList.Add(ball);
            ChangeColor(ball.gameObject, 0.5f);
        }
    }
    void ChangeColor(GameObject obj, float alpha)
    {
        SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>(); //ボールの画像を管理している要素を取得
        ballTexture.color = new Color(1, 1, 1, alpha); ; //透明度を設定
    }

    public static void AddBall()
    {
        float posx = Random.Range(-1.3f, 1.3f);
        float posy = Random.Range(2.0f, 5.0f);
        balls.Add(Ball.newInstance(posx, posy));
    }

    public static bool RemoveBall(Ball ball)
    {
        ball.Destroy();
        return balls.Remove(ball);
    }

    public static void AddRemainsBall()
    {
        for (int i = balls.Count; i < NUM_OF_BALL; i++)
        {
            AddBall();
        }
    }



    Collider2D GetCurrentHitCollider()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider;
    }
}
