using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PersonDAO_Mock: IPersonDAO
    {
        private static PersonDAO_Mock instance = null;
        private List<Person>_persons;

        public static IPersonDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new PersonDAO_Mock();
            }
            return instance;
        }

        private PersonDAO_Mock()
        {
            _persons = new List<Person>();
            _persons.Add(new Person(1, "Василий", "Фудоров", 33));
            _persons.Add(new Person(2, "Касилий", "Пудоров", 25));
            _persons.Add(new Person(3, "Пасилий", "Еудоров", 14));
            _persons.Add(new Person(4, "Сасилий", "Рудоров", 74));
            _persons.Add(new Person(5, "Тасилий", "Кудоров", 11));
            _persons.Add(new Person(6, "Дасилий", "Судоров", 15));
            _persons.Add(new Person(7, "Часилий", "Зудоров", 20));
            _persons.Add(new Person(8, "Игнат", "Иудоров", 30));
            _persons.Add(new Person(9, "Карен", "Яудоров", 23));
        }

        IPersonDAO IPersonDAO.GetInstance()
        {
            return GetInstance();
        }
    
        public void Create(Person p)
        {
            int id = _persons.Count == 0 ? 0 : _persons.Max(item => item.ID);
            p.ID = id + 1;
            _persons.Add(p);
        }

        public List<Person> Read()
        {
            return _persons.ToList();
        }

        public void Update(Person p)
        {
            Person person = _persons.FirstOrDefault(item => item.ID == p.ID);
            if (person != null)
            {
                person.Age = p.Age;
                person.FirstName = p.FirstName;
                person.LastName = p.LastName;
            }
        }

        public void Delete(Person p)
        {
            Person person = _persons.FirstOrDefault(item => item.ID == p.ID);
            if (person != null)
            {
                int index = _persons.IndexOf(person);
                _persons.RemoveAt(index);
            }
        }
    }
}
