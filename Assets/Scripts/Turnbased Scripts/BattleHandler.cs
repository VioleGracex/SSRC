using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{
    private static BattleHandler instance;
    public State state;
    public static BattleHandler Getinstance()
    {
        return instance;
    }

    [SerializeField]
    TextMeshProUGUI whoseTurn, partTargetText;
    [SerializeField]
    Transform[] heroLocations, enemyLocations;
    [SerializeField]  // instead of serialize use instantiate and save them don't forget order in layer and it's child
    public List<Transform> heroes, enemies;

    [SerializeField]
    GameObject attacker,target;
    
    [SerializeField] float slideSpeed = 10f;
    public bool playerTurn = true, playerStart = true; // get if he successed in miniGame fast click if yes he gets to start else he got ambushed 
    public enum State
    {
        WaitingForPlayer,
        Busy,
        Attacking,
        SlidingForward,
        SlidingBackward,
        Fleeing,
    }
    private void Awake()
    {
        instance = this;
        if (playerStart)
        {
            playerTurn = true;
            SetTurnCharges();
        } 
        
    }

    public void MovementTranslation(Transform attacker, Vector3 target)
    {
        attacker.position = Vector3.MoveTowards(attacker.position, target, slideSpeed * Time.deltaTime);        
    }
    public void AttackEnemy()
    {

        if (attacker != null && target != null)
        {
            if (attacker.GetComponent<HeroAbstract>().myStats.turnCharges <= 0)
            {
                Debug.Log("No Charges");
                return;
            }
            state = State.SlidingForward;  
        }
    }

    public void AttackPlayer(GameObject attacker, GameObject attackTarget)
    {
        if (attacker != null && attackTarget != null)
        {
            Debug.Log(attacker.GetComponent<EnemyAbstract>().myStats.turnCharges);
            if (attacker.GetComponent<EnemyAbstract>().myStats.turnCharges <= 0)
            {
                Debug.Log("No Charges");
                return;
            }
            if (attacker.transform.position.x > attacker.transform.position.x)
            {
                attacker.transform.position = attackTarget.transform.position - new Vector3(2, 0, 0);
            }
            else
            {
                attacker.transform.position = attackTarget.transform.position - new Vector3(-2, 0, 0);
            }
            //attackTarget
        }
    }

    public void FlipHeroes()
    {
        state = State.Fleeing;
        foreach (Transform hero in heroes)
        {
            hero.rotation = Quaternion.Euler(0, 180, 0);
        }
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
    IEnumerator SlideToTargetPlayer(GameObject attacker, GameObject target)
    {

        if (state == State.SlidingForward && target.gameObject != null)
        {
            //attacker.GetComponent<Unit>().myAnimator.Play("Walking");
            Vector3 temp = target.transform.position + target.transform.right * (-4f);
            MovementTranslation(attacker.transform, temp);
            Debug.Log(Mathf.Abs(attacker.transform.position.x - temp.x) < 1f);
            yield return new WaitUntil(() => Mathf.Abs(attacker.transform.position.x - temp.x) < 1f);
            //state = State.Busy;
        }
        //attack here
        if(target.gameObject != null)
        {
            state = State.SlidingBackward;
            StopCoroutine(SlideToTargetPlayer(attacker, target));
        } 
       
        yield return new WaitForSeconds(2f);
        //here fix
    }

    IEnumerator SlideToPlacePlayer(GameObject attacker)
    {
        //GameObject attacker = this.GetComponent<MouseSelection>().playerTarget;
        if (state == State.SlidingBackward)
        {
            //attacker.GetComponent<Unit>().myAnimator.Play("Walking");
            Vector3 temp =new Vector3(0,0,0);
            if(playerTurn)
            {
                temp = attacker.GetComponent<HeroAbstract>().myStats.myPosition;
            }
            else
            {
                temp = attacker.GetComponent<EnemyAbstract>().myStats.myPosition;
            }
           
            MovementTranslation(attacker.transform, temp);
            yield return new WaitUntil(() => Mathf.Abs(temp.x - attacker.transform.position.x) < 1f);
        }

        state = State.WaitingForPlayer;
        //attacker.GetComponent<Unit>().myAnimator.Play("Idle");

        yield return null;
    }

   
    void Update()
    {
        Debug.Log(state);
        switch (state)
        {
            case State.WaitingForPlayer:
                StopAllCoroutines();
                //enable player actions
                break;
            case State.SlidingForward:
                StartCoroutine(SlideToTargetPlayer(attacker, target));
                break;
            case State.SlidingBackward:
                StopCoroutine(SlideToTargetPlayer(attacker, target));
                StartCoroutine(SlideToPlacePlayer(attacker));
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

    public void SetAttacker(GameObject unit)
    {
        attacker = unit;
    }

     public void SetTarget(GameObject unit)
    {
        target = unit;
    }


}
