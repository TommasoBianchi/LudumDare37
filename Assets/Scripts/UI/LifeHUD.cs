using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour {

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite bonusHeart;

    public RectTransform lifePanel;

    public GameObject heartPrefab;

    private List<Image> hearts = new List<Image>();

    public void SetLife(int amount, int maxLife)
    {
        for (int i = 0; i < maxLife; i++)
        {
            if (i < hearts.Count)
            {
                hearts[i].enabled = true;           
            }
            else
            {
                hearts.Add(Instantiate(heartPrefab).GetComponent<Image>());
                hearts[i].transform.SetParent(lifePanel);
                hearts[i].transform.localPosition = new Vector3(10 + i * 110, -5, 0);
                lifePanel.sizeDelta += new Vector2(110, 0);
            }
            hearts[i].sprite = (i < amount) ? fullHeart : emptyHeart;
        }

        for (int i = maxLife; amount - i > 0; i++)
        {
            if (i < hearts.Count)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts.Add(Instantiate(heartPrefab).GetComponent<Image>());
                hearts[i].transform.SetParent(lifePanel);
            }
            hearts[i].sprite = bonusHeart;
        }
    }
}
