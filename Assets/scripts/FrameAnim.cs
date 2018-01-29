using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FrameAnim : MonoBehaviour {

    public Sprite[] sprites;

    public float[] deltaTimes;

    public float defaultDeltaTime = 150f; //ms

    public bool playOnAwake;

    public bool playOnClick;

    public bool playOnHover;

    public bool repeat = true;

    public bool keepLastFrame;

    public bool animEnabled = true;

    public bool playSoundWithAnim = true;

    Image img;

    SpriteRenderer spriteRenderer;

    AudioSource sound;

    public void PlayAnim()
    {
        if (animEnabled)
        {
            StartCoroutine("DoAnim");
            if( sound && playSoundWithAnim)
            {
                sound.Play();
            }
        }
    }

    public void StopAnim()
    {
        StopAllCoroutines();
        if (animEnabled)
        {
            SetSprite( sprites[0] );
            if (sound && playSoundWithAnim)
            {
                sound.Stop();
            }
        }
    }

	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponentInChildren<AudioSource>();
        img = gameObject.GetComponentInChildren<Image>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        if ( playOnAwake)
        {
            PlayAnim();
        }

        if( playOnHover)
        {
            var trigger = gameObject.GetComponent<EventTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<EventTrigger>();
            }
            //HOVER BEGIN
            var entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerEnter;
            entry1.callback.AddListener((data) => { PlayAnim(); });
            trigger.triggers.Add(entry1);
            //HOVER END
            var entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((data) => { StopAnim(); });
            trigger.triggers.Add(entry2);
        }

        if( playOnClick)
        {
            var trigger = gameObject.GetComponent<EventTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<EventTrigger>();
            }
            //HOVER BEGIN
            var entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerClick;
            entry3.callback.AddListener((data) => { PlayAnim(); });
            trigger.triggers.Add(entry3);
        }
	}

    IEnumerator DoAnim()
    {
        while (true)
        {
            for (var i = 0; i < sprites.Length; i++)
            {
                SetSprite( sprites[i] );
                var specificDt = deltaTimes.Length > i ? deltaTimes[i] : -1;                
                var dt = specificDt == -1 ? defaultDeltaTime : specificDt;
                yield return new WaitForSeconds(dt / 1000);
            }
            if( !repeat)
            {
                break;
            }
        }
        if (!keepLastFrame)
        {
            SetSprite(sprites[0]);
        }
        yield return null;
    }

    void SetSprite(Sprite s)
    {
        if(img != null)
        {
            img.sprite = s;
        }else if( spriteRenderer != null )
        {
            spriteRenderer.sprite = s;
        }
    }
}
