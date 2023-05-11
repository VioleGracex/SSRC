using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupPanels : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myPages;
    [SerializeField]
    GameObject activePage;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform page in this.transform)
        {
            myPages.Add(page.gameObject);
        }
    }

   public void EnableNewPage(GameObject newPage)
   {    
        if(activePage == null)
        {
            newPage.SetActive(true);
            activePage = newPage;
            return;
        }
        else if(newPage == activePage)
        {
            newPage.SetActive(false);
            activePage = null;
            return;
        }
        activePage.SetActive(false);
        newPage.SetActive(true);
        activePage = newPage;
   }
}
