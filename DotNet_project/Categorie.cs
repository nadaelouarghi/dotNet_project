using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_project
{
    public class Categorie
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public Categorie(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    
       
    }
}
