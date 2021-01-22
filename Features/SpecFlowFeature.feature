Feature: SpecFlow Features Examples
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
	
@example
Scenario: Manejar dropdowns
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I select 'Mes de Nacimiento' dropdown by text 'Enero'
	Then I select 'Mes de Nacimiento' dropdown by text 'Marzo'

@example
Scenario: Manejar textbox
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I set 'Email' with 'mervindiazlugo@gmail.com'


@example
Scenario: Label text value
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I set 'Email' with 'mervindiazlugo@gmail.com'
	And I click on 'Email Confirmacion' element
	Given I pause '3' seconds
	Then I check element 'Email Error' contains 'Este correo electrónico ya está conectado a una cuenta. Inicia sesión.'
	Then I save text of label 'Email Error' in scenario context
	And I set 'Email Confirmacion' with '{scenario:Email Error}'
	Then I check element 'Email Error' contains '{scenario:Email Error}'
	Given I pause '5' seconds


	
@example
Scenario: Open New Tab
    Given Navigates to 'https://www.amazon.es/'
    And I open new tab with URL 'https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert'
    Then I go to 'Second' window
    Then I go to 'Principal' window
    And I open new tab with URL 'https://www.w3schools.com/'
    Then I go to 'Second' window
    And I put 'frames.json' as DOM
    And I Switch to 'Frame5 Alerta' Iframe
    Given I pause '5' seconds
    And I click on 'Alert' element
    Given I pause '5' seconds
    And I close Alert dialog
    Given I pause '5' seconds