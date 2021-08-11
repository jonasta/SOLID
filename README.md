# SOLID PRINCIPLES
## Single Responsibility Principle (SRP) 
A class should do one thing and one thing only

#### Bad Example

```cs
public class UserService  
{  
   public void Register(string email, string password)  
   {  
      if (!ValidateEmail(email))  
         throw new ValidationException("Email is not an email");  
         var user = new User(email, password);  
  
         SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject="HEllo foo" });  
   }
   public virtual bool ValidateEmail(string email)  
   {  
     return email.Contains("@");  
   }  
   public bool SendEmail(MailMessage message)  
   {  
     _smtpClient.Send(message);  
   }  
} 
```

#### Solution
  
```cs
public class UserService  
{  
   EmailService _emailService;  
   DbContext _dbContext;  
   public UserService(EmailService aEmailService, DbContext aDbContext)  
   {  
      _emailService = aEmailService;  
      _dbContext = aDbContext;  
   }  
   public void Register(string email, string password)  
   {  
      if (!_emailService.ValidateEmail(email))  
         throw new ValidationException("Email is not an email");  
         var user = new User(email, password);  
         _dbContext.Save(user);  
         emailService.SendEmail(new MailMessage("myname@mydomain.com", email) {Subject="Hi. How are you!"});  
  
      }  
   }  
   
public class EmailService  
{  
   SmtpClient _smtpClient;  
    
   public EmailService(SmtpClient aSmtpClient)  => _smtpClient = aSmtpClient;  
   
   public bool virtual ValidateEmail(string email)  
   {  
      return email.Contains("@");  
   }  
   public bool SendEmail(MailMessage message)  
   {  
      _smtpClient.Send(message);  
   }  
}   
```

	
## Open-Closed Principle (OCP)
A class should be open to extension but closed to modification

#### Bad Example

```cs
  public class ErrorLogger
{
    private readonly string _whereToLog;
    public ErrorLogger(string whereToLog)
    {
        this._whereToLog = whereToLog.ToUpper();
    }
 
    public void LogError(string message)
    {
        switch (_whereToLog)
        {
            case "TEXTFILE":
                WriteTextFile(message);
                break;
            case "EVENTLOG":
                WriteEventLog(message);
                break;
            default:
                throw new Exception("Unable to log error");
        }
    }
 
    private void WriteTextFile(string message)
    {
        System.IO.File.WriteAllText(@"C:\Users\Public\LogFolder\Errors.txt", message);
    }
 
    private void WriteEventLog(string message)
    {
        string source = "DNC Magazine";
        string log = "Application";
         
        if (!EventLog.SourceExists(source))
        {
            EventLog.CreateEventSource(source, log);
        }
        EventLog.WriteEntry(source, message, EventLogEntryType.Error, 1);
    }
}
```

#### Solution

```cs
public interface IErrorLogger
{
    void LogError(string message);
}
 
public class TextFileErrorLogger : IErrorLogger
{
    public void LogError(string message)
    {
        System.IO.File.WriteAllText(@"C:\Users\Public\LogFolder\Errors.txt", message);
    }
}
 
public class EventLogErrorLogger : IErrorLogger
{
    public void LogError(string message)
    {
        string source = "DNC Magazine";
        string log = "Application";
 
        if (!EventLog.SourceExists(source))
        {
            EventLog.CreateEventSource(source, log);
        }
 
        EventLog.WriteEntry(source, message, EventLogEntryType.Error, 1);
    }
}
```



Extension Techniques
* inheritance
* interface inheritance
* abstract methods (must override)
* virtual method
* extension method

## Liskov Substitution Principle (LSP)
You should be able to replace a class with a subclass without the calling code knowing about the change

#### Bad Example
  
```cs
  
```  
  
#### Solution
  
```cs

```
