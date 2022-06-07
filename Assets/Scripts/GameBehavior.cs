using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBehavior : MonoBehaviour
{
   public string labelText = "Собирайте капсулы с энергией и ищите пробой в трубах";
   //public int MaxEnergyCapsule=4;
    
   private int energyAmount = 1000;
   
         public int Energy{
            get {return energyAmount;}
            set{
                 energyAmount=value;
                // Debug.LogFormat("Energy:{0}",_energy);
            }
         }
         GameObject BoostClone;
         public int CountEnergyBoost = 10;
         void Start(){
            BoostClone=GameObject.Find("EnergyBoostItem");
            for (int i = 0;i<CountEnergyBoost;i++)
            {
            var  xRandom =  Random.Range(-23f,23f);
            var  zRandom =  Random.Range(-23f,23f);
            var  yRandom =  Random.Range(1f,49f);
            Instantiate(BoostClone,new Vector3(xRandom,yRandom,zRandom),Quaternion.identity);
            }
         }
         void OnGUI()
         {
            GUI.Box(new Rect(20,20,150,25),"Energy:"+energyAmount);
            GUI.Box(new Rect(Screen.width/2-200,Screen.height-50,400,50),labelText);
         }

}
