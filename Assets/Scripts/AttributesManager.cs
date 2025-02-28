using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public int armor;

    public float critDamage = 1.5f;
    public float critChance = 0.5f;

    public GameObject damageTextPrefab; // Prefab của sát thương
    public Canvas damageCanvas; // Canvas chứa Damage Text

    public void takeDamage(int amount)
    {

        //health -= amount - (amount * armor / 100);
        int finalDamage = amount - (amount * armor / 100); // Tính sát thương sau khi trừ giáp
        health -= finalDamage;

        if (damageTextPrefab != null)
        {
            GameObject dmgText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, damageCanvas.transform);  // Sửa ở đây
            FloatingDamage floatingDamage = dmgText.GetComponent<FloatingDamage>();
            if (floatingDamage != null)
            {
                floatingDamage.SetDamage(finalDamage, transform); // Truyền sát thương và Transform mục tiêu
            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            Slider slider = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Slider>();
            slider.value = health;

            if (health <=0 )
            {
                EnemyDie();
            }
            
        }
    }
    public void EnemyDie()
    {
        Debug.Log("ke thu da chet");
        Animator ani = gameObject.transform.GetChild(0).GetComponent<Animator>();
        ani.SetBool("isDead", true);
        Destroy(gameObject, 5f);
    }
    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            float totalDamage = attack;
            if (Random.Range(0f, 1f) < critChance)
                totalDamage *= critChance;
            atm.takeDamage((int)totalDamage);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy")) // Kiểm tra va chạm với kẻ địch
    //    {
    //        Gây sát thương hoặc hiệu ứng
    //        Debug.Log("Hit " + other.name);
    //        Destroy(gameObject); // Hủy skill
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}