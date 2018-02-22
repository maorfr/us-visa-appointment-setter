using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;

namespace Frames.Pages
{
    public class AppointmentPage
    {
        private IWebElement OpenCalendarInput { get { return DriverManager.Driver.FindElement(By.XPath("//a[@href='#select']")); } }

        private IWebElement First { get { return DriverManager.Driver.FindElement(By.XPath("//div[@class='ui-datepicker-group ui-datepicker-group-first']")); } }

        private IEnumerable<IWebElement> FirstAvailableDates { get { return First.FindElements(By.XPath(".//a[@class='ui-state-default' and @href='#']")); } }

        private IWebElement Last { get { return DriverManager.Driver.FindElement(By.XPath("//div[@class='ui-datepicker-group ui-datepicker-group-last']")); } }

        private IEnumerable<IWebElement> LastAvailableDates { get { return Last.FindElements(By.XPath(".//a[@class='ui-state-default' and @href='#']")); } }

        private IWebElement TimeSelection { get { return DriverManager.Driver.FindElement(By.Id("appointments_consulate_appointment_time")); } }

        private IWebElement FirstOption { get { return TimeSelection.FindElement(By.XPath("./option[2]")); } }

        private IWebElement SubmitButton { get { return DriverManager.Driver.FindElement(By.Id("appointments_submit")); } }

        private string GetMonth(string month)
        {
            string text = "January";
            if (month.Equals(text))
            {
                return "01";
            }
            text = "February";
            if (month.Equals(text))
            {
                return "02";
            }
            text = "March";
            if (month.Equals(text))
            {
                return "03";
            }
            text = "April";
            if (month.Equals(text))
            {
                return "04";
            }
            text = "May";
            if (month.Equals(text))
            {
                return "05";
            }
            text = "June";
            if (month.Equals(text))
            {
                return "06";
            }
            text = "July";
            if (month.Equals(text))
            {
                return "07";
            }
            text = "August";
            if (month.Equals(text))
            {
                return "08";
            }
            text = "September";
            if (month.Equals(text))
            {
                return "09";
            }
            text = "October";
            if (month.Equals(text))
            {
                return "10";
            }
            text = "November";
            if (month.Equals(text))
            {
                return "11";
            }
            text = "December";
            if (month.Equals(text))
            {
                return "12";
            }

            throw new Exception("Month not found");
        }
        public string FirstGetDate(string day)
        {
            string month = GetMonth(First.FindElement(By.XPath(".//span[contains(@class, 'ui-datepicker-month')]")).Text);
            string year = First.FindElement(By.XPath(".//span[contains(@class, 'ui-datepicker-year')]")).Text;
            
            return year + month + day;
        }

        public string LastGetDate(string day)
        {
            var month = GetMonth(Last.FindElement(By.XPath(".//span[contains(@class, 'ui-datepicker-month')]")).Text);
            var year = Last.FindElement(By.XPath(".//span[contains(@class, 'ui-datepicker-year')]")).Text;

            return year + month + day; 
        }

        public void FindDate(string afterDate, ref string beforeDateTime)
        {
            DriverManager.Driver.Navigate().Refresh();
            Thread.Sleep(5 * 1000);
            OpenCalendarInput.Click();
            Thread.Sleep(1000);
            var availableDates = FirstAvailableDates;

            foreach (var avaiableDate in availableDates)
            {
                string availableDateText = FirstGetDate(avaiableDate.Text);
                if (afterDate.CompareTo(availableDateText) == -1 && availableDateText.CompareTo(beforeDateTime) == -1)
                {
                    avaiableDate.Click();
                    Thread.Sleep(1000);
                    var element = DriverManager.Driver.FindElement(By.Id("appointments_consulate_appointment_time"));
                    var earliestTime = TimeSelection.Text;
                    element.SendKeys(earliestTime);
                    var currentOption = availableDateText + earliestTime.Replace(":", "");
                    if (currentOption.CompareTo(beforeDateTime) == -1)
                    {
                        SubmitButton.Click();
                        DriverManager.Driver.SwitchTo().Alert().Accept();
                        beforeDateTime = availableDateText + earliestTime.Replace(":", "");
                    }
                    return;
                }
            }

            availableDates = LastAvailableDates;

            foreach (var avaiableDate in availableDates)
            {
                string availableDateText = LastGetDate(avaiableDate.Text);
                if (afterDate.CompareTo(availableDateText) == -1 && availableDateText.CompareTo(beforeDateTime) == -1)
                {
                    avaiableDate.Click();
                    Thread.Sleep(1000);
                    var element = DriverManager.Driver.FindElement(By.Id("appointments_consulate_appointment_time"));
                    var earliestTime = TimeSelection.Text;
                    element.SendKeys(earliestTime);
                    var currentOption = availableDateText + earliestTime.Replace(":", "");
                    if (currentOption.CompareTo(beforeDateTime) == -1)
                    {
                        SubmitButton.Click();
                        DriverManager.Driver.SwitchTo().Alert().Accept();
                        beforeDateTime = availableDateText + earliestTime.Replace(":", "");
                    }
                    return;
                }
            }
        }
    }
}
