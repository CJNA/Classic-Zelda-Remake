using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Credit : https://www.daggerhart.com/unity-audio-and-sound-manager-singleton-script/

public class AudioManager : MonoBehaviour
{
	// public AudioSource 
	public static AudioManager Instance = null;
	public AudioSource EffectsSource;
    public AudioSource EnemyKill;
	public AudioSource MusicSource;

    public AudioClip rupee_collection_sound_clip;
    public AudioClip restoring_heart_collection_sound_clip;
    public AudioClip key_collection_sound_clip;
    public AudioClip danger_sound_clip;
    public AudioClip bossAttack_sound_clip;
    public AudioClip swordAttack_sound_clip;
    public AudioClip swordLaserAttack_sound_clip;
    public AudioClip monsterDeath_sound_clip;
    public AudioClip underGroundBGM;
    public AudioClip boomerangAttack_sound_clip;
    public AudioClip obtainItem_sound_clip;
    public AudioClip roomClear_sound_clip;
    public AudioClip openLockedDoor_sound_clip;
    public AudioClip beatTheDungeon_sound_clip;
    public AudioClip playerPushed_sound_clip;
    public AudioClip dead_sound_clip;
    public AudioClip bomb_sound_clip;
    public AudioClip OmaeWasShindeiru_sound_clip;

    private void Awake() {
    	if (Instance == null) {
    		Instance = this;
    	} else {
    		Destroy(gameObject);
    	}
    	PlayMusic(underGroundBGM);

    	DontDestroyOnLoad (gameObject);
    }

    public void Play(AudioClip clip) {
    	EffectsSource.clip = clip;
    	EffectsSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
    	MusicSource.clip = clip;
    	MusicSource.Play();
    }

    public void PlayKill(AudioClip clip) {
        EnemyKill.clip = clip;
        EnemyKill.Play();
    }

    public void RupeeEffect() {
    	Play(rupee_collection_sound_clip);
    }

    public void RestroingHeartEffect() {
    	Play(restoring_heart_collection_sound_clip);
    }

    public void KeyEffect() {
    	Play(key_collection_sound_clip);
    }

    public void DangerEffect() {
    	if (!EffectsSource.isPlaying) {
    		Play(danger_sound_clip);
    	}
    }

    public void BossAttack() {
    	Play(bossAttack_sound_clip);
    }

    public void SwordAttack() {
    	Play(swordAttack_sound_clip);
    }

    public void MonsterDeath() {
    	PlayKill(monsterDeath_sound_clip);
    }

    public void SwordLaserAttack() {
    	Play(swordLaserAttack_sound_clip);
    }

    public void BoomerangAttack() {
    	Play(boomerangAttack_sound_clip);
    }

    public void ObtainItem() {
    	Play(obtainItem_sound_clip);
    }

    public void RoomClear() {
    	Play(roomClear_sound_clip);
    }

    public void OpenLockedDoor() {
    	Play(openLockedDoor_sound_clip);
    }

    public void BeatTheDungeon() {
    	PlayMusic(beatTheDungeon_sound_clip);
    }

    public void PlayerGetPushed() {
    	Play(playerPushed_sound_clip);
    }

    public void DeadSound() {
    	PlayMusic(dead_sound_clip);
    }

    public void BombSound() {
    	Play(bomb_sound_clip);
    }

    public void OmaeSound() {
        Play(OmaeWasShindeiru_sound_clip);
    }
} 
