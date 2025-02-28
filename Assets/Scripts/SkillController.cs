using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private string h = "Horizontal";
    private string v = "Vertical";
    public Transform skillSpawnPoint;
    public GameObject skillPrefab;
    private Animator anim;
    private bool skillTriggered = false;
    public int damage = 20;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !skillTriggered)
        {
            if (Input.GetAxis(h) != 0 || Input.GetAxis(v) != 0)
            {
                anim.SetTrigger("ShooterMask");
                //Invoke("TriggerSkill", 0.5f);
                skillTriggered = true;
                Invoke(nameof(ResetSkillTrigger), 2f);
            }

            else
            {
                anim.SetTrigger("Shooter");
                //Invoke("TriggerSkill", 0.5f);
                skillTriggered = true;
                Invoke(nameof(ResetSkillTrigger), 2f);
            }

        }
    }
    void ResetSkillTrigger()
    {
        skillTriggered = false;
    }
    //void CastSkill()
    //{
    //    Debug.Log("Casting skill...");
    //    Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation); // Tạo hiệu ứng skill
    //}

    public void TriggerSkill()
    {
        if (skillPrefab != null && skillSpawnPoint != null)
        {
            Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("khoong co spawn");
        }
    }

}
