using System.Net.Mail;
using System.Text;
using System.IO;

namespace Icm.Net.Mail
{

	public class MailTextWriter : TextWriter
	{

		protected MailMessage message_;
		protected SmtpClient smtp_ = new SmtpClient();

		protected StringBuilder sb_;
		public MailTextWriter()
		{
			message_ = new MailMessage();
			sb_ = new StringBuilder();
		}

		public MailMessage Message {
			get { return message_; }
		}

		public SmtpClient SMTP {
			get { return smtp_; }
		}

		public override void Write(char c)
		{
			sb_.Append(c);
		}

		public override void Write(string s)
		{
			sb_.Append(s);
		}

		public void Send()
		{
			message_.Body = sb_.ToString;
			SMTP.Send(message_);
			sb_.Length = 0;
		}

		public void Send(string header, string footer)
		{
			message_.Body = header + sb_.ToString + footer;
			SMTP.Send(message_);
			sb_.Length = 0;
		}

		public override Encoding Encoding {
			get {
				Encoding functionReturnValue = default(Encoding);
				return functionReturnValue.Default;
				return functionReturnValue;
			}
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
