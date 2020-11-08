using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	// Random pitch adjustment range.
	public float LowPitchRange = 0.95f;
	public float HighPitchRange = 1.05f;

    [SerializeField]
    private AudioClip select;
    [SerializeField]
    private AudioClip badSelect;
    [SerializeField]
    private AudioClip removeBlocks;
    [SerializeField]
    private AudioClip iceBroken;
    [SerializeField]
    private AudioClip gameOver;
    [SerializeField]
    private AudioClip animalDropOff;
    

    private void Start() {
        AnimalController.OnAnimalSelected += PlaySelect;
        UserController.OnAnimalsToRemove += PlayRemoveBlocks;
        GSM.OnGameOver += PlayGameOver;
    }

    private void PlaySelect(AnimalController _){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = select;
        // this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayBadSelect(){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = badSelect;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayRemoveBlocks(AnimalController[] _){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = removeBlocks;
        Debug.Log("Play");
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayIceBroken(){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = iceBroken;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayGameOver(){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = gameOver;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayAnimalDropOff(){
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        this.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
        this.gameObject.GetComponent<AudioSource>().clip = animalDropOff;
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
