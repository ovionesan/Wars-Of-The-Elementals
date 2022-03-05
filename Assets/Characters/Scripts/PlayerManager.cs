using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    // Start is called before the first frame update
	public int points;
	GameObject SecondPlayer;
	public Text TextP1;
	public Text TextP2;
    public int p1Score;
    public int p2Score;

    MoveController moveController;

    void Start(){
     	if(this.name == "Player One"){
    		SecondPlayer = GameObject.Find("Player 2");
		}
		if(this.name == "Player 2"){
    		SecondPlayer = GameObject.Find("Player One");
		}

    }

    public void updatePoints(int value){
    	points += value;

    	if(this.name == "Player One"){
			p1Score += value;
			TextP1.text = "Points: " + points.ToString();
		}

		if(this.name == "Player Two"){
			p2Score += value;
			TextP2.text = "Points: " + points.ToString();
		}
    }

    IEnumerator Haste(){
    	moveController.moveSpeed = moveController.moveSpeed * 4;
    	yield return new WaitForSeconds(10);
    	moveController.moveSpeed = moveController.moveSpeed / 4;
    }

    IEnumerator Slow(){
        /*
    	SecondPlayer.GetComponent<MoveController>().moveSpeed = SecondPlayer.GetComponent<MoveController>().moveSpeed / 4;
    	
    	SecondPlayer.GetComponent<MoveController>().moveSpeed = SecondPlayer.GetComponent<MoveController>().moveSpeed * 4;
        */
        yield return new WaitForSeconds(10);
    }

    IEnumerator DoubleScore(){
    	yield return new WaitForSeconds(5);
    	updatePoints(this.points * 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello!");
        // Handle Elements
        if (collision.gameObject.tag == "Air")
        {
            if (this.name == "Player One")
            {
                updatePoints(1);
            }
            if (this.name == "Player 2")
            {
                updatePoints(-1);
            }
        }

        if (collision.gameObject.tag == "Fire")
        {
            if (this.name == "Player One")
            {
                updatePoints(-1);
            }
            if (this.name == "Player 2")
            {
                updatePoints(1);
            }
        }

        if (collision.gameObject.tag == "Ice")
        {
            if (this.name == "Player One")
            {
                updatePoints(5);
            }
            if (this.name == "Player 2")
            {
                updatePoints(-5);
            }
        }

        if (collision.gameObject.tag == "Lava")
        {
            if (this.name == "Player One")
            {
                updatePoints(-5);
            }
            if (this.name == "Player 2")
            {
                updatePoints(5);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Game Over");
        }

        if (collision.gameObject.tag == "Haste")
        {
            StartCoroutine(Haste());
        }

        if (collision.gameObject.tag == "Lightning")
        {
            // take points
            SecondPlayer.GetComponent<PlayerManager>().updatePoints(-2);
        }

        if (collision.gameObject.tag == "Slow")
        {
            // slow down the 
            StartCoroutine(Slow());
        }

        if (collision.gameObject.tag == "Fire-Spell")
        {
            SecondPlayer.GetComponent<PlayerManager>().updatePoints(-2);
        }

        if (collision.gameObject.tag == "Flames")
        {
            // to implement
            StartCoroutine(DoubleScore());
        }

        if (collision.gameObject.tag == "Lava-Spell")
        {
            // slow down the 
            StartCoroutine(Slow());
        }
    }
}
