﻿using System;
using System.Collections.Generic;
using System.Collections; //hashtable
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APFebAss
{
    class Env
    {
        Hashtable table;
        Env prev;

       
        public Env(Env p = null)
        {
            table = new Hashtable();
            this.prev = p;
        }

        public void Put(Token t, bool b)
        {
            table.Add(t, b);
            //dump("Adding " + t.ToString());
        }

        public bool get(Token t)
        {
            //dump("Searching " + t.ToString());
            for (Env e = this; e != null; e = e.prev)
            {
                if(e.table.Contains(t))
                    return  (bool)e.table[t];
            }

            //Console.WriteLine("Error: bind for " + t.ToString() + " not found");
            dump("Error: bind for " + t.ToString() + " not found");
            return false;
        }

        public void dump(String msg)
        {
            Console.WriteLine("**** "+msg+". Dumping enviroment ****");
            
            //Console.WriteLine(prev == null ? "No prev env" : "prev env present");
            for (Env e = this; e != null; e = e.prev)
            {
                Console.WriteLine("___________");
                foreach (DictionaryEntry entry in e.table)
                    Console.WriteLine(entry.Key + ":" + entry.Value);
            }
            Console.WriteLine("*********");
        }
    }
}