Feature: SpecFlowFeature1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@example
Scenario: Inicializar Navegador
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I set 'Email' with 'mervindiazlugo@gmail.com'
	And I select 'Mes de Nacimiento' dropdown by text 'Enero'
	When I Pause '5' seconds
	Then I select 'Mes de Nacimiento' dropdown by text 'Marzo'



