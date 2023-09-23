using WongmaneeB_QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WongmaneeB_QueryBuilder.Models
{
    public class Pokemon : IClassModel
    {
        public int Id { get; set; }
        public int DexNumber { get; set; }
        public string Name { get; set; }
        public string Form { get; set; } = string.Empty;
        public string Type1 { get; set; }
        public string Type2 { get; set; } = string.Empty;
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Generation { get; set; }


        
        // ======================== PARAMETERIZED CONSTRUCTOR ======================== //
        public Pokemon(int id, int dexNumber, string name, string form, string type1, string type2,
            int hp, int attack, int defense, int spAtk, int spDef, int speed, int generation)
        {
            this.Id = id;
            this.DexNumber = dexNumber;
            this.Name = name;
            this.Form = form;
            this.Type1 = type1;
            this.Type2 = type2;
            this.HP = hp;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = spAtk;
            this.SpecialDefense = spDef;
            this.Speed = speed;
            this.Generation = generation;
        }
    }
}
