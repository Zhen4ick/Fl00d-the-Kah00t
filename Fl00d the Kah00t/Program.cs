using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace Fl00d_the_Kah00t
{
    class Program
    {
       
        static IWebDriver Browser;
        static int pin;
        static string nick;
        static int counter = 1;
        static int timeSpan;

        static void Main(string[] args)
        {
            Console.WriteLine("Write the pin");
            while (true)
            {
                try
                {
                    pin = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, write only numbers");
                }
            }
            
            Console.WriteLine("Write the nick");
            nick = Console.ReadLine();
            Console.WriteLine("Write timespan(seconds)");
            while (true)
            {
                try
                {
                    timeSpan = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, write only numbers");
                }
            }            
            Fl00d();
            Console.ReadLine();
        }

        static void writeNick(IWebElement gameNick,WebDriverWait ww)
        {          
            gameNick.SendKeys(Keys.LeftControl+"a"+Keys.Delete);//Clear the inputSession
            gameNick.SendKeys($"{nick} {counter++.ToString()}" + OpenQA.Selenium.Keys.Enter);

        }
        static void writePIN(IWebElement gamePin,WebDriverWait ww)
        {           
            gamePin.SendKeys(Keys.LeftControl + "a" + Keys.Delete);//Clear the input named username
            gamePin.SendKeys(pin + OpenQA.Selenium.Keys.Enter);
        }
        static void Fl00d()
        {
            
            while (true)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("headless");//Turn on headless mode
                Browser = new OpenQA.Selenium.Chrome.ChromeDriver(chromeOptions);
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());                
                Browser.Navigate().GoToUrl("https://kahoot.it");
                WebDriverWait ww = new WebDriverWait(Browser, TimeSpan.FromSeconds(1));
                IWebElement gamePin;
                IWebElement gameNick;
                

                while (true)
                {
                    try
                    {
                        writePIN(gamePin= ww.Until(ExpectedConditions.ElementIsVisible(By.Id("inputSession"))), ww);                     
                        break;
                    }
                    catch (NoSuchElementException)
                    {
                        writeNick(gameNick = ww.Until(ExpectedConditions.ElementIsVisible(By.Id("username"))), ww);
                    }
                    catch (WebDriverTimeoutException)
                    {
                        writeNick(gameNick = ww.Until(ExpectedConditions.ElementIsVisible(By.Id("username"))), ww);
                    }
                }
                while (true)
                {
                    try
                    {
                        writeNick(gameNick = ww.Until(ExpectedConditions.ElementIsVisible(By.Id("username"))), ww);
                        break;

                    }
                    catch (NoSuchElementException)
                    {
                        writePIN(gamePin = ww.Until(ExpectedConditions.ElementIsVisible(By.Id("inputSession"))), ww);
                    }
                    catch (WebDriverTimeoutException)
                    {
                        writePIN(gamePin = ww.Until(ExpectedConditions.ElementIsVisible(By.Id("inputSession"))), ww);
                    }
                }
                ((IJavaScriptExecutor)Browser).ExecuteScript("window.open();");
            }



        }
    }
}
