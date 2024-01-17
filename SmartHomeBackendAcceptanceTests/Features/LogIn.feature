Feature: LogIn

@logintag
Scenario: Log in to system
	When I put the following credentials to log in
		| Email			  | Password |
		| adrian@test.com | 123      |
	Then I log in to system successfully