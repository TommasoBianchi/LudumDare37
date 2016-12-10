using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeArmi {
    string tipoArma;
    TypeMateriali[] materiali;
    int tier;
    System.Random random = new System.Random();
    
    public TypeArmi(string tipoArma, int tier){
        this.tipoArma = tipoArma;
       // System.out.println("tier in construct: "+tier);
        this.tier = tier;
        materiali = new TypeMateriali[tier+1];
    }

    public Combinazioni setMateriale(string[] colori, int numColori, int num, List<Combinazioni> combinazioni)
    {
       // System.out.println("Num totali: "+num);
        int rand;
        bool res = true;
        Combinazioni comb = null;
        while(res){
            for(int i=0;i<numColori;i++){
                if(i<numColori-1){
                    rand = random.Next(0, num);
                    TypeMateriali materiale = new TypeMateriali(colori[i], rand);
                   // System.out.println("Tipo Arma: "+this.tipoArma+ " colore: "+ materiale.typeMateriale + " num: "+materiale.num);
                    materiali[i] = materiale;
                    num = num - rand;
                   // System.out.println(num);
                }
                else{
                    TypeMateriali materiale = new TypeMateriali(colori[i],num);
                    materiali[i] = materiale;
                   // System.out.println("Tipo Arma: "+this.tipoArma+ " colore: "+ materiale.typeMateriale + " num: "+materiale.num);
                }
            }
            comb = new Combinazioni(materiali);
            bool exist = false;
            if(combinazioni.Count == 0) res=false;
            
            else {
                foreach(Combinazioni temp in combinazioni){
                    if(comb.Equals(temp)) exist = true;
               
                }
            if(!exist) res=false;
            }
            
        }
       return comb;
    }
}
