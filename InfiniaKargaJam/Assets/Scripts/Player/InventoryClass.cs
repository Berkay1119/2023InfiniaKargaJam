using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryClass: MonoBehaviour
{
    [SerializeField] private List<Image> Images = new List<Image>();
    [SerializeField] private int playerNumber=1;
    private void Update()
    {
        if (playerNumber==1)
        {
            List<Ability> abilities = GameManager.Instance.player1.abilities.ToList();
            if (GameManager.Instance.player1==null)
            {
                return;
            }
            for (int i = 0; i < Images.Count; i++)
            {
                if (abilities[i]==null)
                {
                    Images[i].sprite = null;
                    Images[i].gameObject.SetActive(Images[i].sprite!=null);
                    continue;
                }
                Images[i].sprite = abilities[i].sprite;
                Images[i].gameObject.SetActive(Images[i].sprite!=null);

            }
        }
        else if (playerNumber==2)
        {
            List<Ability> abilities = GameManager.Instance.player2.abilities.ToList();
            if (GameManager.Instance.player2==null)
            {
                return;
            }
            for (int i = 0; i < Images.Count; i++)
            {
                if (abilities[i]==null)
                {
                    Images[i].sprite = null;
                    Images[i].gameObject.SetActive(Images[i].sprite!=null);
                    continue;
                }
                Images[i].sprite = abilities[i].sprite;
                Images[i].gameObject.SetActive(Images[i].sprite!=null);
            }
        }
    }
}
