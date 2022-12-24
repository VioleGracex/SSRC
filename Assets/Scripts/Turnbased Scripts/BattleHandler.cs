using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class BattleHandler : MonoBehaviour
{
    private static BattleHandler instance;
    public State state;
    public static BattleHandler Getinstance()
    {
        return instance;
    }

    [SerializeField]
    TextMeshProUGUI whoseTurn, targetName, partTargetText,enemyStatsText;
    [SerializeField]
    Transform[] heroLocations, enemyLocations;
    [SerializeField]  // instead of serialize use instantiate and save them don't forget order in layer and it's child
    public List<Transform> heroes, enemies;

    GameObject attacker;
    public GameObject target;
    
    [SerializeField] float slideSpeed = 10f;
    public bool playerTurn = true, playerStart = true; // get if he succeeded in miniGame fast click if yes he gets to start else he got ambushed 

    [SerializeField]
    GameObject tokenPrefab; //hp bar for heros
    [SerializeField]
    Button attackButton;

    [SerializeField]
    ScrollMechanic scrollMechanic;

    List<string> temp;

    string targetedPart;

    public enum State
    {
        WaitingForPlayer,
        Busy,
        Attacking,
        SlidingForward,
        SlidingBackward,
        Fleeing,
        Done,
    }
    private void Awake()
    {
        instance = this;
        if (playerStart)
        {
            playerTurn = true;
            SetTurnCharges();
        } 

        List<string> temp = new List<string> {"Head","Body","RightArm","LeftArm","RightLeg","LeftLeg"};
        scrollMechanic.Initialize(temp, true, 0);
        scrollMechanic.RecolorText(Color.black);
    }


    public void FlipHeroes()
    {
        state = State.Fleeing;
        foreach (Transform hero in heroes)
        {
            hero.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
#region  //movement
    public void MovementTranslation(Transform attacker, Vector3 target)
    {
        attacker.position = Vector3.MoveTowards(attacker.position, target, slideSpeed * Time.deltaTime);        
    }

    IEnumerator FleeToTheRight()
    {
        foreach (Transform hero in heroes)
        {
            hero.position += new Vector3(0.2f, 0, 0);
        }

        yield return new WaitForSeconds(3.0f);
        state = State.WaitingForPlayer;
        //end level
    }

    public void SlideToTargetFunction(GameObject attacker,GameObject target)
    {
        StartCoroutine(SlideToTarget(attacker,target));
    }


    IEnumerator SlideToTarget(GameObject attacker, GameObject target)
    {
        if (attacker.GetComponent<IReturnTurnCharges>().ReturnCharges() <= 0)
        {
            Debug.Log("No Charges");
        }
        if (state == State.SlidingForward && target.gameObject != null)
        {
            //myAnimator.Play("Walking");
            Vector3 temp = target.transform.position + target.transform.right * (-2f);
            MovementTranslation(attacker.transform, temp);
          
            yield return new WaitUntil(() => Mathf.Abs(attacker.transform.position.x - temp.x) < 2f);
            state = State.Done;
            Debug.Log("done" + (Mathf.Abs(attacker.transform.position.x - temp.x) < 2f));
        }
        attacker.GetComponent<IAttack>().Attack(target);
        if(target.gameObject != null)
        {
            //add busy and wait for animation to end then change to slidebackward
            //might be unnecessary to have stop coroutine here or in switch
            state = State.SlidingBackward;
            StartCoroutine(SlideToPlace(attacker));
           //StopCoroutine(SlideToTargetPlayer(attacker, target));
        } 
       
        yield return new WaitForSeconds(2f);
        //here fix
    }
  

      IEnumerator SlideToPlace(GameObject attacker)
    {
        //GameObject attacker = this.GetComponent<MouseSelection>().playerTarget;
        if (state == State.SlidingBackward)
        {
            //attacker.GetComponent<Unit>().myAnimator.Play("Walking");
            Vector3 temp = attacker.GetComponent<IReturnPosition>().ReturnPosition();
            MovementTranslation(attacker.transform, temp);
            yield return new WaitUntil(() => Mathf.Abs(temp.x - attacker.transform.position.x) < 0.6f);
        }

        if(playerTurn)
         state = State.WaitingForPlayer;

        if (attacker.GetComponent<IReturnTurnCharges>().ReturnCharges() <= 0)
        {
            Debug.Log("No Charges");
             attackButton.interactable = false;
            //disable attack button
        }
        //attacker.GetComponent<Unit>().myAnimator.Play("Idle");

        yield return null;
    }
#endregion
   
    void Update()
    {
        
        switch (state)
        {
            case State.WaitingForPlayer:
                StopAllCoroutines();
                //enable player actions
                break;
            case State.SlidingForward:
                StartCoroutine(SlideToTarget(attacker, target));
                break;
            case State.SlidingBackward:
                StopCoroutine(SlideToTarget(attacker, target));
                StartCoroutine(SlideToPlace(attacker));
                break;
            case State.Fleeing:
                StartCoroutine(FleeToTheRight());
                break;
            case State.Busy:
                break;
            case State.Done:
                break;
            default:
                //enable player actions
                break;
        }
    }
#region //setting values
    public void SpawnParty(string[] party)
    {
        //get names instantiate
    }

    private void SetTurnCharges()
    {
        if (playerTurn)
        {
            foreach (Transform hero in heroes)
            {
                hero.GetComponent<HeroAbstract>().Newturn();
            }
        }
        else
        {
            foreach (Transform enemy in enemies)
            {
                enemy.GetComponent<EnemyAbstract>().Newturn();
            }
        }
    }
    public void SetPartTarget(string part)
    {
        partTargetText.text = part;
        CheckAttackAvailability();
    }

    public void SetAttacker(GameObject unit)
    {
        if(unit.GetComponent<IReturnTurnCharges>().ReturnCharges() > 0)
        {
            attacker = unit;
            //enable attack button
        }
        else 
        {
            Debug.Log("no charges"); // make popups
            attacker = null;
        }
        CheckAttackAvailability();
    }

     public void SetTarget(GameObject unit)
    {
        if(state == State.WaitingForPlayer)
        {
            target = unit;
            UpdateToggleUI();
            UpdateStatsUI();
        }
    }
#endregion
    
#region //updating ui
    private void UpdateToggleUI()
    {
        List <EnemiesCharStatsBase.PartData> targetPartsData = target.GetComponent<EnemyAbstract>().myParts;
        targetName.text = target.name;
        GameObject toggleParent = GameObject.FindGameObjectWithTag("BodyTarget");
        foreach (var item in targetPartsData)
        {
            if(item.hp <=0)
            {
                Debug.Log("edit Hp text");
                toggleParent.transform.Find(item.partName + "Toggle").GetComponent<Toggle>().interactable = false;   
                scrollMechanic.EditPartTextWheel(item.partName, Color.red);    
            }
        }
    }

    private void UpdateStatsUI()
    {
        EnemyAbstract enemy = target.GetComponent<EnemyAbstract>(); 
        EnemiesCharStatsBase enemyStats = target.GetComponent<EnemyAbstract>().myStats;
        string placeholderString;
        if(enemy.exhausted)
        {
            placeholderString = "Exhausted"; 
        }
        else
        {
            placeholderString = "NotExhausted";
        }
        enemyStatsText.text = "ATK: " + enemyStats.attack.ToString("f0")
            + "\nDef: " + enemyStats.defense.ToString("f0")
            + "\nDex: " + enemyStats.dex.ToString("f0")
            + "\nHP: " + enemy.HP.ToString("f0")
            + "\nStatus: "+ placeholderString
            + "\nCoreType: " + enemyStats.core_type;
    }

    private void SpawnCharTokens()
    {
        GameObject token = Instantiate(tokenPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        token.transform.SetParent (GameObject.FindGameObjectWithTag("Tokens").transform, false);
        //spawn card HeroAbstract mystats.CardSprite
    }
    private void UpdateHpBar()
    {
        //will do later
    }

    private void CheckAttackAvailability()
    {
        //conditions for attacking avaliable target, has charges , player turn
         if(target != null && attacker != null)
         {
            bool hasCharges = attacker.GetComponent<IReturnTurnCharges>().ReturnCharges() > 0;
            bool partDestroyed = target.GetComponent<EnemyAbstract>().ReturnPartHP(scrollMechanic.GetCurrentName()) <= 0;
            Debug.Log(partDestroyed);
            //Debug.Log("Part Already Destroyed"); // make popups
            if(hasCharges && !partDestroyed)
                attackButton.interactable = true; 
            else
                attackButton.interactable = false; 
         }
         else
         {
            attackButton.interactable = false; 
         }
    }
#endregion


}
