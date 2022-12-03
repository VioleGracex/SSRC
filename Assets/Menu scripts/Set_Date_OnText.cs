using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Set_Date_OnText : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetTextToDate();
    }
   
   public void SetTextToDate()
   {
       this.GetComponent<TextMeshProUGUI>().text=System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
       PlayerPrefs.SetString(this.name,System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy"));
       PlayerPrefs.Save();
       //Debug.Log(System.DateTime.Now);
   }
   public void GetTextToDate()
   {  
       this.GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetString(this.name, "saveslots");
   }

}
