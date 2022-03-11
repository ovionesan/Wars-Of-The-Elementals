using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class OnGameOpened : MonoBehaviour
{
    public VideoPlayer[] Videos;
    public UnityEngine.SpriteRenderer IntroSprite;
    public UnityEngine.SpriteRenderer MenuBG;
    public UnityEngine.SpriteRenderer MenuScroll;
    public UnityEngine.UI.Text IntroText;
    public Animator IntroAnimator;
    public float InSpeed = .001f;
    public float OutSpeed = .003f;
    public float FadeDelay = 5f;
    public bool skip = false;
    void Start()
    {
       // Videos[0].aspectRatio = new VideoAspectRatio();
        StartCoroutine(Sequence1());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            skip = true;
        }
    }
    IEnumerator Sequence1()
    {
        Color nc = IntroSprite.color;
        
        while (IntroSprite.color.a < 1)
        {
            yield return new WaitForSeconds(.005f);

            nc.a += InSpeed;
            IntroSprite.color = nc;
        }
        IntroText.enabled = false;
        yield return new WaitForSeconds(3.5f);
        IntroAnimator.speed = 0;
        //
        if(Videos[0] != null)
        {
            Videos[0].enabled = true;
            yield return new WaitForSeconds(.5f);
            while (Videos[0].isPlaying && skip == false)
            {
                yield return new WaitForSeconds(1f);
            }
            Videos[0].Stop();
            
        }
       
        IntroAnimator.speed = 1f;
        yield return new WaitForSeconds(FadeDelay);
        while (IntroSprite.color.a > 0)
        {
            yield return new WaitForSeconds(.005f);

            nc.a -= OutSpeed;
            IntroSprite.color = nc;
        }
        if (Videos[1] != null)
        {
            Videos[1].enabled = true;
            
        }
        Color ncbg = MenuBG.color;
        MenuScroll.gameObject.SetActive(true);
        while (MenuBG.color.a < 1 || MenuScroll.color.a < 1)
        {
            yield return new WaitForSeconds(.005f);

            ncbg.a += InSpeed;
            MenuScroll.color = ncbg;
            MenuBG.color = ncbg;
        }

    }
}
