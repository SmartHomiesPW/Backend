Feature: Register

@mytag
Scenario: Register to the system
	When I put the following credentials to register
		| Email          | Password | FirstName | LastName   |
		| tomek@test.com | 123      | Tomasz    | Olbrychski |
	Then I register to system successfully