using application.Models;

namespace application.Services
{
    public class SubscriptionService: Interfaces.Services.ISubscriptionService
    {
        public IEnumerable<Subscription> GetSubscriptions()
        {
            var subscriptions = new List<Subscription>()
            {
                new Subscription() { Name = "test1", Icon = "folder" },
                new Subscription() { Name = "test2", Icon = "folder" },
                new Subscription() { Name = "test3", Icon = "folder" },
                new Subscription() { Name = "test4", Icon = "folder" },
                new Subscription() { Name = "test5", Icon = "folder" },
                new Subscription() { Name = "test6", Icon = "folder" },
                new Subscription() { Name = "test7", Icon = "folder" },
                new Subscription() { Name = "test8", Icon = "folder" },
                new Subscription() { Name = "test9", Icon = "folder" },
                new Subscription() { Name = "test10", Icon = "folder" },
                new Subscription() { Name = "test11", Icon = "folder" },
                new Subscription() { Name = "test12", Icon = "folder" },
                new Subscription() { Name = "test13", Icon = "folder" },
                new Subscription() { Name = "test14", Icon = "folder" },
                new Subscription() { Name = "test15", Icon = "folder" },
                new Subscription() { Name = "test16", Icon = "folder" },
                new Subscription() { Name = "test17", Icon = "folder" },
                new Subscription() { Name = "test18", Icon = "folder" },
                new Subscription() { Name = "test19", Icon = "folder" },
                new Subscription() { Name = "test20", Icon = "folder" },
            };
            return subscriptions;
        }
    }
}
