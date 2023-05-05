using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class BattleHandler : MonoBehaviour
{

#region Vars
    private static BattleHandler instance;
    public State state;
    
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
    public static BattleHandler Getinstance()
    {
        return instance;
    }

    public enum State
    {
        WaitingForPlayer,
        Busy,
        Attacking,
        Returning,
        Fleeing,
        ReachedTarget,
    }
#endregion
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
        CheckAttackAvailability();
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.WaitingForPlayer:
                StopAllCoroutines();
                //enable player actions
                break;
            case State.Attacking:
                StartCoroutine(SlideToTarget());       
                break;  
            case State.ReachedTarget:
                attacker.GetComponent<IAttack>().Attack(target, targetedPart);
                state = State.Returning;
                break; 
            case State.Returning:
                StartCoroutine(SlideToPlace());
                //check if turn ends 
                break;                          
            case State.Fleeing:
                StartCoroutine(FleeToTheRight());
                break;
            case State.Busy:
                break;
            default:
                //enable player actions
                break;
        }
    }

#region  //movement

    public void FlipHeroes()
    {
        state = State.Fleeing;
        foreach (Transform hero in heroes)
        {
            hero.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
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

    public void LocalSlideToTarget(string partName)
    {
        Debug.Log("teezi");

        if (attacker.GetComponent<IReturnTurnCharges>().GetTurnCharges() <= 0)
        {
            Debug.Log("No Charges");
            return;
        }

        state = State.Attacking;
        attackButton.interactable = false;
        targetedPart = partName;
      
    }

    IEnumerator SlideToTarget()
    {
        Vector3 temp = target.transform.position + target.transform.right * (-2f);
        //myAnimator.Play("Walking");
        MovementTranslation(attacker.transform, temp);
        yield return new WaitUntil(() => Mathf.Abs(attacker.transform.position.x - temp.x) < 2f);
        state = State.ReachedTarget;
        yield return new WaitForSeconds(2f);
        //here fix
    }
  

      IEnumerator SlideToPlace()
    {
        //attacker.GetComponent<Unit>().myAnimator.Play("Walking");
        Vector3 temp = attacker.GetComponent<IReturnPosition>().GetPosition();
        MovementTranslation(attacker.transform, temp);
        yield return new WaitUntil(() => Mathf.Abs(temp.x - attacker.transform.position.x) < 0.6f);

        if(playerTurn)
            state = State.WaitingForPlayer;

        if (attacker.GetComponent<IReturnTurnCharges>().GetTurnCharges() <= 0)
        {
            Debug.Log("No Charges");
            attackButton.interactable = false;
            //disable attack button
        }
        //attacker.GetComponent<Unit>().myAnimator.Play("Idle");

        yield return null;
    }
#endregion
   
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
    public void EndPlayerTurn()
    {
        playerTurn = false; 
        SetTurnCharges();
    }
    public void EndEnemyTurn()
    {
        playerTurn = false; 
        SetTurnCharges();
    }

    public void SetPartTarget(string part)
    {
        partTargetText.text = part;
        CheckAttackAvailability();
    }

    public void SetAttacker(GameObject unit)
    {
        //fix for enemy object useless check for attack button during enemy turn
        if(unit.GetComponent<IReturnTurnCharges>().GetTurnCharges() > 0)
        {
            attacker = unit;
            //enable attack button
        }
        else 
        {
            Debug.Log("no charges"); // make popups
            attacker = null;
        }
        if(playerTurn)
            CheckAttackAvailability();
    }

     public void SetTarget(GameObject unit)
    {
        if(state == State.WaitingForPlayer)
        {
            target = unit;
            if(playerTurn)
                CheckAttackAvailability();                                
        }
    }
#endregion
    
#region //updating ui
   
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
         if(target != null && attacker != null && state == State.WaitingForPlayer)
         {
            bool hasCharges = attacker.GetComponent<IReturnTurnCharges>().GetTurnCharges() > 0;
            if(hasCharges)
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

#region old codes
    /*  private void UpdateToggleUI()
    {
        List <EnemiesCharStatsBase.PartData> targetPartsData = target.GetComponent<EnemyAbstract>().myParts;
        targetName.text = target.name;
        GameObject toggleParent = GameObject.FindGameObjectWithTag("BodyTarget");
        foreach (var item in targetPartsData)
        {
            if(item.currentArmor <=0)
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
 */
#endregion
}
