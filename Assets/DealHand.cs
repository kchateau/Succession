using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealHand : MonoBehaviour{

    public static DealHand Instance;
    public List<Card> deck = new List<Card>();
    public GameObject myPrefab;

    void Awake(){
        Instance = this;
    }

    void Start(){

        GenerateDeck();

        //deal hand
        for (int i = 0; i < 5; i++) {
            dealCard("Hand");
        }

        //deal start drop area
        for (int i = 0; i < 1; i++) {
            dealCard("Card drop area");
        }
    }

    public static void dealCard(string parent) {
        int x = Random.Range(0, 850);

        var panel = GameObject.Find(parent);

        if (panel != null) {
            GameObject card = (GameObject)Instantiate(DealHand.Instance.myPrefab);

            card.transform.SetParent(panel.transform, false);

            //set game date on card
            Transform gameDateTransform = card.transform.Find("date");
            Text gameDateText = gameDateTransform.GetComponent<Text>();
            gameDateText.text = DealHand.Instance.deck[x].date.ToString("MMM dd yyyy");

            //set game name on card
            Transform gameNameTransform = card.transform.Find("name");
            Text gameNameText = gameNameTransform.GetComponent<Text>();
            gameNameText.text = DealHand.Instance.deck[x].name;

            if (parent == "Hand") {
                gameDateText.enabled = false;
            }
            else {
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
