// Author : Renaud THIERRY
// Created : 28-01-2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

	// Singleton
	// 

	static private SoundManager instance;
	static public SoundManager Instance{
		get{ return SoundManager.instance;}

	}

    // Properties
    //
    
    public bool SoundOn = true;
    public bool MusicOn = true;
    private bool musicPlaying = false;

    [Header("Mixer")]
    public AudioMixer MixerMaster;

    //
    [Header("Sound Lists")]
    public List<AudioClip> ClickMenu = new List<AudioClip>();
    public List<AudioClip> PopupOpen = new List<AudioClip>();
    public List<AudioClip> PopupClose = new List<AudioClip>();

	public List<AudioClip> PowerFire = new List<AudioClip>();
	public List<AudioClip> PowerLightning = new List<AudioClip>();
	public List<AudioClip> PowerBoost = new List<AudioClip>();
	public List<AudioClip> PowerHeal = new List<AudioClip>();

	public List<AudioClip> VillagerScream = new List<AudioClip>();

    [Header("Jingles")]
    public List<AudioClip> WinGame = new List<AudioClip>();

    [Tooltip("The music will lower to this volume while a jingle is playing")]
    [Range(0f, 1f)]
    public float MusicVolumeWhileJinglePlay;

    [Header("Music")]
    public AudioClip MenuMusic;
    public AudioClip InGameMusic;


    // AudioSource
    [Header("Audio Source")]
    public AudioSource PlayerSource;
    public AudioSource VillagerSource;
    public AudioSource WorldSource;
    public AudioSource JingleSource;

    public AudioSource MusicSource;


    private float defaultMusicVolume;
    private AudioListener audioListener;

    private AudioClip nextMusicToPlay;

    private bool isInit = false;

    public enum FadeState { none, FadeIn, FadeOut };
    private FadeState currentFadeState = FadeState.none;

    private float currentFadeTime = 0f;
    private float fadeTime = 1;

    private float hintPopTimer = -1f;

    // Methods
    //
    
    void Awake()
    {
		// Singleton logic
		if(SoundManager.instance == null){

			SoundManager.instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);

		}
		else{
			GameObject.Destroy(this.gameObject);
			return;
		}

        SoundManager.instance = this;

        this.InitSoundManager();

    }

    // Use this for initialization
    public void InitSoundManager()
    {

        // If the sound manager hasn't been init yet
        if (!this.isInit)
        {

            this.searchAudioListener();

            // Add the Audio source to this god object

            this.PlayerSource = this.gameObject.AddComponent<AudioSource>();
            this.WorldSource = this.gameObject.AddComponent<AudioSource>();
            this.VillagerSource = this.gameObject.AddComponent<AudioSource>();
            this.JingleSource = this.gameObject.AddComponent<AudioSource>();

            this.MusicSource = this.gameObject.AddComponent<AudioSource>();

            this.MusicSource.loop = true;

            this.defaultMusicVolume = 1f;


            // Init is done
            this.isInit = true;

        }

		// Start to play the main theme
		this.PlayInGameMusic();

    }

    private void searchAudioListener()
    {

        AudioListener camAL = Camera.main.GetComponent<AudioListener>();

        // If we found an AudioListener
        if (camAL != null)
        {
            this.audioListener = camAL;
        }
        // Search in the wole scene, more costly
        else {
            this.audioListener = GameObject.FindObjectOfType<AudioListener>();

        }
    }

    public void Update()
    {

        // If the music is playing
        if (this.MusicOn)
        {

            // Re up the volume if a jingle sound stopped playing or an ad is finished
            if (this.musicPlaying && this.MusicSource.volume != this.defaultMusicVolume && !this.JingleSource.isPlaying)
            {

                this.MusicSource.volume = this.defaultMusicVolume;

            }
        }

        // Music Fade In/Out
        if (this.currentFadeState != FadeState.none)
        {

            this.currentFadeTime += Time.deltaTime;

            // Fade in
            if (this.currentFadeState == FadeState.FadeIn)
            {

                this.MusicSource.volume = Mathf.Clamp01(this.currentFadeTime / this.fadeTime);


            }
            // Fade out
            else if (this.currentFadeState == FadeState.FadeOut)
            {
                this.MusicSource.volume = Mathf.Clamp01((this.fadeTime - this.currentFadeTime) / this.fadeTime);
            }


            // If the fade in operation have ended, or where overwritten
            if (this.MusicSource.volume == 1f)
            {
                Debug.Log("SoundManager.Update - End Fade In Music");

                this.currentFadeState = FadeState.none;
                this.currentFadeTime = 0f;
            }

        }

        if (this.hintPopTimer > 0)
        {
            // Diminue the timer during no hintPop sound should be played
            this.hintPopTimer -= Time.deltaTime;
        }

    }

    public void TransitionPause()
    {

        this.MixerMaster.FindSnapshot("Music Pause").TransitionTo(0.1f);

    }

    public void TransitionNormal()
    {

        this.MixerMaster.FindSnapshot("Music Normal").TransitionTo(0.1f);

    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (this.audioListener != null)
        {
            this.audioListener.enabled = !pauseStatus;
        }
    }

    public void OnLevelWasLoaded(int lvl)
    {
        // Find this scene audio listener
        this.searchAudioListener();
    }

    #region Music
    //
    // Music
    //

    public void StopMusic()
    {

        // If a music is already playing, we stop it
        if (this.MusicSource.isPlaying)
        {

            this.MusicSource.Stop();
            this.MusicSource.time = 0f;
            this.MusicSource.clip = null;
        }

        this.musicPlaying = false;

    }

    private void LowerMusicWhileJinglePlay()
    {

        if (this.musicPlaying)
        {
            // Debug.Log("SoundManager.LowerMusicWhileJinglePlay - Lowered music");
            this.MusicSource.volume = this.MusicVolumeWhileJinglePlay;
            // this.MusicSource.ignoreListenerVolume = false;
        }

    }

    public void MuteMusicDuringAds()
    {

        if (this.musicPlaying)
        {
            // Debug.Log("SoundManager.MuteMusicDuringAds - Lowered music");
            this.MusicSource.volume = 0;
            // this.MusicSource.ignoreListenerVolume = false;
        }

    }

    private void PlayNextMusic()
    {

        this.MusicSource.volume = 1f;

        this.MusicSource.clip = this.nextMusicToPlay;
        this.MusicSource.Play();
    }

    public void ChooseNextMusicLevel(int lvl)
    {
        // RTBK implement

    }

    public void PlayMenuMusic()
    {

        this.musicPlaying = false;

        // If we should play it
        if (this.MusicOn && this.MenuMusic != null && !(this.MusicSource.isPlaying && this.MusicSource.clip.Equals(this.MenuMusic)))
        {

            Debug.Log("SoundManager.PlayMenuMusic - Let's play the menu music");

            this.MusicSource.Stop();

            this.FadeInMusic(0.5f);


            this.MusicSource.clip = this.MenuMusic;

            this.MusicSource.Play((ulong)0.1);

        }

    }

    public void PlayInGameMusic()
    {

        this.musicPlaying = false;

        // If we should play it
        if (this.MusicOn && this.InGameMusic != null && !(this.MusicSource.isPlaying && this.MusicSource.clip.Equals(this.InGameMusic)))
        {

            Debug.Log("SoundManager.PlayInGameMusic - Let's play the in game music");

            this.MusicSource.Stop();

            this.FadeInMusic(0.5f);


            this.MusicSource.clip = this.InGameMusic;

            this.MusicSource.Play((ulong)0.1);

        }

    }

    public void FadeInMusic(float timer)
    {

        this.currentFadeTime = 0f;
        this.fadeTime = timer;

        this.currentFadeState = FadeState.FadeIn;

    }
    #endregion



    #region PlayMethod
    // -_-_-_-_-_-_-_-
    // Play methods
    //

    /// <summary>
    /// Play one click menu sound, randomly selected from the list
    /// </summary>
    public void PlayClickMenu()
    {

        // If the hero can play a sound
        if (this.SoundOn && this.ClickMenu.Count > 0 && this.audioListener != null)
        {

            //Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

            AudioClip ac = this.ClickMenu[Random.Range(0, this.ClickMenu.Count)];
            this.VillagerSource.PlayOneShot(ac);

        }

    }

    /// <summary>
    /// Play one popup open sound, randomly selected from the list
    /// </summary>
    public void PlayPopupOpen()
    {

        // If the hero can play a sound
        if (this.SoundOn && this.PopupOpen.Count > 0 && this.audioListener != null)
        {

            //Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

            AudioClip ac = this.PopupOpen[Random.Range(0, this.PopupOpen.Count)];
            this.VillagerSource.PlayOneShot(ac);

        }

    }

    /// <summary>
    /// Play one popup close sound, randomly selected from the list
    /// </summary>
    public void PlayPopupClose()
    {

        // If the hero can play a sound
        if (this.SoundOn && this.PopupClose.Count > 0 && this.audioListener != null)
        {

            //Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

            AudioClip ac = this.PopupClose[Random.Range(0, this.PopupClose.Count)];
            this.VillagerSource.PlayOneShot(ac);

        }

    }

	/// <summary>
	/// Play one power fire sound, randomly selected from the list
	/// </summary>
	public void PlayPowerFire()
	{

		// If the hero can play a sound
		if (this.SoundOn && this.PowerFire.Count > 0 && this.audioListener != null)
		{

			//Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

			AudioClip ac = this.PowerFire[Random.Range(0, this.PowerFire.Count)];
			this.WorldSource.PlayOneShot(ac);

		}

	}

	/// <summary>
	/// Play one power lightning sound, randomly selected from the list
	/// </summary>
	public void PlayPowerLightning()
	{

		// If the hero can play a sound
		if (this.SoundOn && this.PowerLightning.Count > 0 && this.audioListener != null)
		{

			//Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

			AudioClip ac = this.PowerLightning[Random.Range(0, this.PowerLightning.Count)];
			this.WorldSource.PlayOneShot(ac);

		}

	}

	/// <summary>
	/// Play one power boost sound, randomly selected from the list
	/// </summary>
	public void PlayPowerBoost()
	{

		// If the hero can play a sound
		if (this.SoundOn && this.PowerBoost.Count > 0 && this.audioListener != null)
		{

			//Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

			AudioClip ac = this.PowerBoost[Random.Range(0, this.PowerBoost.Count)];
			this.WorldSource.PlayOneShot(ac);

		}

	}

	/// <summary>
	/// Play one power heal sound, randomly selected from the list
	/// </summary>
	public void PlayPowerHeal()
	{

		// If the hero can play a sound
		if (this.SoundOn && this.PowerHeal.Count > 0 && this.audioListener != null)
		{

			//Debug.Log("SoundManager.PlayHeroHit - Hero hit sound");

			AudioClip ac = this.PowerHeal[Random.Range(0, this.PowerHeal.Count)];
			this.WorldSource.PlayOneShot(ac);

		}

	}

	/// <summary>
	/// Play one villager scream sound, randomly selected from the list
	/// </summary>
	public void PlayVillagerScream()
	{

		// If the hero can play a sound
		if (this.SoundOn && this.VillagerScream.Count > 0 && this.audioListener != null)
		{

			AudioClip ac = this.VillagerScream[Random.Range(0, this.VillagerScream.Count)];
			this.VillagerSource.PlayOneShot(ac);

		}

	}


    #endregion

    #region Jingles

    /// <summary>
    /// Play one Win game jingle, randomly selected from the list
    /// </summary>
    public void PlayWinGame()
    {

        // If the hero can play a sound
        if (this.SoundOn && this.WinGame.Count > 0 && this.audioListener != null)
        {

            AudioClip ac = this.WinGame[Random.Range(0, this.WinGame.Count)];
            this.JingleSource.clip = ac;
            this.JingleSource.Play();
            this.LowerMusicWhileJinglePlay();

        }

    }

    #endregion


}