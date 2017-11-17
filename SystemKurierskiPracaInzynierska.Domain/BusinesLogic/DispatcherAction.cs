using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Operation;

namespace SystemKurierskiPracaInzynierska.Domain.BusinesLogic
{
    public class DispatcherAction
    {
        public bool AddPerson(Person person) //Add Person to DispatcherController to database table Person
        {
            if (person.Name == "")
            {
                return false;
            }
            else
            {
                DispatcherOperation dispatcherOperation = new DispatcherOperation();
                dispatcherOperation.SavePerson(person);
                return true;
            }
        }     
        public City NameCity (int idCity)
        {
            DispatcherOperation dispatcherOperation = new DispatcherOperation();
            return dispatcherOperation.getCityName(idCity);

        }
    

    }
}
