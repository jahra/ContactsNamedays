using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsNamedays
{
    class UrlResolver
    {
        const string ADDRESS = @"http://svatky.adresa.info/txt?lang=cz&";

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                value = value.ToLower();
                name = char.ToUpper(value[0]) + value.Substring(1);
            }
        }

        public UrlResolver() { }
        public UrlResolver(string n)
        {
            Name = n;           
        }

        public string GetAdress()
        {
            return ADDRESS + "name=" + name;
        }
    }
}
