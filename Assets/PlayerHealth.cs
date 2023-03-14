using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] CapsuleCollider col;
    [SerializeField] Slider slider;

    private float currentVelocity;
    public Animator anim;
    public GameObject damagePop;
    public MovementStateManager move;

    public void TakeDamage(float damage, bool isCrit)
    {
        if (health > 0)
        {
            //Instantiate popup
            //DamagePopup.Create(Vector3.zero, (int)damage, isCrit);
            health -= damage;
            if (health <= 0)
            {
                ChangeHealth();
                PlayerDeath();
            }
        }
    }

    public void ChangeHealth()
    {
        float currentvalue = Mathf.SmoothDamp(slider.value, health, ref currentVelocity, 100 * Time.deltaTime);
        slider.value = currentvalue;
    }
    

    void PlayerDeath()
    {
        move = GetComponent<MovementStateManager>();
        move.gameObject.SetActive(false);
        col.enabled = !col.enabled;
        anim.SetTrigger("Dead");
    }


}
