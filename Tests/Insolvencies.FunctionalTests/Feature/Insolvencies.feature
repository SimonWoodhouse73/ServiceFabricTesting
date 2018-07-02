Feature: Insolvencies
	As a valid user of Insolvency service
	I want to call the service with different Residence Ids which have Insolvency records
		So that I can confirm that correct Insolvency responses are returned
	Also I want to call the service with Residence Id which has no Insolvency records
		So that I can confirm that no Insolvency data is returned

Background:
Given The default domain root is from App.config DomainRoot
	And I generate an authorization token for Query and TS permitted purpose
	And I add headers from configuration
	| Header         |
	| Host           |
	| User-Agent     |
	| Correlation-Id |
	| Date           |

@INS_INCS_FunctionalTests
Scenario: INS_INCS_1_ToVerifyNoDataReturnedWhenNoInsolvenciesExist
	Given I have a ResidenceId 147233298
	When I call the Insolvencies Service based on current date
	Then the response returned should not have any InsolvencyOrder records

@INS_INCS_FunctionalTests
Scenario: INS_INCS_2_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=1
	Given I have a ResidenceId 164088245
	When I call the Insolvencies Service based on current date	
	Then the response returned should have 1 InsolvencyOrder records
	And the response returned should match the following InsolvencyOrder data
     | InsolvencyOrderId | InsolvencyServiceCaseId | OrderDate            | RestrictionsStartDate | RestrictionsEndDate    | DischargeDate        | LineofBusiness | ValueofDebt |
     | 196248            | 196248                  | 50 months 0 days old | 40 months 0 days old  | -108 months 0 days old | 40 months 0 days old | Hire Company   |             |	 
	 
@INS_INCS_FunctionalTests
Scenario: INS_INCS_3_ToVerifyCorrectValuesAreReturnedWhenNoOfInsolvencyAccount=4
	Given I have a ResidenceId 163014698
	When I call the Insolvencies Service based on current date	
	Then the response returned should have 4 InsolvencyOrder records
	And the response returned should match the following InsolvencyOrder data
     | InsolvencyOrderId | InsolvencyServiceCaseId | OrderDate            | RestrictionsStartDate | RestrictionsEndDate    | DischargeDate        | LineofBusiness | ValueofDebt | 
     | 192662            | 192662                  | 24 months 0 days old | 12 months 0 days old  | -167 months 0 days old | 12 months 0 days old |                |             | 
     | 192663            | 192663                  | 24 months 0 days old | 18 months 0 days old  | -15 months 0 days old  | 12 months 0 days old |                |             | 
     | 192664            | 192664                  | 24 months 9 days old | 18 months 0 days old  | -15 months 0 days old  |                      |                |             | 
     | 192665            | 192665                  | 24 months 0 days old | 12 months 0 days old  | -167 months 0 days old |                      |                |             | 

@INS_INCS_FunctionalTests
Scenario: INS_INCS_4_ToVerifyNoDataReturnedWhenNoInsolvenciesExistwithRetroDate
	Given I have a ResidenceId 147233298
	When I call the Insolvencies Service based on currentMinus0Days date
	Then the response returned should not have any InsolvencyOrder records	 

@INS_INCS_FunctionalTests
Scenario: INS_INCS_5_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount=4
	Given I have a ResidenceId 163014698
	When I call the Insolvencies Service based on currentMinus2Years date	
	Then the response returned should have 4 InsolvencyOrder records
	And the response returned should match the following InsolvencyOrder data
     | InsolvencyOrderId | InsolvencyServiceCaseId | OrderDate            | RestrictionsStartDate | RestrictionsEndDate    | DischargeDate        | LineofBusiness | ValueofDebt | 
     | 192662            | 192662                  | 24 months 0 days old | 12 months 0 days old  | -167 months 0 days old | 12 months 0 days old |                |             | 
     | 192663            | 192663                  | 24 months 0 days old | 18 months 0 days old  | -15 months 0 days old  | 12 months 0 days old |                |             | 
     | 192664            | 192664                  | 24 months 9 days old | 18 months 0 days old  | -15 months 0 days old  |                      |                |             | 
     | 192665            | 192665                  | 24 months 0 days old | 12 months 0 days old  | -167 months 0 days old |                      |                |             | 

@INS_INCS_FunctionalTests
Scenario: INS_INCS_6_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount=1
	Given I have a ResidenceId 164088255
	When I call the Insolvencies Service based on currentMinus5Years date		
	Then the response returned should not have any InsolvencyOrder records

Scenario: INS_INCS_7_ToVerifyCorrectInsolvencyTypeReturnedWhenNoOfInsolvencies=6
	Given I have a ResidenceId 208116264
	When I call the Insolvencies Service based on current date
	Then the response returned should match the following InsolvencyType data
	| Code | Description       |
	| DR   | Debt Relief Order |
	| DR   | Debt Relief Order |
	| DR   | Debt Relief Order |
	| DR   | Debt Relief Order |
	| BO   | Bankruptcy Order  |
	| DR   | Debt Relief Order |	

Scenario Outline: INS_INCS_8_ToVerifyCorrectInsolvencyTypeReturned
	Given I have a ResidenceId <ResidenceId>
	When I call the Insolvencies Service based on current date	
	Then the response returned should contain the following InsolvencyType data
	| Code   | Description   |
	| <Code> | <Description> |	
	Examples: 
	| ResidenceId | Code | Description                      |
	| 148488400   | TD   | Trust Deed                       |
	| 149292114   | IV   | Individual Voluntary Arrangement |
	| 149292115   | BO   | Bankruptcy Order                 |


Scenario Outline: INS_INCS_9_ToVerifyCorrectRestrictionsTypeReturned
	Given I have a ResidenceId <ResidenceId>
	When I call the Insolvencies Service based on current date	
	Then the response returned should contain the following RestrictionsType data
	| Code   | Description   |
	| <Code> | <Description> |	
	Examples: 
	| ResidenceId | Code | Description                                  |
	| 152662806   | BRU  | BANKRUPTCY RESTRICTIONS UNDERTAKING (BRU)    |
	| 156090673   | BRO  | INTERIM BANKRUPTCY RESTRICTIONS ORDER (IBRO) |
	| 156090685   | BRO  | BANKRUPTCY RESTRICTIONS ORDER (BRO)          |
	| 195268299   | BRU  | DEBT RELIEF RESTRICTION UNDERTAKING (DRRU)   |

@INS_INCS_FunctionalTests
Scenario: INS_INCS_10_ToVerifyCorrectValuesAreReturnedwithRetroDateWhenNoOfInsolvencyAccount=4
	Given I have a ResidenceId 208116264
	When I call the Insolvencies Service based on currentMinus2Years date	
	Then the response returned should have 4 InsolvencyOrder records
	And the response returned should match the following InsolvencyOrder data
     | InsolvencyOrderId | InsolvencyServiceCaseId | OrderDate             | RestrictionsStartDate | RestrictionsEndDate    | DischargeDate | LineofBusiness | ValueofDebt |
     | 1052241           | 1052241                 | 33 months 0 days old  | 2 months 5 days old   | -31 months 15 days old |               |                |             |
     | 1052240           | 1052240                 | 64 months 0 days old  | 9 months 7 days old   | -13 months -7 days old |               |                |             |
     | 1052238           | 1052238                 | 64 months 0 days old  | 14 months 7 days old  | -11 months -7 days old |               |                |             |
     | 1052239           | 1052239                 | 65 months 18 days old | 14 months 6 days old  | -11 months -7 days old |               |                |             |
