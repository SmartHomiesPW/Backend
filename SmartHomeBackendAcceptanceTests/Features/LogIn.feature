Feature: LogIn
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SmartHomeBackendAcceptanceTests/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Log in to system
	When I put the following credentials to log in
		| Email			  | Password |
		| adrian@test.com | 123      |
	Then I log in to system successfully