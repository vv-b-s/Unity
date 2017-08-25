using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite DmgSprite;
    public int Hp = 4;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamaageWall (int loss)
    {
        spriteRenderer.sprite = DmgSprite;
        Hp -= loss;
        if (Hp <= 0)
            gameObject.SetActive(false);
    }
}
