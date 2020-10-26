using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using AventStack.ExtentReports.Model;
using TestAttribute = NUnit.Framework.TestAttribute;


namespace eNotaryLog
{
    [TestFixture]
    public class eNotaryTest
    {
        IWebDriver driver = new ChromeDriver();
        string pageTitle = null;
        /// <summary>
        /// Test Repository clone.
        /// </summary>
        [Test,Order(1)]
        [Obsolete]
        public void VerifyMainPageLaunch()
        {
                               
           
            try
            {
                driver.Url = ("https://enotarylog.com/");

                pageTitle = "Your Trusted Online Notary";

                WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.TitleContains(pageTitle));             
       
                Assert.AreEqual(driver.Title, "Your Trusted Online Notary");

                //driver.Close();
            }
            catch (Exception e)
            {
                pageTitle = driver.Title.ToString();
                Console.Write("The test step Fail and the actual title is: " + pageTitle);
                driver.Close();

                throw (e);              
            }           
        }

        [Test,Order(2)]
        public void VerifyEquipmentsPageLaunch()
        {
            //WaitTime(8000);
            IWebElement webElement;
            //driver.Url = ("https://enotarylog.com/");
            WaitTime(5000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);

                
            driver.Manage().Window.Maximize();

            WaitTime(5000);
            //Find Notarize button
            webElement = driver.FindElement(By.XPath("//*[@id='navbar-menu']/ul/li[5]/a"));
            bool foundElement = webElement.Displayed;

            try
            {
                Assert.IsTrue(foundElement);
                var elementType = webElement.GetType();
                webElement.Click();
            }
            catch(Exception e)
            {
                Console.Write("Element not found");
                throw (e);
            }
            WaitTime(8000);
            //Capture and verify the texts from the notarize page
            string expectedString = "Let's begin your online notarization";
            webElement = driver.FindElement(By.XPath("//*[@id='dashboard']/section/div/div[1]/div/div/h1"));
            foundElement = webElement.Displayed;

            //Verify checking equipments page lauch correctly 
            Assert.That(webElement.Text.ToUpper(), Is.EqualTo(expectedString.ToUpper()));
        }

        [Test, Order(3)]
        [Obsolete]
        public void VerifyCustomerEnteredInfo()
        {
        
            IWebElement webElement;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Uncomment the lines below for running Debug
            /*driver.Url = ("https://enotarylog.com/notarization");
            WaitTime(5000);
            */driver.Manage().Window.Maximize();

            //===========Comment these line below for before running Debug
            WaitTime(5000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Manage().Window.Maximize();
            WaitTime(5000);
            //============================================================


            //Find Get Started button then click
            webElement = driver.FindElement(By.XPath("//*[@id='get-started-undetermined']"));
            //webElement = driver.FindElement(By.Id("get - started - undetermined"));
            bool foundElement = webElement.Displayed;
            webElement.Click();

            var handles = driver.WindowHandles;

            driver.SwitchTo().ActiveElement();
            WaitTime(5000);
            webElement = driver.FindElement(By.XPath("//*[@id='modal-info-1']"));

            webElement = driver.FindElement(By.XPath("//*[@id='dashboard']"));

            //driver.SwitchTo().Frame(webElement);


            //webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']"));
            WaitTime(8000);

            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/h2"));
            foundElement = webElement.Displayed;
            string expectedString = "Primary Signer";
            //Verify Sign in mordal is displayed 
            Assert.That(webElement.Text.ToUpper(), Is.EqualTo(expectedString.ToUpper()));

            //Enter First Name field
            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/form/div[1]/div/div/input"));
            webElement.SendKeys("FirstNameT");

            //Enter Midle Name //*[@id="modal-start-now-1"]/div/div/div/form/div[2]/div/div/input
            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/form/div[2]/div/div/input"));
            webElement.SendKeys("MidNameT");

            //Enter Last Name
            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/form/div[3]/div/div/input"));
            webElement.SendKeys("LastNameT");

            //Enter E-Mail
            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/form/div[4]/div/div/input"));
            webElement.SendKeys("bly1201@mailinator.com");

            //*[@id="modal-start-now-1"]/div/div/div/form/div[5]/div/div/button

            webElement = driver.FindElement(By.XPath("//*[@id='modal-start-now-1']/div/div/div/form/div[5]/div/div/button"));

            webElement.Submit();

            //Verify registered user
            WaitTime(8000);
            expectedString = "bly1201@mailinator.com";
            webElement = driver.FindElement(By.XPath("//*[@id='dashboard']/section/div/div[3]/div/div/div[1]/div/div/div[4]"));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='dashboard']/section/div/div[3]/div/div/div[1]/div/div/div[4]")));

            Assert.That(webElement.Text.ToUpper(), Is.EqualTo(expectedString.ToUpper()));


            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='next-button']")));
            webElement = driver.FindElement(By.XPath("//*[@id='next-button']"));
            bool isElementEnabled = webElement.Enabled;
            if (isElementEnabled)
            {
                webElement.Click();
            }
            else
            {
                Console.WriteLine("The Next button is not Enabled: " + (webElement.Text).ToUpper());
                driver.Quit();

            }

            //<h1 aria-label="email confirmation title">Email verification pending</h1>
            expectedString = "Email verification pending";
            //Wait for page "Email Verification pending" to load
            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='dashboard']/section/div/div/div/div/h1")));

            WaitTime(8000);
            //Get the text and verify the page is loaded.
            webElement = driver.FindElement(By.XPath("//*[@id='dashboard']/section/div/div/div/div/h1"));
            expectedString = "Email verification pending";
            Assert.That(webElement.Text.ToUpper(), Is.EqualTo(expectedString.ToUpper()));

            webElement = driver.FindElement(By.XPath("//*[@id='dashboard']/section/div/div/div/div/button"));
            isElementEnabled = webElement.Enabled;
            if (isElementEnabled)
            {
                webElement.Click();
            }
            else
            {
                Console.WriteLine("The Next button is not Enabled: " + (webElement.Text).ToUpper());
                driver.Close();

            }
            
        }

        //===============================================================================
        //============================= CHECK EQUIPMENT =================================
        //===============================================================================

        [Test, Order(4)]
        [Obsolete]
        public void CheckMailAndEquipments()
        {
            IWebElement webElement;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //driver.Url = ("https://www.mailinator.com/v3/index.jsp?zone=public&query=bly1201#/#inboxpane");
            //WaitTime(2000);
            // driver.Manage().Window.Maximize();

            //===============================================================================
            //============================= CHECK EQUIPMENT =================================
            //===============================================================================
            driver.Url = ("https://www.mailinator.com/v3/index.jsp?zone=public&query=bly1201#/#inboxpane");
            WaitTime(5000);
            driver.Manage().Window.Maximize();
            //< td class="ng-binding" onclick="showTheMessage('bly1201-1603435725-88236');" onkeyup="if (event.keyCode == 13) { showTheMessage('bly1201-1603435725-88236'); }" style="width:0%;overflow: hidden;white-space: nowrap;box-sizing: border-box;">
            //
            //</td>
            //< td class="ng-binding" onclick="showTheMessage('bly1201-1603435725-88236');" onkeyup="if (event.keyCode == 13) { showTheMessage('bly1201-1603435725-88236'); }" style="width:0%;overflow: hidden;white-space: nowrap;box-sizing: border-box;">
            //
            //</td>

            //Find Received mail and click open
            WaitTime(10000);
            var xwebElement = driver.FindElements(By.CssSelector("td[class='ng-binding']"));
            Actions actions = new Actions(driver);
            actions.Click(xwebElement[1]);
            actions.Perform();

            //Find iFrame and switch to iFrame msg_body
            WaitTime(5000);
            webElement = driver.FindElement(By.Id("msg_body"));
            driver.SwitchTo().Frame(webElement);

            //Find CONTINUE button in iFrame and click continue
            WaitTime(5000);
            webElement = driver.FindElement(By.LinkText("CONTINUE"));
            webElement.SendKeys("CONTINUE");
            webElement.SendKeys(Keys.Enter);

            //Switch to active web tab
            int windowCount = driver.WindowHandles.Count();
            //driver.SwitchTo().Window(driver.WindowHandles[0]);
            //Maker sure to focus on test equipment browser tab
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            
            //Wait for Equipments windows to showup
            WaitTime(20000);
            xwebElement = driver.FindElements(By.CssSelector("a[aria-selected]"));
            WaitTime(3000);
            //Check Browser
            xwebElement[0].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();

            //Check Device
            xwebElement[1].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();
            WaitTime(3000);
            //Check Audio
            xwebElement[2].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            //Select the speaker
            webElement = driver.FindElement(By.CssSelector("select[class='form-control']")); //<select class="form -control"><option>Speaker 1</option></select>
            SelectElement selectElement = new SelectElement(webElement);
            webElement.Click();
            //selectElement.SelectByText("Speaker 1");
            selectElement.SelectByIndex(0);
            webElement.Click();
            WaitTime(5000);
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();
            WaitTime(5000);
            xwebElement[3].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();
            WaitTime(5000);
            xwebElement[4].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();
            WaitTime(50000);
            xwebElement[5].Click();
            //< button type = "button" class="btn btn-primary">Start</button>
            //Find Start button
            webElement = driver.FindElement(By.CssSelector("button[type='button']"));
            webElement.Click();
            WaitTime(50000);
        }

        private string IsEqal(string v)
        {
            throw new NotImplementedException();
        }

        public void WaitTime(int varTimer)
        {
            Thread.Sleep(varTimer);
        }

        public void TakeScreenShot(IWebDriver driver, IWebElement webElement)
        {
            //=============== Take Element Screen shot and save to a file=======
            //==================================================================
            string fileName = @"C:\DevEnvironment\Screenshots\ScreenShot.png";

            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            var cropArea = new Rectangle(webElement.Location, webElement.Size);
            var bitmap = bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);
            bitmap.Save(fileName);
            //=================================================================
            //================= End taking emlement screen shot ===============
        }
    }
}
