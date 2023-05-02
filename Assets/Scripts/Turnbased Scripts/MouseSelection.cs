using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MouseSelection : MonoBehaviour
{   
    [SerializeField]
    Camera cam;
    [SerializeField]
    BattleHandler battleHandler;

    [SerializeField]
    Transform heroLabel, enemyTargetLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        battleHandler = GameObject.FindObjectOfType<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //change this to event later
        if (Input.GetMouseButtonDown(0) && battleHandler.state == BattleHandler.State.WaitingForPlayer)
        { 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider!=null)
            {
                if(hit.transform.gameObject.tag == "Hero")
                {
                    battleHandler.SetAttacker(hit.transform.gameObject);
                    LocateTargetFollow(heroLabel,hit.transform);
                    heroLabel.GetChild(0).GetComponent<TextMeshProUGUI>().text = hit.transform.GetComponent<HeroAbstract>().myStats.unitName;
                }
                else if(hit.transform.gameObject.tag == "Enemy")
                {
                    battleHandler.SetTarget(hit.transform.gameObject);
                    LocateTargetFollow(enemyTargetLabel,hit.transform);
                }
                
            }
        } 
    }

    public void LocateTargetFollow(Transform myUI,Transform target)
    {
        myUI.position = target.position + new Vector3(0f, 4f, 0f);//Camera.main.WorldToViewportPoint(target.position); 
        myUI.gameObject.SetActive(true);
    }


}
