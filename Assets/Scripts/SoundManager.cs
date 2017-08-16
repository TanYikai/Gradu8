using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public bool mute;

    public AudioSource coinSound;
    public AudioSource powerupSound;
    public AudioSource deathSound;
    public AudioSource jumpSound;

    public AudioSource jellyBurst;
    public AudioSource blacklyBurst;
    public AudioSource bellyBurst;
    public AudioSource gellyBurst;
    public AudioSource rellyBurst;
    public AudioSource bombBurst;

    public AudioSource birdSound;
    public AudioSource birdAfterEffect;

    public AudioSource dragonRoar;
    public AudioSource dragonKill;

    public AudioSource fullAttackBurst;

    public AudioSource damageSound;
    public AudioSource timeHack;

    public AudioSource airWalkSound;

    public AudioSource platformCrushSound;

    public AudioSource bgm;
	
	void Start () {
      
    }

    public void Mute()
    {
        mute = !mute;
    }

    public void MuteBGM()
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        else
        {
            bgm.Play();
        }
    }

    public void CoinBling()
    {
        if (!mute)
        {
            //this if condition is to prevent skipping of coin sounds when multiple coins are picked really fast
            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
        }
    }

    public void PowerupGrab()
    {
        if (!mute)
        {
            //this if condition is to prevent skipping of coin sounds when multiple coins are picked really fast
            if (powerupSound.isPlaying)
            {
                powerupSound.Stop();
                powerupSound.Play();
            }
            else
            {
                powerupSound.Play();
            }
        }
    }

    public void DeathSound()
    {
        if (!mute)
        {
            deathSound.Play();
        }
    }

    public void JumpSound()
    {
        if (!mute)
        {
            jumpSound.Play();
        }      
    }
	
	public void JellyHitSound(int number)
    {
        if (!mute)
        {

            if (number == 1)
            {
                if (jellyBurst.isPlaying)
                {
                    jellyBurst.Stop();
                    jellyBurst.Play();
                }
                else
                {
                    jellyBurst.Play();
                }

                if (birdSound.isPlaying || birdAfterEffect.isPlaying)
                {
                    birdSound.Stop();
                    //birdAfterEffect.Stop();
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
                else
                {
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
            }

            if (number == 2)
            {
                if (blacklyBurst.isPlaying)
                {
                    blacklyBurst.Stop();
                    blacklyBurst.Play();
                }
                else
                {
                    blacklyBurst.Play();
                }

                if (birdSound.isPlaying || birdAfterEffect.isPlaying)
                {
                    birdSound.Stop();
                    //birdAfterEffect.Stop();
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
                else
                {
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
            }

            if (number == 3)
            {
                if (bellyBurst.isPlaying)
                {
                    bellyBurst.Stop();
                    bellyBurst.Play();
                }
                else
                {
                    bellyBurst.Play();
                }

                if (birdSound.isPlaying || birdAfterEffect.isPlaying)
                {
                    birdSound.Stop();
                    //birdAfterEffect.Stop();
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
                else
                {
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
            }

            if (number == 4)
            {
                if (gellyBurst.isPlaying)
                {
                    gellyBurst.Stop();
                    gellyBurst.Play();
                }
                else
                {
                    gellyBurst.Play();
                }

                if (birdSound.isPlaying || birdAfterEffect.isPlaying)
                {
                    birdSound.Stop();
                    //birdAfterEffect.Stop();
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
                else
                {
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
            }

            if (number == 5)
            {
                if (rellyBurst.isPlaying)
                {
                    rellyBurst.Stop();
                    rellyBurst.Play();
                }
                else
                {
                    rellyBurst.Play();
                }

                if (birdSound.isPlaying || birdAfterEffect.isPlaying)
                {
                    birdSound.Stop();
                    //birdAfterEffect.Stop();
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
                else
                {
                    birdSound.Play();
                    birdAfterEffect.Play();
                }
            }
        }
    }

    public void BombHitSound()
    {
        if (!mute)
        {
            if (bombBurst.isPlaying)
            {
                bombBurst.Stop();
                bombBurst.Play();
            }
            else
            {
                bombBurst.Play();
            }

            if (birdSound.isPlaying || birdAfterEffect.isPlaying)
            {
                birdSound.Stop();
                //birdAfterEffect.Stop();
                birdSound.Play();
                birdAfterEffect.Play();
            }
            else
            {
                birdSound.Play();
                birdAfterEffect.Play();
            }
        }
    }

    public void DragonRoar()
    {
        if (!mute)
        {
            if (dragonRoar.isPlaying)
            {

            }
            else
            {
                dragonRoar.Play();
            }
        }
    }

    public void DragonMute()
    {
        if (!mute)
        {
            dragonRoar.Stop();
        }
    }

    public void DragonKill()
    {
        if (!mute)
        {
            dragonKill.Play();
        }
    }

    public void FullAttackSound()
    {
        if (!mute)
        {
            fullAttackBurst.Play();
        }

        Handheld.Vibrate();
    }

    public void DamageSound()
    {
        if (!mute)
        {
            damageSound.Play();
        }
    }

    public void TimeHackStart()
    {
        if (!mute)
        {
            timeHack.Play();
        }
    }

    public void TimeHackStop()
    {
        if (!mute)
        {
            timeHack.Stop();
        }
    }

    public void AirWalkStart()
    {
        if (!mute)
        {
            airWalkSound.Play();
        }
    }

    public void AirWalkStop()
    {
        if (!mute)
        {
            airWalkSound.Stop();
        }
    }

    public void PlatformCrushSound()
    {
        if (!mute)
        {
            if (jellyBurst.isPlaying)
            {
                jellyBurst.Stop();
                jellyBurst.Play();
            }
            else
            {
                jellyBurst.Play();
            }

            if (birdSound.isPlaying || birdAfterEffect.isPlaying)
            {
                birdSound.Stop();
       
                birdSound.Play();
                birdAfterEffect.Play();
            }
            else
            {
                birdSound.Play();
                birdAfterEffect.Play();
            }

        }
    }

    public void BGM()
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
            bgm.Play();
        }
        else
        {
            bgm.Play();
        }
    }

    public void BGMStop()
    {
        bgm.Stop();
    } 
}
