using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;

namespace Kaizen.EmailService
{
    public partial class EmailSvc : ServiceBase
    {
        public EmailSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 31002);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            while (true)
            {
                const int bytesize = 1024 * 1024;
                string message = null;
                byte[] buffer = new byte[bytesize];
                var sender = listener.AcceptTcpClient();
                sender.GetStream().Read(buffer, 0, bytesize);
                message = cleanMessage(buffer);
                Person person = JsonConvert.DeserializeObject<Person>(message);
                byte[] bytes = Encoding.Unicode.GetBytes("Thank you for your message, " + person.Name);
                sender.GetStream().Write(bytes, 0, bytes.Length);
                sendEmail(person);
            }
        }

        protected override void OnStop()
        {
        }

        private static void sendEmail(Person p)
        {
            try
            {
                // Send an email to user also to notify him of the delivery.  
                using (SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("", "");

                    client.Send(
                        new MailMessage("", p.Email,
                            "Thank you for using the Web Service",
                            string.Format(
    @"Thank you for using our Web Service, {0}.   
  
We have recieved your message, '{1}'.", p.Name, p.Message
                            )
                        )
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string cleanMessage(byte[] bytes)
        {
            string message = Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }

        // Sends the message string using the bytes provided.  
        private static void sendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream  
        }

    }
    class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

}
