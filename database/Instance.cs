using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.database
{
    internal class Instance
    {
        static private Entities _entities;

        public static Entities GetContext()
        {
            if (_entities == null)
                _entities = new Entities();
            return _entities;
        }

        public static bool IsLoginValidated(login login)
        {
            return !String.IsNullOrEmpty(login.login1) && !String.IsNullOrEmpty(login.password);
        }

        public static void Reload() => Instance.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
    }
}
