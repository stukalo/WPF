using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPersonDAO
    {
        IPersonDAO GetInstance();
        void Create(Person p);
        List<Person> Read();
        void Update(Person p);
        void Delete(Person p);
    }
}
