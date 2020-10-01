using System;
using System.Collections.Generic;
using SeleniumProject.Utilities;
using OpenQA.Selenium;
using log4net;
using System.Threading;

namespace SeleniumProject.Function
{
	public class Script : ScriptingInterface.IScript
	{		
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public void Execute(DriverManager driver, TestStep step)
		{
			long order = step.Order;
			string wait = step.Wait != null ? step.Wait : "";
			List<TestStep> steps = new List<TestStep>();
			IWebElement ele;
			string data = "";
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver.GetDriver();
			VerifyError err = new VerifyError();
			bool numeric = false;
			int number = 0;
			
			if (step.Name.Equals("Verify Event Odds Details by Number")) {
				bool numeric = int.TryParse(step.Data, out 1);
				for (int i = 1; i <= 3; i++) {
					if (numeric) {
						number = step.Data;
						steps.Add(new TestStep(order, "Verify Number of Header Items", "3", "verify_count", "xpath", "(//div[contains(@class,'event')]//a)["+ number +"]//div[contains(@class,'event-card-header')]//div[contains(@class,'flex-col')]", wait));
						steps.Add(new TestStep(order, "Verify First Slide Odds Header", "SPREAD", "verify_value", "xpath", "(//div[contains(@class,'event')])["+ number +"]//div[contains(@class,'feed-component')]//li[" + i + "]//div[contains(@class,'chart-container-header')]//div[contains(@class,'text fs')]", wait));
						steps.Add(new TestStep(order, "Verify Sub Header Text Exists", "", "verify_displayed", "xpath", "(//div[contains(@class,'event')])[" + number +"]//div[contains(@class,'feed-component')]//li[" + i + "]//div[contains(@class,'chart-container-header')]//div[contains(@class,'sub-header')]", wait));
						steps.Add(new TestStep(order, "Verify Odds Numbers Exist", "", "verify_displayed", "xpath", "((//div[contains(@class,'event')])[" + number + "]//div[contains(@class,'feed-component')]//li[" + i + "]//div[contains(@class,'chart-container-header')]//div[contains(@class,'number')])[1]", wait));
						steps.Add(new TestStep(order, "Verify Odds Numbers Exist", "", "verify_displayed", "xpath", "((//div[contains(@class,'event')])["+ number +"]//div[contains(@class,'feed-component')]//li[" + i + "]//div[contains(@class,'chart-container-header')]//div[contains(@class,'number')])[2]", wait));
						steps.Add(new TestStep(order, "Verify Odds Numbers Exist", "TEAM TO WIN", "verify_value", "xpath", "((//div[contains(@class,'event')])["+ number +"]//div[contains(@class,'feed-component')]//div[contains(@class,'chart-container-header')]//div[contains(@class,'text fs')])[2]", wait));
						steps.Add(new TestStep(order, "", "", "verify_displayed", "xpath", "", wait));
						TestRunner.RunTestSteps(driver, null, steps);
						steps.Clear();
					}
					if (i != 3) {
						steps.Add(new TestStep(order, "Click Arrow Right", "", "click", "xpath", "(//button[contains(@class,'next')])["+ number +"]", wait));
						steps.Add(new TestStep(order, "", "", "verify_displayed", "xpath", "", wait));
						TestRunner.RunTestSteps(driver, null, steps);
						steps.Clear();
					}
				}
			
			}
			
			else {
				throw new Exception("Test Step not found in script");
			}
		}
	}
}