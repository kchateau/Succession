using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealHand : MonoBehaviour{

    public List<Card> deck = new List<Card>();
    public GameObject myPrefab;
    Text date;

    void Start(){

        GenerateDeck();

        for (int i = 0; i < 3; i++) {
            int x = Random.Range(0, 850);

            var panel = GameObject.Find("Hand");

            if (panel != null) {
                GameObject a = (GameObject)Instantiate(myPrefab);
                a.transform.SetParent(panel.transform, false);
                Debug.Log(deck[x].name);
            }
        }     
    }

    private void GenerateDeck() {
        TextAsset gameData = Resources.Load<TextAsset>("game_data");

        string[] data = gameData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++) {

            string[] row = data[i].Split(new char[] { ',' });

            if (row[0] != "") {
                Card c = new Card();
                c.name = row[0];
                c.month = row[1];
                int.TryParse(row[2], out c.day);
                int.TryParse(row[3], out c.year);

                deck.Add(c);
            }
        }
    }

}
