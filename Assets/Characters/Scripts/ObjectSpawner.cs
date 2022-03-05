using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
	private string currentElement = "";
	private bool countdownPassed = true;

	public Sprite AirSprite;
	public Sprite FireSprite;
	public Sprite IceSprite;
	public Sprite LavaSprite;
	public Sprite NoneSprite;
	public SpriteRenderer spriteRenderer;

    private void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentElement == "" && countdownPassed ){
        	int ind = Random.Range (1, 100);

			if(ind >= 1 && ind <= 40){
				spriteRenderer.sprite = AirSprite; 
				gameObject.tag = "Air";
				currentElement = "Air";
			}

			if(ind >= 41 && ind <= 81){
				spriteRenderer.sprite = FireSprite; 
				gameObject.tag = "Fire";
				currentElement = "Fire";
			}

			if(ind >= 82 && ind <= 89){
				spriteRenderer.sprite = IceSprite;
				gameObject.tag = "Ice";
				currentElement = "Ice";
			}

			if(ind >= 90 && ind <= 100){
				spriteRenderer.sprite = LavaSprite; 
				gameObject.tag = "Lava";
				currentElement = "Lava";
			}
        }
    }

    IEnumerator Despawn(){
    	currentElement = "";
    	gameObject.tag = "Untagged";
    	spriteRenderer.sprite = NoneSprite;
    	countdownPassed = false;
    	yield return new WaitForSeconds(5);
    	countdownPassed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player"))
        {
			StartCoroutine(Despawn());
		}		
	}
}
