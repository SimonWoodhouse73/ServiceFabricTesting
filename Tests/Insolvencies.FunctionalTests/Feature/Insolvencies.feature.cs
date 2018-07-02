// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Insolvencies.FunctionalTests.Feature
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Insolvencies")]
    public partial class InsolvenciesFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Insolvencies.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Insolvencies", @"	As a valid user of Insolvency service
	I want to call the service with different Residence Ids which have Insolvency records
		So that I can confirm that correct Insolvency responses are returned
	Also I want to call the service with Residence Id which has no Insolvency records
		So that I can confirm that no Insolvency data is returned", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 8
#line 9
testRunner.Given("The default domain root is from App.config DomainRoot", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.And("I generate an authorization token for Query and TS permitted purpose", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Header"});
            table1.AddRow(new string[] {
                        "Host"});
            table1.AddRow(new string[] {
                        "User-Agent"});
            table1.AddRow(new string[] {
                        "Correlation-Id"});
            table1.AddRow(new string[] {
                        "Date"});
#line 11
 testRunner.And("I add headers from configuration", ((string)(null)), table1, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_1_ToVerifyNoDataReturnedWhenNoInsolvenciesExist")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_1_ToVerifyNoDataReturnedWhenNoInsolvenciesExist()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_1_ToVerifyNoDataReturnedWhenNoInsolvenciesExist", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 20
 testRunner.Given("I have a ResidenceId 147233298", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
 testRunner.Then("the response returned should not have any InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_2_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=1")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_2_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_2_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=1", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 25
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 26
 testRunner.Given("I have a ResidenceId 164088245", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the response returned should have 1 InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "InsolvencyOrderId",
                        "InsolvencyServiceCaseId",
                        "OrderDate",
                        "RestrictionsStartDate",
                        "RestrictionsEndDate",
                        "DischargeDate",
                        "LineofBusiness",
                        "ValueofDebt"});
            table2.AddRow(new string[] {
                        "196248",
                        "196248",
                        "50 months 0 days old",
                        "40 months 0 days old",
                        "-108 months 0 days old",
                        "40 months 0 days old",
                        "Hire Company",
                        ""});
#line 29
 testRunner.And("the response returned should match the following InsolvencyOrder data", ((string)(null)), table2, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_3_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=4")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_3_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount4()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_3_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=4", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 34
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 35
 testRunner.Given("I have a ResidenceId 163014698", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
 testRunner.Then("the response returned should have 4 InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "InsolvencyOrderId",
                        "InsolvencyServiceCaseId",
                        "OrderDate",
                        "RestrictionsStartDate",
                        "RestrictionsEndDate",
                        "DischargeDate",
                        "LineofBusiness",
                        "ValueofDebt"});
            table3.AddRow(new string[] {
                        "192662",
                        "192662",
                        "24 months 0 days old",
                        "12 months 0 days old",
                        "-167 months 0 days old",
                        "12 months 0 days old",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "192663",
                        "192663",
                        "24 months 0 days old",
                        "18 months 0 days old",
                        "-15 months 0 days old",
                        "12 months 0 days old",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "192664",
                        "192664",
                        "24 months 9 days old",
                        "18 months 0 days old",
                        "-15 months 0 days old",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "192665",
                        "192665",
                        "24 months 0 days old",
                        "12 months 0 days old",
                        "-167 months 0 days old",
                        "",
                        "",
                        ""});
#line 38
 testRunner.And("the response returned should match the following InsolvencyOrder data", ((string)(null)), table3, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_4_ToVerifyNoDataReturnedWhenNoInsolvenciesExistwithRetroDate")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_4_ToVerifyNoDataReturnedWhenNoInsolvenciesExistwithRetroDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_4_ToVerifyNoDataReturnedWhenNoInsolvenciesExistwithRetroDate", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 46
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 47
 testRunner.Given("I have a ResidenceId 147233298", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
 testRunner.When("I call the Insolvencies Service based on currentMinus0Days date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.Then("the response returned should not have any InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_5_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount" +
            "=4")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_5_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount4()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_5_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount" +
                    "=4", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 52
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 53
 testRunner.Given("I have a ResidenceId 163014698", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 54
 testRunner.When("I call the Insolvencies Service based on currentMinus2Years date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("the response returned should have 4 InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "InsolvencyOrderId",
                        "InsolvencyServiceCaseId",
                        "OrderDate",
                        "RestrictionsStartDate",
                        "RestrictionsEndDate",
                        "DischargeDate",
                        "LineofBusiness",
                        "ValueofDebt"});
            table4.AddRow(new string[] {
                        "192662",
                        "192662",
                        "24 months 0 days old",
                        "12 months 0 days old",
                        "-167 months 0 days old",
                        "12 months 0 days old",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "192663",
                        "192663",
                        "24 months 0 days old",
                        "18 months 0 days old",
                        "-15 months 0 days old",
                        "12 months 0 days old",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "192664",
                        "192664",
                        "24 months 9 days old",
                        "18 months 0 days old",
                        "-15 months 0 days old",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "192665",
                        "192665",
                        "24 months 0 days old",
                        "12 months 0 days old",
                        "-167 months 0 days old",
                        "",
                        "",
                        ""});
#line 56
 testRunner.And("the response returned should match the following InsolvencyOrder data", ((string)(null)), table4, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_6_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount" +
            "=1")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_6_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_6_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount" +
                    "=1", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 64
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 65
 testRunner.Given("I have a ResidenceId 164088255", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
 testRunner.When("I call the Insolvencies Service based on currentMinus5Years date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.Then("the response returned should not have any InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_7_ToVerifyCorrectInsolvencyTypeReturnedWhenNoOfInsolvencies=6")]
        public virtual void INS_INCS_7_ToVerifyCorrectInsolvencyTypeReturnedWhenNoOfInsolvencies6()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_7_ToVerifyCorrectInsolvencyTypeReturnedWhenNoOfInsolvencies=6", ((string[])(null)));
#line 69
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 70
 testRunner.Given("I have a ResidenceId 208116264", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 71
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Code",
                        "Description"});
            table5.AddRow(new string[] {
                        "DR",
                        "Debt Relief Order"});
            table5.AddRow(new string[] {
                        "DR",
                        "Debt Relief Order"});
            table5.AddRow(new string[] {
                        "DR",
                        "Debt Relief Order"});
            table5.AddRow(new string[] {
                        "DR",
                        "Debt Relief Order"});
            table5.AddRow(new string[] {
                        "BO",
                        "Bankruptcy Order"});
            table5.AddRow(new string[] {
                        "DR",
                        "Debt Relief Order"});
#line 72
 testRunner.Then("the response returned should match the following InsolvencyType data", ((string)(null)), table5, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_8_ToVerifyCorrectInsolvencyTypeReturned")]
        [NUnit.Framework.TestCaseAttribute("148488400", "TD", "Trust Deed", null)]
        [NUnit.Framework.TestCaseAttribute("149292114", "IV", "Individual Voluntary Arrangement", null)]
        [NUnit.Framework.TestCaseAttribute("149292115", "BO", "Bankruptcy Order", null)]
        public virtual void INS_INCS_8_ToVerifyCorrectInsolvencyTypeReturned(string residenceId, string code, string description, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_8_ToVerifyCorrectInsolvencyTypeReturned", exampleTags);
#line 81
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 82
 testRunner.Given(string.Format("I have a ResidenceId {0}", residenceId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 83
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Code",
                        "Description"});
            table6.AddRow(new string[] {
                        string.Format("{0}", code),
                        string.Format("{0}", description)});
#line 84
 testRunner.Then("the response returned should contain the following InsolvencyType data", ((string)(null)), table6, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_9_ToVerifyCorrectRestrictionsTypeReturned")]
        [NUnit.Framework.TestCaseAttribute("152662806", "BRU", "BANKRUPTCY RESTRICTIONS UNDERTAKING (BRU)", null)]
        [NUnit.Framework.TestCaseAttribute("156090673", "BRO", "INTERIM BANKRUPTCY RESTRICTIONS ORDER (IBRO)", null)]
        [NUnit.Framework.TestCaseAttribute("156090685", "BRO", "BANKRUPTCY RESTRICTIONS ORDER (BRO)", null)]
        [NUnit.Framework.TestCaseAttribute("195268299", "BRU", "DEBT RELIEF RESTRICTION UNDERTAKING (DRRU)", null)]
        public virtual void INS_INCS_9_ToVerifyCorrectRestrictionsTypeReturned(string residenceId, string code, string description, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_9_ToVerifyCorrectRestrictionsTypeReturned", exampleTags);
#line 94
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 95
 testRunner.Given(string.Format("I have a ResidenceId {0}", residenceId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 96
 testRunner.When("I call the Insolvencies Service based on current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Code",
                        "Description"});
            table7.AddRow(new string[] {
                        string.Format("{0}", code),
                        string.Format("{0}", description)});
#line 97
 testRunner.Then("the response returned should contain the following RestrictionsType data", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("INS_INCS_10_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccoun" +
            "t=4")]
        [NUnit.Framework.CategoryAttribute("INS_INCS_FunctionalTests")]
        public virtual void INS_INCS_10_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount4()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("INS_INCS_10_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccoun" +
                    "t=4", new string[] {
                        "INS_INCS_FunctionalTests"});
#line 108
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 109
 testRunner.Given("I have a ResidenceId 208116264", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 110
 testRunner.When("I call the Insolvencies Service based on currentMinus2Years date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 111
 testRunner.Then("the response returned should have 4 InsolvencyOrder records", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "InsolvencyOrderId",
                        "InsolvencyServiceCaseId",
                        "OrderDate",
                        "RestrictionsStartDate",
                        "RestrictionsEndDate",
                        "DischargeDate",
                        "LineofBusiness",
                        "ValueofDebt"});
            table8.AddRow(new string[] {
                        "1052241",
                        "1052241",
                        "33 months 0 days old",
                        "2 months 5 days old",
                        "-31 months 15 days old",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "1052240",
                        "1052240",
                        "64 months 0 days old",
                        "9 months 7 days old",
                        "-13 months -7 days old",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "1052238",
                        "1052238",
                        "64 months 0 days old",
                        "14 months 7 days old",
                        "-11 months -7 days old",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "1052239",
                        "1052239",
                        "65 months 18 days old",
                        "14 months 6 days old",
                        "-11 months -7 days old",
                        "",
                        "",
                        ""});
#line 112
 testRunner.And("the response returned should match the following InsolvencyOrder data", ((string)(null)), table8, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
