using System.Net.Mail;
using System.Text;
using System.IO;

namespace Icm.Net.Mail
{

	public class MailTextWriter : TextWriter
	{
	    private readonly StringBuilder _sb;

        public MailTextWriter()
		{
			Message = new MailMessage();
			_sb = new StringBuilder();
		}

		public MailMessage Message { get; }

	    public SmtpClient Smtp { get; } = new SmtpClient();

	    public override void Write(char c)
		{
			_sb.Append(c);
		}

		public override void Write(string s)
		{
			_sb.Append(s);
		}

		public void Send()
		{
			Message.Body = _sb.ToString();
			Smtp.Send(Message);
			_sb.Length = 0;
		}

		public void Send(string header, string footer)
		{
			Message.Body = header + _sb + footer;
			Smtp.Send(Message);
			_sb.Length = 0;
		}

		public override Encoding Encoding => Encoding.Default;
	}

}