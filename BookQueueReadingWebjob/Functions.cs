using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace BookQueueReadingWebjob
{

    public class Books
    {
        //public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
    }

    public class Functions
    {
       
        public void ProcessQueueMessage([ServiceBusTrigger("%QueueName%")] BrokeredMessage brokeredMessage)
        {
            try
            {
                var bodyJson = new StreamReader(brokeredMessage.GetBody<Stream>(), Encoding.UTF8).ReadToEnd();
                Books b = JsonConvert.DeserializeObject<Books>(bodyJson);

                Console.WriteLine(bodyJson);
            }
            catch (Exception ex)
            {
                throw;
            }

            brokeredMessage.Complete();
        }
    }
}
