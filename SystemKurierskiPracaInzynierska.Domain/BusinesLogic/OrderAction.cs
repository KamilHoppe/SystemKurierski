using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Operation;

namespace SystemKurierskiPracaInzynierska.Domain.BusinesLogic
{
    public class OrderAction
    {
        public Order GetOrder(int idOrder)
        {  
            OrderOperation getOrder = new OrderOperation();
            return getOrder.getOrder(idOrder);
        }

        public void AddOrder(int idPackage, string EmailReceiver, string EmailSender,string latitude, string longitude,string idCourier,string idDispatcher)
        {

            OrderOperation orderOperation = new OrderOperation();
            orderOperation.AddOrder(idPackage, EmailReceiver, EmailSender,latitude,longitude,idCourier,idDispatcher);

        }


    }
}
