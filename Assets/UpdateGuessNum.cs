using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGuessNum : MonoBehaviour{

    public Text guess_num;

    void Start(){
        guess_num = GetComponent<Text>();
    }

    void Update(){
        guess_num.text = DealHand.Instance.guess_counter.ToString();;
    }
}
