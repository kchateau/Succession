using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealHand : MonoBehaviour{

    public List<Card> deck = new List<Card>();
    public GameObject myPrefab;

    void Start(){

        GenerateDeck();

        //deal hand
        for (int i = 0; i < 5; i++) {
            int x = Random.Range(0, 850);
        //TODO make it so cards wont show up more than once
            var panel = GameObject.Find("Hand");

            if (panel != null) {
                GameObject card = (GameObject)Instantiate(myPrefab);

                card.transform.SetParent(panel.transform, false);

                //set game date on card
                Transform gameDateTransform = card.transform.Find("date");
                Text gameDateText = gameDateTransform.GetComponent<Text>();
                gameDateText.text = deck[x].date.ToString("MMM dd yyyy");

                //set game name on card
                Transform gameNameTransform = card.transform.Find("name");
                Text gameNameText = gameNameTransform.GetComponent<Text>();
                gameNameText.text = deck[x].name;

                Debug.Log(deck[x].name);

                gameDateText.enabled = false;
            }
        }

        //deal start drop area
        for (int i = 0; i < 1; i++) {
            int x = Random.Range(0, 850);
            //TODO make it so cards wont show up more than once
            var panel = GameObject.Find("Card drop area");

            if (panel != null) {
                GameObject card = (GameObject)Instantiate(myPrefab);

                card.transform.SetParent(panel.transform, false);

                //set game date on card
                Transform gameDateTransform = card.transform.Find("date");
                Text gameDateText = gameDateTransform.GetComponent<Text>();
                gameDateText.text = deck[x].date.ToString("MMM dd yyyy");

                //set game name on card
                Transform gameNameTransform = card.transform.Find("name");
                Text gameNameText = gameNameTransform.GetComponent<Text>();
                gameNameText.text = deck[x].name;

                Debug.Log(deck[x].name);

                gameDateText.enabled = true;
            }
        }

    }

    /*
     * Builds a List of card values
     */
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
                c.date = System.Convert.ToDateTime(c.month + " " + c.day + " " + c.year);

                deck.Add(c);
            }
        }
    }

}
