using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SubscriptionUserName { get; set; }
    }
}
