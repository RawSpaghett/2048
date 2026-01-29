using TMPro;
using UnityEngine.UI;
using UnityEngine;
//could've been in GameManager but i wanted it to be seperate


public class PrestigeManager : MonoBehaviour
{
   public TMP_Text prestigeText; // to visualize changed
   public int prestige = 0;
   public string key = "Prestige"; // key to access specific data within playerprefs

   void Start()
   {
    LoadPrestige(); // properly loads up prestige
   }

   public void LoadPrestige()
   {
    prestige = PlayerPrefs.GetInt(key); //playerprefs is data saved onto the system
    UpdateUI();
   }

   public void AddPrestige() //adds one
   {
    prestige += 1;
    Save();
   }

   public void UpdateUI(){
    prestigeText.text = "Prestige: "+ prestige.ToString(); //updates prestige counter
   }

    public void Save() //saves prestige
    {
        PlayerPrefs.SetInt(key,prestige);
        PlayerPrefs.Save();
    }
}
