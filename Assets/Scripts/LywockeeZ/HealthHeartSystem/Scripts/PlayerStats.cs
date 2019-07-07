/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }
    public Animator animator;
    public bool isDeath = false;
    public ParticleSystem brust;
    public Canvas Canvas;

    private void Update()
    {
        if (Health == 0 && GameManager.gameManager.isEnd == false && isDeath == false)
        {
            StartCoroutine(die());
           
        }
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    IEnumerator CameraMove()
    {
        yield return new WaitForSeconds(1f);
        CameraController.Instance.startMove = true;
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.endgame();
    }
    IEnumerator die()
    {
        isDeath = true;
        animator.SetTrigger("Death");
        AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\fall") as AudioClip, new Vector3(0, 0, 0));
        animator.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\Death") as AudioClip, new Vector3(0, 0, 0));
        StartCoroutine(CameraMove());
        Debug.Log("End");
        Canvas.enabled = false;
    }
}
