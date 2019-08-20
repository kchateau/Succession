using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDeck : MonoBehaviour {

    List<Card> deck = new List<Card>();

    void Start() {
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
