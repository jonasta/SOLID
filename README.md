# SOLID PRINCIPLES
## Single Responsibility Principle (SRP) 
A class should do one thing and one thing only

<details>
<summary>Bad Example</summary>    
  
```cs
  
```  
  
</details> 

<details>
<summary>Solution</summary>
  
```cs

```
  
</details> 
	
## Open-Closed Principle (OCP)
A class should be open to extension but closed to modification
<details>
<summary>Bad Example</summary>

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
</details> 
 
<details>
<summary>Solution</summary>

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
</details> 


Extension Techniques
* inheritance
* interface inheritance
* abstract methods (must override)
* virtual method
* extension method

## Liskov Substitution Principle (LSP)
You should be able to replace a class with a subclass without the calling code knowing about the change

<details>
<summary>Bad Example</summary>    
  
```cs
  
```  
  
</details> 

<details>
<summary>Solution</summary>
  
```cs

```
  
</details> 
	
