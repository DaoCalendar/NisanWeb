using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Customer
    {
        public Customer(string name)
            : base()
        {
            this.typeField = Role.Customer;
            this.Name = name;
            System.Diagnostics.Debug.WriteLine("-- Customer --");
        }
        //TODO: Make order
        public bool MakeOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public void Pay(Order order)
        {
            throw new NotImplementedException();
        }
        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}