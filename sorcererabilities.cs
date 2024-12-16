using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class sorcererabilities : MonoBehaviour
{
    bool teleport = false;
    [SerializeField]
    Camera _maincamera;

    public GameObject cloneskeleton;
    private UnityEngine.AI.NavMeshAgent agent;

    private float health;
    public float newhealth;

    public Slider healthslider;
    public TMP_Text healthtext;

    public InfernoSpawner ispawner;

    public bool detonated = false;
    public bool exploded = false;

    public sorcererunlockabilities unlockabilities;

    public bool teleportactive = false;
    public bool cloneactive = false;

    public float healthpotions = 0;
    public bool invincible = false;
    private float invincibleHealth;
    private bool slowMotionEnabled = false;  // Slow motion toggle
    private float originalTimeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        health = 100f;
        newhealth = 100f;
       
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = (newhealth / health);
        if (invincible)
        {
            newhealth = invincibleHealth;
        }
        if (Input.GetKeyDown(KeyCode.W) && unlockabilities.teleportunlocked)
        {
            teleport = true;
        }
        if (Input.GetMouseButtonDown(1) && teleport)
        {
            Ray ray = _maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.Warp(hit.point);
            }
            teleport = false;
            teleportactive = true;
        }
        if (exploded)
        {
            cloneskeleton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleSlowMotion();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnlockAllAbilities();
        }



        healthtext.text = newhealth + "/" + health;


        Debug.Log(unlockabilities.cloneunlocked);
        if (Input.GetKeyDown(KeyCode.Q) && unlockabilities.cloneunlocked)
        {
            cloneskeleton.SetActive(true);
            cloneskeleton.transform.position = transform.position;
            cloneskeleton.transform.rotation = transform.rotation;

            detonated = true;
            Debug.Log("clonnneee");
            explodeclone();


            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (healthpotions > 0)
            {
                healthpotions -= 1;
                newhealth += (health / 2);
            }


        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            newhealth += 20;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            newhealth-=20;
        }
        if (newhealth > health)
        {
            newhealth = health;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            invincibleHealth=newhealth;
            invincible = !invincible;
            Debug.Log("Invincibility: " + invincible);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetCooldowns();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GainAbilityPoints(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(100);
        }


    }

    private void UnlockAllAbilities()
    {
        unlockabilities.teleportunlocked = true;
        unlockabilities.teleportimage.gameObject.SetActive(false);
        unlockabilities.cloneunlocked = true;
        unlockabilities.cloneimage.gameObject.SetActive(false);
        unlockabilities.infernounlocked = true;
        unlockabilities.infernoimage.gameObject.SetActive(false);

    }


    private void GainXP(int amount)
    {
        unlockabilities.sorcererlevels.increaseXp(amount);
    }

    private void GainAbilityPoints(int amount)
    {
        unlockabilities.sorcererlevels.currability += amount;
        Debug.Log("Gained " + amount + " Ability Points. Total Ability Points: " );
    }
    private void ResetCooldowns()
    {
        Debug.Log("resettttt");
        Debug.Log(unlockabilities.teleportunlocked);
        Debug.Log(unlockabilities.cloneunlocked);
        Debug.Log(unlockabilities.infernounlocked);


       
            
            unlockabilities.StartCountdown(unlockabilities.teleportimage, 0, unlockabilities.teleporttext,"teleport");
        
       
            unlockabilities.StartCountdown(unlockabilities.cloneimage, 0, unlockabilities.clonetext,"clone");
        
        
            Debug.Log("resettttt");
            unlockabilities.StartCountdown(unlockabilities.infernoimage, 0, unlockabilities.infernotext,"inferno");
        

        Debug.Log("Cooldowns reset for all abilities!");
    }
    private void explodeclone()
    {
        cloneactive = true;
        StartCoroutine(timedexplosion());
    }
    private void ToggleSlowMotion()
    {
        slowMotionEnabled = !slowMotionEnabled;
        Time.timeScale = slowMotionEnabled ? 0.5f : originalTimeScale;
        Debug.Log("Slow Motion: " + slowMotionEnabled);
    }
    private IEnumerator timedexplosion()
    {
        yield return new WaitForSeconds(5);
        
        exploded = true;
        detonated = false;

        cloneskeleton.transform.position = new Vector3(0,0,0);
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "goblin")
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "goblin")
        {
            
        }
    }*/



}
