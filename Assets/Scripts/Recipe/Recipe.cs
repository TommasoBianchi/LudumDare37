using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {

    public WeaponData weaponData;
    public List<KeyValuePair<ResourceType, int>> resources;

    public Recipe(WeaponData wd, List<KeyValuePair<ResourceType, int>> r) {
        this.weaponData = wd;
        this.resources = r;
    }

    /*
    public List<TypeArmi> createListArmiRecipe() {
        Random random = new Random();

        int baseMateriali = 0;
        int tier = 5;
        int numTypeMateriali = 6;
        int numTypeArmi = 4;

        WeaponType[] wType = new WeaponType[numTypeArmi];
        wType[0] = WeaponType.Sword;
        wType[1] = WeaponType.PickAxe;
        wType[2] = WeaponType.Lance;
        wType[3] = WeaponType.Hammer;


        ResourceType[] rType = new ResourceType[numTypeMateriali];
        rType[0] = ResourceType.Wood;
        rType[1] = ResourceType.Rock;
        rType[2] = ResourceType.Bronze;
        rType[3] = ResourceType.Iron;
        rType[4] = ResourceType.Adamantium;
        rType[5] = ResourceType.Cryptonite;

        List<TypeArmi> armi = new List<TypeArmi>();
        List<Combinazioni> combinazioni = new List<Combinazioni>();
        
        int cont=0;
        for(int i=1;i<=tier;i++){
            baseMateriali = baseMateriali+25;
            for(int j=0;j<numTypeArmi;j++){
                //System.out.println("tier: "+i);
                TypeArmi arma = new TypeArmi(wType[j], i);
                Combinazioni comb = arma.setMateriale(rType,i+1, baseMateriali, combinazioni);
                combinazioni.Add(comb);
                armi.Add(arma);
                cont++;
            }
        }
        return armi;
    }

    //Costruttore delle ricette, setta 
    public Recipe() {

        List<TypeArmi> armi = createListArmiRecipe();
        foreach (TypeArmi temp in armi) {
            weapons.Add(new Weapon(temp.tipoArma, temp.tier));
            foreach (TypeMateriali mTemp in temp.materiali) {
                resources.Add(new KeyValuePair<ResourceType, int>(mTemp.typeMateriale, mTemp.num));
            }
        }
    
    }*/
     /* Questo pezzo di codice è per accedere ai dati delle classi
      * foreach(TypeArmi temp in *nome della lista){
            temp.tier;
            temp.tipoArma;
            foreach(TypeMateriali matTemp in temp.materiali){
                matTemp.typeMateriale
                matTemp.num
            }
            
        }*/

   
}
