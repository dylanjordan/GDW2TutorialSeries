using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] GameObject Mario;
    [SerializeField] List<GameObject> Goombas;
    [SerializeField] List<GameObject> Koopas;

    Animator marioAnimator;
    MarioController marioMovement;

    private void Start()
    {
        marioAnimator = Mario.GetComponent<Animator>();
        marioMovement = Mario.GetComponent<MarioController>();
    }

    private void Update()
    {
        if (marioMovement.GetIsRunning())
        {
            marioAnimator.SetBool("isWalking", true);
        }
        else
        {
            marioAnimator.SetBool("isWalking", false);
        }

        if (marioMovement.GetIsDead())
        {
            marioAnimator.SetBool("isDead", true);
        }
        else
        {
            marioAnimator.SetBool("isDead", false);
        }

        if (marioMovement.GetIsGrounded())
        {
            marioAnimator.SetBool("isJumping", false);
        }
        else
        {
            marioAnimator.SetBool("isJumping", true);
        }

        if (marioMovement.GetIsBig())
        {
            marioAnimator.SetBool("isBig", true);
        }
        else
        {
            marioAnimator.SetBool("isBig", false);
        }

        for (int i = 0; i < Goombas.Count; i++)
        {
            if (Goombas[i] != null)
            {
                if (Goombas[i].GetComponent<Goomba>().GetIsSquashed())
                {
                    Goombas[i].GetComponent<Animator>().SetBool("Squashed", true);
                }
                else
                {
                    Goombas[i].GetComponent<Animator>().SetBool("Squashed", false);
                }
            }
        }

        for (int i = 0; i < Koopas.Count; i++)
        {
            if (Koopas[i] != null)
            {
                if (Koopas[i].GetComponent<Koopa>().GetIsSquashed())
                {
                    Koopas[i].GetComponent<Animator>().SetBool("isSquashed", true);
                }
                else
                {
                    Koopas[i].GetComponent<Animator>().SetBool("isSquashed", false);
                }

                if (Koopas[i].GetComponent<Koopa>().GetIsKicked())
                {
                    Koopas[i].GetComponent<Animator>().SetBool("isKicked", true);
                }
                else
                {
                    Koopas[i].GetComponent<Animator>().SetBool("isKicked", false);
                }
            }
        }
    }
}
