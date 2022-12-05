using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelection : MonoBehaviour
{   
    [SerializeField]
    Camera cam;
    public BattleHandler battleHandler;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        battleHandler = GameObject.FindObjectOfType<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && battleHandler.state == BattleHandler.State.WaitingForPlayer)
        { 
            Debug.Log("searching");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider!=null)
            {
                Debug.Log("found");
                if(hit.transform.gameObject.tag == "Hero")
                {
                    battleHandler.SetAttacker(hit.transform.gameObject);
                }
                else if(hit.transform.gameObject.tag == "Enemy")
                {
                    battleHandler.SetTarget(hit.transform.gameObject);
                    /* targetName.text = enemyTarget.GetComponent<Unit>().unitName;
                    enemyTarget.GetComponent<Unit>().nameUnitLabel.SetActive(true);
                    DisableOtherLabelsEnemies();
                    RefreshHealth();
                    RefreshArmor(); */
                }
                
            }
        } 
    }
}
