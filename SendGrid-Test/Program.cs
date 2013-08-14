using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid_Test
{
	class Program
	{
		static void Main(string[] args)
		{
			//Get the items out of the command line
			if (args.Length < 3)
			{
				Console.WriteLine("usage: SendGrid-Test <username> <password> <email address * n>");
				return;
			}

			//First item is the sendgrid username
			string username = args[0];
			
			//second item it the sendgrid password
			string pwd = args[1];

			// Create the email object first, then add the properties.
			var myMessage = SendGrid.GetInstance();

			// Add the message properties.
			myMessage.From = new MailAddress("john@example.com");

			//Get all the rest of the command line arguments as email addresses
			List<string> recipients = new List<string>();
			for (int i = 2; i < args.Length; i++)
			{
				recipients.Add(args[i]);
			}
			myMessage.AddTo(recipients);

			myMessage.Subject = "Testing the SendGrid Library";

			//Add the HTML and Text bodies
			myMessage.Html = "<p>Hello World!</p>";
			myMessage.Text = "Hello World plain text!";

			//myMessage.InitializeFilters(); //this doesnt need to be called
			// Add a footer to the message.
			//myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");

			// true indicates that links in plain text portions of the email should also be overwritten for link tracking purposes. 
			//myMessage.EnableClickTracking(true);

			// Create network credentials to access your SendGrid account.
			var credentials = new NetworkCredential(username, pwd);

			// Create a Web transport for sending email.
			var transportWeb = Web.GetInstance(credentials);

			try
			{
				// Send the email.
				Console.WriteLine("sending email...\n\n");
				transportWeb.Deliver(myMessage);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("buttnuts happened: \n{0}", ex.ToString()));
			}
			
		}
	}
}
