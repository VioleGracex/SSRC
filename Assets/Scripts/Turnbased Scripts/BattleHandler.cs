using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{

#region Vars
    private static BattleHandler instance;
    [SerializeField]
    MouseSelection mouseSelection;
    public State state;
    
    [SerializeField]
    TextMeshProUGUI whoseTurn;

    [SerializeField]
    Transform[] heroLocations, enemyLocations;

   /*  [SerializeField]  // instead of serialize use instantiate and save them don't forget order in layer and it's child
    public List<Transform> heroes, enemies; */
    [SerializeField]
    HeroAbstract[] herosAbstract;
    EnemyAbstract[] enemiesAbstract;
    [SerializeField]
    GameObject attacker, target;
    
    [SerializeField] float slideSpeed = 20f;
    public bool playerTurn = true, playerStart = true; // get if he succeeded in miniGame fast click if yes he gets to start else he got ambushed 

    [SerializeField]
    Button attackButton, endTurnButton;
    [SerializeField]
    GameObject partMenu;
    
    GetPartsHPBars partsHPBars;
    string targetedPart;
    public static BattleHandler Getinstance()
    {
        return instance;
    }

    public enum State
    {
        WaitingForPlayer,
        WaitingForEnemy,
        Busy,
        Attacking,
        Returning,
        Fleeing,
        ReachedTarget,
    }
#endregion
   
#region Init
    private void OnEnable()
    {
        instance = this;
        mouseSelection = this.GetComponent<MouseSelection>();
        if(playerStart)
        {
            playerTurn = true;
        } 
        UpdateWhoseTurn();
        InitGetAllPlayerHeroes();
        InitGetAllEnemies();
    }

    void Start()
    {
        partsHPBars = FindObjectOfType<GetPartsHPBars>();
        SetTurnCharges();
        CheckAttackAvailability();
    }

    private void InitGetAllPlayerHeroes()
    {
        herosAbstract = GameObject.FindObjectsOfType<HeroAbstract>();
    }
    
    private void InitGetAllEnemies()
    {
        enemiesAbstract = GameObject.FindObjectsOfType<EnemyAbstract>();
    }
#endregion
#region Loop
    void FixedUpdate()
    {
        if(state == State.WaitingForPlayer && playerTurn)
            endTurnButton.interactable = true;
        else
            endTurnButton.interactable = false;
        switch (state)
        {
            case State.WaitingForPlayer:
                StopAllCoroutines();
                /* if(!HerosHasCharges())
                    EndTurn(); */
                //check if turn ends not best place will be called too many times
                //enable player actions
                break;
            case State.WaitingForEnemy:
                EnemyBattleManager.Getinstance().SendEnemyToAttack();
                break;                
            case State.Attacking:
                StartCoroutine(SlideToTarget());       
                break;  
            case State.ReachedTarget:
                attacker.GetComponent<IAttack>().BasicAttack(target, targetedPart);
                state = State.Returning;
                break; 
            case State.Returning:
                StartCoroutine(SlideToPlace());
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
#endregion
#region movement

    public void FlipHeroes()
    {
        state = State.Fleeing;
        foreach (HeroAbstract hero in herosAbstract)
        {
            hero.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void MovementTranslation(Transform attacker, Vector3 target)
    {
        attacker.position = Vector3.MoveTowards(attacker.position, target, slideSpeed * Time.deltaTime);        
    }

    IEnumerator FleeToTheRight()
    {
        foreach (HeroAbstract hero in herosAbstract)
        {
            hero.transform.position += new Vector3(0.2f, 0, 0);
        }

        yield return new WaitForSeconds(3.0f);
        state = State.WaitingForPlayer;
        //end level
    }

    public void LocalSlideToTarget(string partName)
    {
        if (attacker.GetComponent<IReturnTurnCharges>().GetTurnCharges() <= 0)//null when enemy attacks why ?
        {
            Debug.Log("No Charges");
            return;
        }
        attacker.GetComponent<IReturnTurnCharges>().UseCharges(1);
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
        {
            state = State.WaitingForPlayer;
            CheckAttackAvailability();
        }
        else
        {
            state = State.WaitingForEnemy;
        }
           
        yield return null;
    }
#endregion
   
#region setting/getting values
    public void SpawnParty(string[] party)
    {
        //get names instantiate
    }
    private void SetTurnCharges()
    {
        if (playerTurn)
        {
            foreach (HeroAbstract hero in herosAbstract)
                hero.Newturn();
        }
        else
        {
            foreach (EnemyAbstract enemy in enemiesAbstract)
                enemy.Newturn();
        }
    }
    private bool HerosHasCharges()
    {
        foreach(HeroAbstract hero in herosAbstract)
        {
            if(hero.GetTurnCharges() > 0)
               return true;
        }
        return false;
    }
    public void EndTurn()
    {
        Debug.Log(whoseTurn.text + " ended");
        playerTurn = !playerTurn;
        NullTargetAndAttacker();
        mouseSelection.ClearLabels();
        UpdateWhoseTurn();
        SetTurnCharges();
    }

    private void UpdateWhoseTurn()
    {
        whoseTurn.text = playerTurn ? "Your Turn" : "Enemy Turn";
        state = playerTurn ? State.WaitingForPlayer : State.WaitingForEnemy;
    }

    public void SetPartTarget(string part)
    {
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
            //attacker = null;
        }
        if(playerTurn)
            CheckAttackAvailability();
    }

     public void SetTarget(GameObject unit)
    {
        target = unit;
        if(state == State.WaitingForPlayer)
        {
            if(playerTurn)
                CheckAttackAvailability();                                
        }
        
    }
    public GameObject GetTarget()
    {
        return target;
    }

    public void NullTargetAndAttacker()
    {
        attacker = null;
        target = null;
    }
#endregion
    
#region updating ui
    private void CheckAttackAvailability()
    {
        //conditions for attacking avaliable target, has charges , player turn
         if(target != null && attacker != null && state == State.WaitingForPlayer)
         {
            bool hasCharges = attacker.GetComponent<IReturnTurnCharges>().GetTurnCharges() > 0;
            if(hasCharges)
            {
                attackButton.interactable = true; 
                //partsHPBars.ClearBars();
                //partsHPBars.SpawnBars(); // need to fix the list opens b4 the damage is done maybe
            }   
            else
            {
                attackButton.interactable = false; 
                partMenu.SetActive(false);
                //partsHPBars.ClearBars();
            }         
         }
         else
         {
            attackButton.interactable = false; 
            partMenu.SetActive(false);
            partsHPBars.ClearBars();
            //disable attack menu
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
